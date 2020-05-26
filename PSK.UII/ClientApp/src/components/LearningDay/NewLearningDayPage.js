import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import moment from 'moment';
import { get, post } from '../../helpers/request';

import './NewLearningDayPage.css';

class NewLearningDayPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedDate: moment().format("YYYY-MM-DD"),
            topics: [],
            selectedTopic: {},
            recommendations: [],
        };
        this.changeDate = this.changeDate.bind(this);
        this.changeTopic = this.changeTopic.bind(this);
        this.createDay = this.createDay.bind(this);
        this.makeTopicOptionList = this.makeTopicOptionList.bind(this);
    }

    componentDidMount() {
        get("topics").then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ topics: res.data });
        }).catch(err => {
            console.error(`GET /api/topics failed: ${err}`);
            this.setState({ topics: [] });
        });
        // Use this when login returns user id:
        // get(`recommendations?receiverId=${this.state.currentUser.id}`).then(res => res.json()).then(res => {
        // for now hardcoded for use with mock repository
        get(`recommendations?receiverId=3`).then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ recommendations: res.data });
        }).catch(err => {
            console.error(`GET /api/recommendations failed: ${err}`);
        })
    }

    changeDate(newDate) {
        this.setState({ selectedDate: newDate.target.value });
    }

    changeTopic(selectedTopic) {
        this.setState({ selectedTopic });
    }

    createDay() {
        // Uncomment this when login returns user id

        //post('days', {
        //    date: this.state.selectedDate,
        //    employeeId: this.state.currentUser.id,
        //    topicId: this.state.selectedTopic.id,
        //}).then(r => r.json())
        //    .then(() => {
        //        this.props.history.push('/home');
        //    });
    }

    makeTopicOptionList() {
        const recommended = this.state.topics
            .filter(t => this.state.recommendations.find(r => r.topicId === t.id) !== undefined)
            .map(t => <option key={`topic-selection-${t.id}`} value={t.id}>{"\u2606"} {t.name}</option>);
        const other = this.state.topics
            .filter(t => this.state.recommendations.find(r => r.topicId === t.id) === undefined)
            .map(t => <option key={`topic-selection-${t.id}`} value={t.id}>{t.name}</option>);
        return [...recommended, ...other];
    }

    render() {
        return (
            <div className='day-wrapper'>
                <div className='day-holder'>
                <h2>Create new learning day</h2>
                    <form>
                        <div className='row'>
                            <label htmlFor="learn-date">Date:</label>
                        </div>
                        <div className='row'>
                        <input
                            type="date"
                            id="learn-date"
                            defaultValue={this.state.selectedDate}
                            onChange={this.changeDate}
                            min={moment().format("YYYY-MM-DD")}
                            pattern="d{4}-d{2}-d{2}"
                        />
                        </div>
                        <div className='row'>
                            <label htmlFor="topics">Topic:</label>
                        </div>
                        <div className='row'>
                        <select
                            id="topics"
                            onChange={this.changeTopic}
                        >
                            {this.makeTopicOptionList()}
                        </select>
                        </div>
                        <div className='row'>
                            <button type="submit" className="btn btn-custom" onClick={this.createDay}>Create</button>
                        </div>
                </form>
                </div>
            </div>
        )
    }
}

function mapStateToProps(state, ownProps) {
    return {
        currentUser: state.currentUser
    };
}

function mapDispatchToProps() {
    return {};
}

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(NewLearningDayPage));