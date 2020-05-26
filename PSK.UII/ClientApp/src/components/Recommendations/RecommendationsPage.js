import React from "react"
import './RecommendationsPage.css';

import { get, del } from '../../helpers/request'
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'

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
                console.log("To:");
                console.log(res);
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
                console.log("By:");
                console.log(res);
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
                    <td><Link to={`topic?id=${recommendation.topicId}`}>{recommendation.topicName}</Link></td>
                    <td><Link to={''}>{recommendation.receiverName}</Link></td>{/* TODO change into normal link after employees are done */}
                    <td><button className="btn btn-custom" onClick={() => this.deleteRecommendation(recommendation.id)}>Delete</button></td>
                    <td><Link to={`edit-recommendation?id=${recommendation.id}`} className="btn btn-custom">Edit</Link></td>
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
            <div className="rec-wrapper">
                <div className='rec-holder'>
                    <h2>Recommended topics to learn for you:</h2>
                    <div className="row">
                        {this.state.loadingBy || !this.state.recommendedToEmp
                            ? <div className="loader">
                                <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px" />
                            </div>
                            : <table>
                                <thead>
                                    <tr>
                                        <th className="topicColumn">Topic</th>
                                        <th className="creatorColumn">Created by</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {this.showRecommendationList()}
                                </tbody>
                            </table>
                        }
                    </div>
                    <h2>Recommendations you have created:</h2>
                    <div className="row">
                        <Link to='add-recommendation' className="btn btn-custom">Add recommendation</Link>
                    </div>
                    <div className="row">
                        {this.state.loadingTo || !this.state.recommending
                            ? <div className="loader">
                                <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px" />
                            </div>
                            : <table>
                                <thead>
                                    <tr>
                                        <th className="topicColumn">Topic</th>
                                        <th className="creatorColumn">Created by</th>
                                        <th className="link" />
                                        <th className="link" />
                                    </tr>
                                </thead>
                                <tbody>
                                    {this.showCreatedRecommendationsList()}
                                </tbody>
                            </table>
                        }
                    </div>
                </div>
            </div>
        )
    }
}