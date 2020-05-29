import React from "react"
import './TopicPage.css';

import { put, get } from '../../helpers/request'
import { Link, withRouter } from "react-router-dom";
import Loader from '../Loader/loader';
import { notification } from "../../helpers/notification";
import { connect } from 'react-redux';

class EditTopicPage extends React.Component {
    constructor() {
        super()
        const query = new URLSearchParams(window.location.search);
        this.state = {
            topic: null,
            topicName: null,
            topicDescription: null,
            loadingTopic: true,
            topicId: query.get("id"),
        }
        this.onSubmit = this.handleSubmit.bind(this);

    }

    componentDidMount() {
        get(`topics/${this.state.topicId}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({
                        topic: res.data,
                        topicName: res.data.name,
                        topicDescription: res.data.description,
                        loadingTopic: false,
                        topicId: res.data.id,
                    })
                }
                else {
                    console.warn(`Cannot get topic:`);
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error(`GET topics/${this.state.topicId} failed:`);
                console.error(error);
            });
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.handleSubmit(e);
    }

    handleSubmit(e) {
        e.preventDefault();

        const {
            topic,
            topicName,
            topicDescription,
            topicId
        } = this.state;

        put(`topics/${topicId}`, {
            id: topicId,
            name: topicName,
            description: topicDescription,
            rowVersion: topic.rowVersion
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    notification("Updated!");
                    this.props.history.push(`topic?id=${topicId}`);
                }
                else {
                    notification(res.message, 'error');
                    console.warn("Cannot update topic");
                    console.warn(res.message);
                    this.setState({
                        topic: res.data,
                        topicName: res.data.name,
                        topicDescription: res.data.description
                    });
                }
            })
            .catch(error => {
                console.error(`PUT topics/${topicId} failed:`)
                console.error(error)
            });
    }

    render() {
        return (
            <form className="topic-wrapper" onSubmit={this.onSubmit}>
                <div className="topic-holder">
                    <h2>Edit topic</h2>
                    <div className='info'>
                        <div className="row">
                            {this.state.loadingTopic || !this.state.topic
                                ? <Loader/>
                                : <div>
                                    <h5>Current data:</h5>
                                    <p><b>Topic: </b>{this.state.topic.name}</p>
                                    <p><b>Description: </b>{this.state.topic.description}</p>
                                </div>
                            }
                        </div>
                    </div>
                    <div className='newData'>
                        <div className="row">
                            {this.state.loadingTopic || !this.state.topic
                                ? <Loader/>
                                : <div>
                                    <h5>Enter new data:</h5>
                                    <div className='row'>
                                        <input
                                            type="text"
                                            defaultValue={this.state.topicName}
                                            onChange={e => this.setState({ topicName: e.target.value })}
                                            onKeyPress={e => this.handleKeyPress(e)}
                                            required />
                                        <textarea cols="50"
                                            defaultValue={this.state.topicDescription}
                                            onChange={e => this.setState({ topicDescription: e.target.value })}
                                            onKeyPress={e => this.handleKeyPress(e)}
                                        />
                                    </div>
                                </div>
                            }
                        </div>
                    </div>
                    <div className="row">
                        <button className="btn btn-custom" type="submit">Submit</button>
                        <Link to="/topics" className="btn btn-custom">Return</Link>
                    </div>
                </div>
            </form>
        )
    }
}

const mapStateToProps = (state) => ({
    currentUser: state.currentUser
});

const mapDispatchToProps = () => ({})

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(EditTopicPage));