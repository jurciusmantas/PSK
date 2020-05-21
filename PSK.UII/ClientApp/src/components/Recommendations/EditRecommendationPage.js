import React from "react"
import './RecommendationPage.css';
import { put, get } from '../../helpers/request'
import { Link, withRouter } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'

class EditRecommendationsPage extends React.Component {
    constructor() {
        super()

        this.state = {
            recommendation: null,
            topics: null,
            loading1: true,
            loading2: true,
            topicid: "",
            recommendedTo: "",
            id: window.location.pathname.split('/').pop()
        }
        this.onSubmit = this.handleSubmit.bind(this)
    }

    componentDidMount() {
        get('recommendations/recommendation/' + this.state.id)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ recommendation: res.data, loading1: false, topicid: res.data.topicId, recommendedTo: res.data.receiverName })
                }
                else {
                    console.log(res.message);
                }
            })
            .catch(error => {
                console.log(error);
            })

        get('topic/topic')
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ topics: res.data, loading2: false })
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.handleSubmit(e);
    }

    handleOnChange = (e) => {
        this.setState({
            topicid: e.target.value
        })
    }

    handleSubmit(e) {
        e.preventDefault();

        const {
            topicid,
            recommendedTo,
        } = this.state;

        var createdById = 1; //TODO: get current user id

        put('recommendations/update-recommendation/' + this.state.id, {
            topicid: parseInt(topicid),
            recommendedTo: recommendedTo,
            createdById: createdById
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    alert("Recommendation changed successfully");
                }
                else {
                    alert(res.message);
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    deleteRecommendation(id, e) {
        fetch("./api/recommendations/delete/" + id, {
            method: "delete"
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    alert("Recommendation deleted");
                    this.props.history.push('/recommendations'); 
                }
            })
            .catch(error => {
                console.log(error);
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
        return this.state.topics.map((topic) =>
            topic.subTopicList.map((subTopic, index) =>
                <option key={index} value={subTopic.id}>
                    {subTopic.name}
                </option>
            )
        )
    }

    render() {
        return (
            <form className="rec-wrapper" onSubmit={this.onSubmit}>
                <div className="rec-holder">
                    <h2>Edit recommendation</h2>
                    <div className='info'>
                        <div className="row">
                            {this.state.loading1 || !this.state.recommendation ? 
                                <div className="loader">
                                    <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px" />
                                </div>
                                :
                                <div>
                                    <h5>Current data:</h5>
                                    <p><b>Topic: </b>{this.state.recommendation.topicName}</p>
                                    <p><b>Asignee: </b>{this.state.recommendation.receiverName}</p>
                                </div>
                            }
                        </div>
                    </div>
                    <div className='newData'>
                        <div className="row">
                            {this.state.loading1 || !this.state.recommendation || this.state.loading2 || !this.state.topics ?
                                <div className="loader">
                                    <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px" />
                                </div>
                                :
                                <div>
                                    <h5>New data:</h5>
                                    <div className="row">
                                        <select
                                            value={this.state.topicid}
                                            onChange={this.handleOnChange} >
                                            {this.showTopicOptions()}
                                            {this.showSubTopicOptions()}
                                        </select>
                                    </div>
                                    <div className="row">
                                        <input
                                            type="text"
                                            defaultValue={this.state.recommendedTo}
                                            onChange={e => this.setState({ recommendedTo: e.target.value })}
                                            onKeyPress={e => this.handleKeyPress(e)}
                                            required />
                                    </div>
                                </div>
                            }
                        </div> 
                    </div>
                    <div className="row">
                        <Link to="/recommendations" className="btn btn-custom">Return</Link>
                        <button className="btn btn-custom" type="button" onClick={(e) => this.deleteRecommendation(this.state.id, e)}>Delete</button>
                        <button className="btn btn-custom" type="submit">Submit</button>
                    </div>
                </div>
            </form>
        )
    }
}

export default withRouter(EditRecommendationsPage);