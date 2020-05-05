import React from "react"
import './RecommendationPage.css';

import { get } from '../../helpers/request'
import { Link } from "react-router-dom";

export default class RecommendationsPage extends React.Component {
    constructor() {
        super()

        this.state = {
            recommendedToEmp: null,
            loadingBy: true,
            recommending: null,
            loadingTo: true
        }
    }

    componentDidMount() {
        var employeeId = 1; //TODO get current user. 
        get('recommendations?by=' + employeeId)
            .then(res => res.json())
            .then(res => {
                console.log("By:");
                console.log(res.data);
                if (res.success) {
                    this.setState({ recommendedToEmp: res.data, loadingBy: false })
                }
                else {
                    console.log(res.message);
                }
            })
            .catch(error => {
                console.log(error);
            })

        get('recommendations?to=' + employeeId)
            .then(res => res.json())
            .then(res => {
                console.log("To:");
                console.log(res.data);
                if (res.success) {
                    this.setState({ recommending: res.data, loadingTo: false })
                }
                else {
                    console.log(res.message);
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    showRecommendationList() {
        return this.state.recommendedToEmp.map((recommendation, index) => {
            return (
                <tr key={index}>
                    <td><Link to={'topic/' + recommendation.topicId}>{recommendation.topicName}</Link></td>
                    <td>{recommendation.creatorName}</td>
                </tr>
            )
        })
    }

    showCreatedRecommendationsList() {
        return this.state.recommending.map((recommendation, index) => {
            return (
                <tr key={index}>
                    <td>{recommendation.topicName}</td>
                    <td>{recommendation.receiverName}</td>
                    <td><Link to={'edit-recommendation/' + recommendation.id}>Edit</Link></td>
                </tr>
            )
        })
    }

    render() {
        return (
            <div className="wrapper">
                <div className="row">
                    <Link to='add-recommendation' className="btn btn-dark">Add recommendation</Link>
                </div>
                <h3>Recommended topics to learn for you:</h3>
                <div className="row">
                    {this.state.loadingBy || !this.state.recommendedToEmp ?
                        <div>loading...</div>
                        :
                        <table>
                            <tbody>
                                {this.showRecommendationList()}
                            </tbody>
                        </table>
                    }
                </div>
                <h3>Recommendations you have created:</h3>
                <div className="row">
                    {this.state.loadingTo || !this.state.recommending ?
                        <div>loading...</div>
                        :
                        <table>
                            <tbody>
                                {this.showCreatedRecommendationsList()}
                            </tbody>
                        </table>
                    }
                </div>
            </div>
        )
    }
}