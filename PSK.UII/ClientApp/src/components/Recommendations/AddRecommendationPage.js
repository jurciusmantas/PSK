import React from "react"
import './RecommendationPage.css';

import { post } from '../../helpers/request'
import { get } from '../../helpers/request'
import { Link } from "react-router-dom";

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
                    if (this.topics != null) {
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

    render() {
        return (
            <form className="wrapper" onSubmit={this.onSubmit}>
                <h3>Add a recommendation</h3>
                <div className="invite-holder">
                    <div className="row">
                        <label>Select topic: </label>
                        {this.state.loading || !this.state.topics ?
                            <div>
                                loading...
                            </div>
                            :
                            <select
                                value={this.state.topicid}
                                onChange={this.handleOnChange}>
                                {this.showTopicOptions()}
                            </select>
                        }
                    </div>
                    <div className="row">
                        <label>Assign to:</label>
                        <input
                            type="text"
                            name="recommendedTo"
                            value={this.state.recommendedTo}
                            onChange={e => this.setState({ recommendedTo: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                            required/>
                    </div>
                    <div className="row">
                        <button className="btn btn-dark" type="submit">Submit</button>
                    </div>
                </div>
                <div className="row">
                    <Link to="/recommendations" className="btn btn-dark">Return</Link>
                </div>
            </form>
        )
    }
}

export default AddRecommendationPage;
