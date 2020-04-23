import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import queryString from 'query-string';
import { get, post } from '../../helpers/request';

import './NewLearningDayPage.css';
import * as moment from 'moment';

class NewLearningDayPage extends React.Component {
    constructor(props) {
        super(props);
        const queryArgs = queryString.parse(this.props.location.search);
        this.state = {
            selectedDate: queryArgs.date,
            topics: [],
            selectedTopic: {},
        };
        this.changeDate = this.changeDate.bind(this);
        this.changeTopic = this.changeTopic.bind(this);
        this.createDay = this.createDay.bind(this);
    }

    componentDidMount() {
        get("topics").then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ topics: res.data });
        }).catch(err => {
            console.error(`GET failed: ${err}`);
            this.setState({ topics: [] });
        });
    }

    changeDate(newDate) {
        this.setState({ selectedDate: newDate });
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

    render() {
        return (
            <div>
                <h1>Create new learning day</h1>
                <form>
                    <div>
                        <label htmlFor="learn-date">Date:</label>
                        <input
                            type="date"
                            id="learn-date"
                            defaultValue={this.state.selectedDate}
                            onChange={this.changeDate}
                            min={moment().format("YYYY-MM-DD")}
                            pattern="d{4}-d{2}-d{2}"
                        />
                    </div>
                    <div>
                        <label htmlFor="topics">Topic:</label>
                        <select
                            id="topics"
                            onChange={this.changeTopic}
                        >
                            {
                                this.state.topics.map(t => {
                                    const { name, id } = t;
                                    return <option key={`topic-selection-${id}`} value={id}>{name}</option>
                                })
                            }
                        </select>
                    </div>
                    <button type="submit" className="btn btn-dark" onClick={this.createDay}>Create</button>
                </form>
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