import React from "react"
import { connect } from 'react-redux';
import './RecommendationsPage.css';

import { get, del } from '../../helpers/request'
import { Link } from "react-router-dom";
import { notification } from "../../helpers/notification";
import Loader from '../Loader/loader';

class RecommendationsPage extends React.Component {
    constructor(props) {
        super(props)

        this.state = {
            recommendedTo: null,
            loadingBy: true,
            recommendedBy: null,
            loadingTo: true
        };

        this.deleteRecommendation = this.deleteRecommendation.bind(this);
    }

    componentDidMount() {
        var employeeId = this.props.currentUser.id
        get(`recommendations?to=${employeeId}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ recommendedTo: res.data, loadingTo: false })
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
                    this.setState({ recommendedBy: res.data, loadingBy: false })
                }
                else {
                    console.warn(`GET recommendations?by=${employeeId} failed: ${res.message}`);
                }
            })
            .catch(error => console.error(error));
    }

    showRecommendationList() {
        return this.state.recommendedTo.map((recommendation, index) => {
            return (
                <tr key={index}>
                    <td><Link to={`topic?id=${recommendation.topicId}`}>{recommendation.topicName}</Link></td>
                    <td><Link to={`user-profile?id=${recommendation.creatorId}`}>{recommendation.creatorName}</Link></td>                  
                </tr>
            )
        })
    }

    showCreatedRecommendationsList() {
        return this.state.recommendedBy.map((recommendation, index) => {
            return (
                <tr key={index}>
                    <td><Link to={`topic?id=${recommendation.topicId}`}>{recommendation.topicName}</Link></td>
                    <td><Link to={`user-profile?id=${recommendation.receiverId}`}>{recommendation.receiverName}</Link></td>{/* TODO change into normal link after employees are done */}
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
                    notification("Deleted!");
                    this.setState({
                        recommendedBy: this.state.recommendedBy.filter(r => r.id !== id)
                    });
                }
            })
            .catch(error => console.error(error));
    }

    render() {
        if (this.state.loadingBy || this.state.loadingTo)
            return <Loader />

        return (
            <div className="rec-wrapper">
                <div className='rec-holder'>
                    <h2>Recommended topics to learn for you:</h2>
                    <div className="row">
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
                    </div>
                    <h2>Recommendations you have created:</h2>
                    <div className="row">
                        <Link to='add-recommendation' className="btn btn-custom">Add recommendation</Link>
                    </div>
                    <div className="row">
                        <table>
                            <thead>
                                <tr>
                                    <th className="topicColumn">Topic</th>
                                    <th className="creatorColumn">Created for</th>
                                    <th className="link" />
                                    <th className="link" />
                                </tr>
                            </thead>
                            <tbody>
                                {this.showCreatedRecommendationsList()}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        )
    }
}

const mapStateToProps = (state) => ({
    currentUser: state.currentUser
});

const mapDispatchToProps = () => ({})

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(RecommendationsPage);