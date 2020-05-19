import React from "react"
import './RecommendationsPage.css';

import { get, del } from '../../helpers/request'
import { Link } from "react-router-dom";

export default class RecommendationsPage extends React.Component {
    constructor(props) {
        super(props)

        this.state = {
            recommendedToEmp: null,
            loadingBy: true,
            recommending: null,
            loadingTo: true
        };

        this.deleteRecommendation = this.deleteRecommendation.bind(this);
    }

    componentDidMount() {
        var employeeId = 1; //TODO get current user. 
        get(`recommendations?to=${employeeId}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ recommendedToEmp: res.data, loadingBy: false })
                }
                else {
                    console.warn(`GET recommendations?to=${employeeId} failed: ${res.message}`);
                }
            })
            .catch(error => console.error(error));

        get(`recommendations?by=${employeeId}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ recommending: res.data, loadingTo: false })
                }
                else {
                    console.warn(`GET recommendations?by=${employeeId} failed: ${res.message}`);
                }
            })
            .catch(error => console.error(error));
    }

    showRecommendationList() {
        return this.state.recommendedToEmp.map((recommendation, index) => {
            return (
                <tr key={index}>
                    <td><Link to={`topic?id=${recommendation.topicId}`}>{recommendation.topicName}</Link></td>
                    <td>{recommendation.creatorName}</td>
                </tr>
            )
        })
    }

    showCreatedRecommendationsList() {
        return this.state.recommending.map((recommendation, index) => {
            return (
                <tr key={index}>
                    <td><Link to={`edit-recommendation?id=${recommendation.id}`}>{recommendation.topicName}</Link></td>
                    <td>for <Link to={''}>{recommendation.receiverName}</Link></td>{/* TODO change into normal link after employees are done */}
                    <td><Link onClick={() => this.deleteRecommendation(recommendation.id)}>X</Link></td>
                </tr>
            )
        })
    }

    deleteRecommendation(id) {
        del(`recommendations/${id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    alert("Recommendation deleted");
                    this.setState({
                        recommending: this.state.recommending.filter(r => r.id !== id)
                    });
                }
            })
            .catch(error => console.error(error));
    }

    render() {
        return (
            <div className="wrapper">
                <h3>Recommended topics to learn for you:</h3>
                <div className="row">
                    {this.state.loadingBy || !this.state.recommendedToEmp
                        ? <div>loading...</div>
                        : <table>
                            <tbody>
                                {this.showRecommendationList()}
                            </tbody>
                        </table>
                    }
                </div>
                <h3>Recommendations you have created:</h3>
                <div className="row">
                    {this.state.loadingTo || !this.state.recommending
                        ? <div>loading...</div>
                        : <table>
                            <tbody>
                                {this.showCreatedRecommendationsList()}
                            </tbody>
                        </table>
                    }
                </div>
                <div className="row">
                    <Link to='add-recommendation' className="btn btn-dark">Add recommendation</Link>
                </div>
            </div>
        )
    }
}