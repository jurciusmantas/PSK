import React from "react"
import './RecommendationPage.css';
import 'bootstrap/dist/css/bootstrap.css';
import { get } from '../../helpers/request'
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'

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
        var employeeId = 1; //TODO get current user. 
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
                    <td className="topicColumn">{recommendation.topicName}</td>
                    <td className="creatorColumn">{recommendation.creatorName}</td>
                    <td className="topicColum"><Link to={'topic/' + recommendation.topicId}>More</Link></td>
                </tr>
            )
        })
    }

    showCreatedRecommendationsList() {
        return this.state.recommending.map((recommendation, index) => {
            return (
                <tr key={index}>
                    <td className="topicColumn">{recommendation.topicName}</td>
                    <td className="creatorColumn">{recommendation.receiverName}</td>
                    <td className="topicColum"><Link to={'edit-recommendation/' + recommendation.id}>Edit</Link></td>
                </tr>
            )
        })
    }

    render() {
        return (
            <div className="rec-wrapper">
                <div className="rec-holder">
                    <div className="row">
                        <Link to='add-recommendation/' className="btn btn-custom">Add recommendation</Link>
                    </div>
                    <h2>Recommended topics to learn for you:</h2>
                    <div className="row">
                        {this.state.loading1 || !this.state.recommendedToEmp ?
                            <div className="loader">
                                <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px"/>
                            </div>
                            :
                            <table>
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
                        {this.state.loading2 || !this.state.recommending ?
                            <div className="loader">
                                <FontAwesomeIcon icon={faSpinner} class="fa-spin" height="20px" />
                            </div>
                            :
                            <table>
                                <thead>
                                    <tr>
                                        <th className="topicColumn">Topic</th>
                                        <th className="creatorColumn">Created by</th>
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

export default RecommendationsPage;
