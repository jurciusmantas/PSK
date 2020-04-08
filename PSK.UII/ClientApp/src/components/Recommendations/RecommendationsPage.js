import React from "react"
import '../Invite/InvitePage.css';

import { get } from '../../helpers/request'

import { Link } from "react-router-dom";

class RecommendationsPage extends React.Component {
    constructor() {
        super()

        this.state = {
            recommendedToEmp: null,
            loading1: true,
            recommending: null,
            loading2: true
        }
    }

    componentDidMount() {
        var employeeId = 0; //TODO get current user. 
        get('recommendations/recommendations/' + employeeId)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ recommendedToEmp: res.data, loading1: false })
                }
                else {
                    console.log(res.message);
                }
            })
            .catch(error => {
                console.log(error);
            })

        get('recommendations/recommended/' + employeeId)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ recommending: res.data, loading2: false })
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
                    <td>{recommendation.topic.name}</td>
                    <td>{recommendation.createdBy.name}</td>
                </tr>
            )
        })
    }

    showCreatedRecommendationsList() {
        return this.state.recommending.map((recommendation, index) => {
            return (
                <tr key={index}>
                    <td>{recommendation.topic.name}</td>
                    <td>{recommendation.recommendedTo.name}</td>
                    <td><Link to={'edit-recommendation/' + recommendation.id}>Edit</Link></td>
                </tr>
            )
        })
    }

    render() {
        return (
            <div className="invite-wrapper">
                <div className="row">
                    <Link to='add-recommendation/'>Add recommendation</Link>
                </div>
                <h3 className="row">Recommended topics to learn for you:</h3>
                <div className="row">
                    {this.state.loading1 || !this.state.recommendedToEmp ?
                        <div>
                            loading...
                    </div>
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
                    {this.state.loading2 || !this.state.recommending ?
                        <div>
                            loading...
                        </div>
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

export default RecommendationsPage;
