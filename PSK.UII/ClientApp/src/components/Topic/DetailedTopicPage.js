import React from 'react';
import './TopicPage.css';
import { get } from '../../helpers/request';
import { Link } from 'react-router-dom';
import Loader from '../Loader/loader'
import { notification } from '../../helpers/notification';
import NotFoundPage from '../NotFound/NotFoundPage';

export default class DetailedTopicPage extends React.Component {
    constructor(props) {
        super(props);
        const queryParams = new URLSearchParams(window.location.search);
        this.state = {
            topic: null,
            loadingTopic: true,
            subtopics: null,
            loadingSubtopics: true,
            topicId: queryParams.get("id"),
        }
    }

    componentDidMount() {
        this.setDetails();
    }

    setDetails() {
        get(`topics/${this.state.topicId}`).then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ topic: res.data, loadingTopic: false });
                }
                else {
                    notification("Cannot load topic :(", "error");
                    console.warn("Cannot load topic:")
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error(`GET topics/${this.state.topicId} failed:`)
                console.error(error);
            })
        get(`topics/${this.state.topicId}/subtopics`)
            .then(res => res.json())
            .then(res => {
                if (res.success)
                    this.setState({ subtopics: res.data, loadingSubtopics: false })
                else {
                    notification("Cannot load subtopics", "warning")
                    console.warn("Cannot load subtopics")
                    console.warn(res.message)
                }
            }).catch(reason => {
                console.error(`GET topics/${this.state.topicId}/subtopics failed:`)
                console.error(reason)
            })
    }

    getSubtopicTable() {
        if (Array.isArray(this.state.subtopics) && this.state.subtopics.length === 0) {
            return (
                <div>No subtopics</div>
            )
        }

        return (
            <table>
                <tbody>
                    {
                        this.state.subtopics.map((d) => {
                            const { id, name } = d

                            return (
                                <tr key={`subtopic-list-item-${id}`}>
                                    <td>
                                        <Link onClick={this.forceUpdate} to={{ pathname: "/topic", search: `?id=${id}` }}>{name}</Link>
                                    </td>
                                </tr>
                            )
                        })
                    }
                </tbody>
            </table>
        )
    }

    render() {
        if (this.state.loadingTopic)
            return <Loader/>
        if (!this.state.topic || this.state.topicId === null)
            return <NotFoundPage />

        return (
            <div className="topic-wrapper">
                <div className="topic-holder">
                    {this.state.loadingTopic
                        ? <Loader />
                        : <>
                            <h2>{this.state.topic.name}</h2>
                            <h5>Description</h5>
                            <p>{this.state.topic.description}</p>
                            <Link className="btn btn-custom" to={{ pathname: "/edit-topic", search: `?id=${this.state.topicId}` }}>Edit</Link>
                        </>}

                    <hr />
                    <h5>Subtopics</h5>
                    <div>
                        <Link className="btn btn-dark" to={{ pathname: "/add-topic", search: `?parent=${this.state.topicId}` }}>Add New Subtopic</Link>
                    </div>
                    {this.state.loadingSubtopics
                        ? <Loader />
                        : this.getSubtopicTable()}
                </div>
            </div>
        )
    }
}
