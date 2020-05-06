import React from "react"
import './RecommendationsPage.css';

import { put, get } from '../../helpers/request'
import { Link, withRouter } from "react-router-dom";

class EditRecommendationsPage extends React.Component {
    constructor() {
        super()
        const query = new URLSearchParams(window.location.search);
        this.state = {
            recommendation: null,
            topics: null,
            loading1: true,
            loading2: true,
            topicId: null,
            recommendedTo: "",
            id: query.get("id"),
        }
        this.onSubmit = this.handleSubmit.bind(this)
    }

    componentDidMount() {
        get(`recommendations/${this.state.id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ recommendation: res.data, loading1: false, topicId: res.data.topicId, recommendedTo: res.data.receiverName })
                }
                else {
                    console.warn(`GET recommendations/${this.state.id} failed: ${res.message}`);
                }
            })
            .catch(error => console.error(error));
        get(`topics`)
            .then(r => r.json())
            .then(r => {
                if (r.success) {
                    this.setState({ topics: r.data, loading2: false })
                }
                else {
                    console.error(`GET topics failed: ${r.message}`);
                }
            })
            .catch(error => console.error(error));
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.handleSubmit(e);
    }

    handleOnChange = (e) => {
        this.setState({
            topicId: e.target.value
        })
    }

    handleSubmit(e) {
        e.preventDefault();

        const {
            topicId,
            recommendedTo,
        } = this.state;

        const createdById = 1; //TODO: get current user id

        put('recommendations/' + this.state.id, {
            topicid: parseInt(topicId),
            recommendedTo: recommendedTo,
            createdById: createdById
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    alert("Recommendation updated successfully");
                }
                else {
                    alert(res.message);
                }
            })
            .catch(error => console.error(error));
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
            <form className="wrapper" onSubmit={this.onSubmit}>
                <h3>Edit recommendation</h3>
                <div className="row">
                    {this.state.loading1 || !this.state.recommendation
                        ? <div>loading...</div>
                        : <div>{this.state.recommendation.topicName} {this.state.recommendation.receiverName}</div>
                    }
                </div>
                <div className="row">
                    <h3>Enter new data:</h3>
                    {this.state.loading1 || !this.state.recommendation || this.state.loading2 || !this.state.topics
                        ? <div> loading... </div>
                        : <div>
                            <select
                                value={this.state.topicId}
                                onChange={this.handleOnChange}
                            >
                                {this.showTopicOptions()}
                                {this.showSubTopicOptions()}
                            </select>
                            <input
                                type="text"
                                name="recommendedTo"
                                defaultValue={this.state.recommendedTo}
                                onChange={e => this.setState({ recommendedTo: e.target.value })}
                                onKeyPress={e => this.handleKeyPress(e)}
                                required />
                        </div>
                    }
                </div>
                <div className="row">
                    <Link to="/recommendations" className="btn btn-dark">Return</Link>
                    <button className="btn btn-dark" type="submit">Submit</button>
                </div>
            </form>
        )
    }
}

export default withRouter(EditRecommendationsPage);