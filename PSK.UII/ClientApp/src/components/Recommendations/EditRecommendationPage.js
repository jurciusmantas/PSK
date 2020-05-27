import React from "react"
import './RecommendationsPage.css';

import { put, get } from '../../helpers/request'
import { Link, withRouter } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'
import { notification } from "../../helpers/notification";
import { connect } from 'react-redux';

class EditRecommendationsPage extends React.Component {
    constructor() {
        super()
        const query = new URLSearchParams(window.location.search);
        this.state = {
            recommendation: null,
            topics: null,
            subordinates: null,
            loadingRecommendation: true,
            loadingTopics: true,
            loadingSubordinates: true,
            topicId: null,
            subordinateId: null,
            recommendationId: query.get("id"),
        }
        this.onSubmit = this.handleSubmit.bind(this);
        this.handleOnTopicChange = this.handleOnTopicChange.bind(this);
        this.handleOnSubordinateChange = this.handleOnSubordinateChange.bind(this);
    }

    componentDidMount() {
        get(`recommendations/${this.state.recommendationId}`)
            .then(res => res.json())
            .then(res => {
                console.log(`GET recommendations/${this.state.recommendationId} finished`)
                console.log(res);
                if (res.success) {
                    this.setState({
                        recommendation: res.data,
                        loadingRecommendation: false,
                        topicId: res.data.topicId,
                        subordinateId: res.data.receiverId
                    })
                }
                else {
                    console.warn(`Cannot get recommendation:`);
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error(`GET recommendations/${this.state.recommendationId} failed:`);
                console.error(error);
            });
        get(`topics`)
            .then(res => res.json())
            .then(res => {
                console.log(`GET topics finished`);
                console.log(res);
                if (res.success) {
                    this.setState({ topics: res.data, loadingTopics: false })
                }
                else {
                    console.warn(`Cannot load topics:`);
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error(`GET topics failed:`);
                console.error(error);
            });
        get(`employees/${this.props.currentUser.id}/subordinates`)
            .then(res => res.json())
            .then(res => {
                console.log(`GET employees/${this.props.currentUser.id}/subordinates finished`)
                console.log(res);
                if (res.success) {
                    this.setState({ subordinates: res.data, loadingSubordinates: false });
                }
                else {
                    console.warn('Cannot load subordinates:');
                    console.warn(res.message);
                }
            })
            .catch(reason => {
                console.error(`GET employees/${this.props.currentUser.id}/subordinates failed`);
                console.error(reason);
            })
    }

    handleSubmit(e) {
        e.preventDefault();

        const {
            topicId,
            subordinateId,
            recommendationId
        } = this.state;

        const creatorId = this.props.currentUser.id

        put(`recommendations/${recommendationId}`, {
            topicId: parseInt(topicId),
            receiverId: parseInt(subordinateId),
            creatorId: creatorId
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    notification("Updated!");
                    this.props.history.push('/recommendations');
                }
                else {
                    notification("Error updating :(", 'error');
                    console.warn("Cannot update recommendation:");
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error(`PUT recommendations/${recommendationId} failed:`)
                console.error(error)
            });
    }

    showTopicOptions() {
        return this.state.topics.map(topic =>
            <option key={`topic-${topic.id}`} value={topic.id}>
                {topic.name}
            </option>
        )
    }

    showSubTopicOptions() {
        return this.state.topics.map((topic) =>
            topic.subTopicList.map((subTopic) =>
                <option key={`topic-${subTopic.id}`} value={subTopic.id}>
                    {subTopic.name}
                </option>
            )
        )
    }

    getSubordinatesOptions() {
        return this.state.subordinates.map(employee =>
            <option key={`subordinate-${employee.id}`} value={employee.id}>{employee.name}</option>
        );
    }

    handleOnTopicChange(e) {
        this.setState({ topicId: e.target.value })
    }

    handleOnSubordinateChange(e) {
        this.setState({ subordinateId: e.target.value });
    }

    render() {
        return (
            <form className="rec-wrapper" onSubmit={this.onSubmit}>
                <div className="rec-holder">
                    <h2>Edit recommendation</h2>
                    <div className='info'>
                        <div className="row">
                            {this.state.loadingRecommendation || !this.state.recommendation
                                ? <div className="loader">
                                    <FontAwesomeIcon icon={faSpinner} className="fa-spin" height="20px" />
                                </div>
                                : <div>
                                    <h5>Current data:</h5>
                                    <p><b>Topic: </b>{this.state.recommendation.topicName}</p>
                                    <p><b>Asignee: </b>{this.state.recommendation.receiverName}</p>
                                </div>
                            }
                        </div>
                    </div>
                    <div className='newData'>
                        <div className="row">
                            {this.state.loadingRecommendation
                                || !this.state.recommendation
                                || this.state.loadingTopics
                                || !this.state.topics
                                || this.state.loadingSubordinates
                                || !this.state.subordinates
                                ? <div className="loader">
                                    <FontAwesomeIcon icon={faSpinner} className="fa-spin" height="20px" />
                                </div>
                                : <div>
                                    <h5>Enter new data:</h5>
                                    <div className='row'>
                                        <select
                                            defaultValue={this.state.topicId}
                                            onChange={this.handleOnTopicChange}
                                        >
                                            {this.showTopicOptions()}
                                            {this.showSubTopicOptions()}
                                        </select>
                                    </div>
                                    <div className='row'>
                                        <select
                                            defaultValue={this.state.subordinateId}
                                            onChange={this.handleOnSubordinateChange}
                                        >
                                            {this.getSubordinatesOptions()}
                                        </select>
                                    </div>
                                </div>
                            }
                        </div>
                    </div>
                    <div className="row">
                        <button className="btn btn-custom" type="submit">Submit</button>
                        <Link to="/recommendations" className="btn btn-custom">Return</Link>
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
)(EditRecommendationsPage));