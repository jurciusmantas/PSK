import React from 'react';
import './TopicPage.css';
import { get } from '../../helpers/request';
import { Link } from 'react-router-dom';
import Loader from '../Loader/loader'
import { notification } from '../../helpers/notification';
import NotFoundPage from '../NotFound/NotFoundPage';
import moment from 'moment';

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
            loadingLearnedSubordinates: true,
            learnedSubordinates: null,
        }
    }

    componentDidMount() {
        this.setDetails();
        this.loadLearnedSubordinates();
    }

    setDetails() {
        get(`topics/${this.state.topicId}`).then(res => res.json())
            .then(res => {
                if (res.success)
                    this.setState({ topic: res.data, loadingTopic: false });
                
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
            })
            .catch(reason => {
                console.error(`GET topics/${this.state.topicId}/subtopics failed:`)
                console.error(reason)
            })
    }

    loadLearnedSubordinates(){
        get(`topics/${this.state.topicId}/learnedsubordinates`)
            .then(res => res.json())
            .then(res => {
                if (res.success)
                    this.setState({
                        loadingLearnedSubordinates: false,
                        learnedSubordinates: res.data,
                    })

                else {
                    notification("Cannot load subtopics", "warning")
                    this.setState({ loadingLearnedSubordinates: false })
                    console.warn("Cannot load subtopics")
                    console.warn(res.message)
                }    
            })
            .catch(reason => {
                console.error(`GET topics/${this.state.topicId}/learnedsubordinates failed:`)
                console.error(reason)
            })
    }

    getSubtopicTable() {
        if (Array.isArray(this.state.subtopics) && this.state.subtopics.length === 0)
            return <div>No subtopics</div>;

        return (
            <table>
                <tbody>
                    { this.state.subtopics.map(d => 
                            <tr key={`subtopic-list-item-${d.id}`}>
                                <td>
                                    <Link 
                                        onClick={this.forceUpdate} 
                                        to={{ pathname: "/topic", search: `?id=${d.id}` }}
                                    >
                                        {d.name}
                                    </Link>
                                </td>
                            </tr>
                        )
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
            <React.Fragment>
                <div className="topic-wrapper" style={{marginTop: '20px'}}>
                    <div className="topic-holder">
                        <h2>{this.state.topic.name}</h2>
                        <h5>Description</h5>
                        <p>{this.state.topic.description}</p>
                        <Link className="btn btn-custom" to={{ pathname: "/edit-topic", search: `?id=${this.state.topicId}` }}>Edit</Link>
                    </div>
                </div>
                <div className="topic-wrapper" style={{marginTop: '20px'}}>
                    <div className="topic-holder">
                        <h5>Subtopics</h5>
                        <div>
                            <Link className="btn btn-dark" to={{ pathname: "/add-topic", search: `?parent=${this.state.topicId}` }}>Add New Subtopic</Link>
                        </div>
                        { this.state.loadingSubtopics ? <Loader /> : this.getSubtopicTable()}
                    </div>
                </div>
                <div className="topic-wrapper" style={{marginTop: '20px'}}>
                    <div className="topic-holder">
                        <h5>Subordinates who learned this topic</h5>
                        { this.state.loadingLearnedSubordinates &&
                            <Loader/>
                        }
                        { !this.state.loadingLearnedSubordinates && (!this.state.learnedSubordinates || this.state.learnedSubordinates.length === 0) &&
                            <div>No subordinate learned or is learning this topic</div>
                        }
                        { !this.state.loadingLearnedSubordinates && this.state.learnedSubordinates && this.state.learnedSubordinates.length > 0 &&
                            <table>
                                <thead>
                                    <tr>
                                        <th>Subordinate name</th>
                                        <th>Completed on</th>
                                        <th>Learns at</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    { this.state.learnedSubordinates.map(s => 
                                        <tr>
                                            <td>{s.subordinateName}</td>
                                            <td>{s.completedOn ? moment(s.completedOn).format('YYYY-MM-DD') : '-'}</td>
                                            <td>{s.learnsAt ? moment(s.learnsAt).format('YYYY-MM-DD') : '-'}</td>
                                        </tr>    
                                    )}
                                </tbody>
                            </table>
                        }
                    </div>
                </div>
            </React.Fragment>
        )
    }
}
