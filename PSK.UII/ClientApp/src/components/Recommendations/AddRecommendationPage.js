import React from "react";
import './RecommendationsPage.css';

import { post } from '../../helpers/request';
import { get } from '../../helpers/request';
import { Link } from "react-router-dom";
import { notification } from "../../helpers/notification";
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import Loader from '../Loader/loader';

class AddRecommendationPage extends React.Component {
    constructor() {
        super()

        this.state = {
            topics: null,
            subordinates: null,
            loadingTopics: true,
            loadingSubordinates: true,
            topicId: null,
            subordinateId: null,
        }

        this.onSubmit = this.handleSubmit.bind(this)
    }

    componentDidMount() {
        get('topics')
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ topics: res.data, loadingTopics: false })
                    if (res.data != null && res.data.length > 0) {
                        this.setState({ topicId: res.data[0].id });
                    }
                }
                else {
                    notification("Cannot load topics :(", "error");
                    console.warn("Cannot load topics")
                    console.warn(res.message)
                }
            })
            .catch(error => {
                console.error(`GET topics failed:`);
                console.error(error);
            })

        get(`employees/${this.props.currentUser.id}/subordinates`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ subordinates: res.data, loadingSubordinates: false });
                    if (res.data != null && res.data.length > 0)
                        this.setState({ subordinateId: res.data[0].id });
                }
                else {
                    notification("Cannot load subordinates :(", "error");
                    console.warn("Cannot load subordinates");
                    console.warn(res.message);
                }
            })
            .catch(reason => {
                console.error(`GET employees/${this.props.currentUser.id}/subordinates failed:`);
                console.error(reason);
            });
    }

    handleOnTopicChange = (e) => {
        this.setState({
            topicId: e.target.value
        })
    }

    handleOnSubordinateChange = (e) => {
        this.setState({
            subordinateId: e.target.value
        })
    }

    handleSubmit(e) {
        e.preventDefault();

        const {
            topicId,
            subordinateId,
        } = this.state;
        const creatorId = this.props.currentUser.id;

        if (!topicId || !subordinateId) {
            notification('Topic and subordinate must be selected', 'error');
            return;
        }

        post('recommendations', {
            topicId: parseInt(topicId),
            receiverId: parseInt(subordinateId),
            creatorId: creatorId,
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.props.history.push('/recommendations');
                }
                else {
                    notification('Recommendation creation failed :(', 'warn');
                    console.warn('Recommendation creation failed:');
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error('POST recommendations failed:');
                console.error(error);
            })
    }

    showTopicOptions() {
        return this.state.topics.map((topic, index) =>
            <option key={index} value={topic.id}>
                {topic.name}
            </option>
        )
    }

    showSubTopicOptions() {
        return this.state.topics.map(topic =>
            topic.subTopicList.map((subTopic, index) =>
                <option key={index} value={subTopic.id}>
                    {subTopic.name}
                </option>
            )
        )
    }

    getSubordinatesOptions() {
        return this.state.subordinates.map(employee =>
            <option key={`subordinate-${employee.id}`} value={employee.id}>
                {employee.name}
            </option>
        );
    }

    render() {
        if (this.state.loadingSubordinates || !this.state.subordinates
            || this.state.loadingTopics || !this.state.topics)
            return <Loader />

        return (
            <form className="rec-wrapper" onSubmit={this.onSubmit}>
                <div className="rec-holder">
                    <h2>Add a recommendation</h2>
                    <div className="row">
                        <select
                            onChange={this.handleOnTopicChange}>
                            {this.showTopicOptions()}
                            {this.showSubTopicOptions()}
                        </select>
                    </div>
                    <div className="row">
                        <select
                            onChange={this.handleOnSubordinateChange}>
                            {this.getSubordinatesOptions()}
                        </select>
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
)(AddRecommendationPage));