import React from "react"
import './RecommendationPage.css';
import { post } from '../../helpers/request'
import { get } from '../../helpers/request'
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'

class AddRecommendationPage extends React.Component {
    constructor() {
        super()

        this.state = {
            topics: null,
            loading: true,
            topicid: "",
            recommendedTo: "",
        }

        this.onSubmit = this.handleSubmit.bind(this)
    }

    componentDidMount() {
        get('topic/topic').then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ topics: res.data, loading: false })
                    if (res.data != null) {
                        this.setState({ topicid: res.data[0].id });
                    }
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

        var createdById = 1;  //TODO: get current user id

        post('recommendations', {
            topicid: parseInt(topicid),
            recommendedTo: recommendedTo,
            createdById: createdById
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    alert("Recommendation added");
                }
                else {
                    alert(res.message);
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
                    <h2>Add recommendation</h2>
                    <div className="row">
                        {this.state.loading || !this.state.topics ?
                            <div className="loader">
                                <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px" />
                            </div>
                            :
                            <select
                                value={this.state.topicid}
                                onChange={this.handleOnChange}>
                                {this.showTopicOptions()}
                                {this.showSubTopicOptions()}
                            </select>
                        }
                    </div>
                    <div className="row">
                        <input
                            type="text"
                            placeholder='Assign to'
                            value={this.state.recommendedTo}
                            onChange={e => this.setState({ recommendedTo: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                            required />
                    </div>
                    <div className="row">
                        <Link to="/recommendations" className="btn btn-custom">Return</Link>
                        <button className="btn btn-custom" type="submit">Submit</button>
                    </div>
                </div>
            </form>
        )
    }
}

export default AddRecommendationPage;
