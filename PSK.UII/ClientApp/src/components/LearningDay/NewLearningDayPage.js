import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import moment from 'moment';
import { get, post } from '../../helpers/request';
import { notification } from '../../helpers/notification';

import './NewLearningDayPage.css';

class NewLearningDayPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedDate: moment().format("YYYY-MM-DD"),
            topics: [],
            selectedTopicId: 0,
            recommendations: [],
            restriction: null,
            userDays: null,
        };
        this.changeDate = this.changeDate.bind(this);
        this.changeTopic = this.changeTopic.bind(this);
        this.createDay = this.createDay.bind(this);
        this.makeTopicOptionList = this.makeTopicOptionList.bind(this);
    }

    componentDidMount() {
        get("topics").then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ topics: res.data, selectedTopicId: res.data[0].id });
        }).catch(err => {
            console.error(`GET /api/topics failed: ${err}`);
            this.setState({ topics: [] });
        });
        get(`recommendations?receiverId=${this.props.currentUser.id}`).then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ recommendations: res.data });
        }).catch(err => {
            console.error(`GET /api/recommendations failed: ${err}`);
        })
        get(`restrictions/${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({
                        restriction: res.data
                    })
                }
            })
            .catch(err => {
                console.error(`GET /api/restrictions failed: ${err}`);
            })
        get(`days?employeeId=${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ userDays: res.data });
                }
            })
            .catch(reason => {
                console.error(`GET days?employeeId=${this.props.currentUser.id} failed`)
            });
    }


    checkIfValidForRestriction(newDate) {

        if (this.state.restriction == null)
            return;

        var monthCounter = 1;
        var yearCounter = 1;
        var quarterCounter = 1;

        var quarter = this.getQuarter(newDate)

        this.state.userDays.map((dat) => {
            const { date } = dat
            
            var d = new Date(date);

            if (d.getMonth() == newDate.getMonth())
                monthCounter++;

            if (d.getFullYear() == newDate.getFullYear())
                yearCounter++;

            if (this.getQuarter(d) == quarter)
                quarterCounter++;
        })

        if (monthCounter > this.state.restriction.maxDaysPerMonth) {
            notification('Could not add new learning day. Maximum days in this month reached', 'error');
            return false;
        }

        if (quarterCounter > this.state.restriction.maxDaysPerQuarter) {
            notification('Could not add new learning day. Maximum days in this quarter reached', 'error');
            return false;
        }

        if (yearCounter > this.state.restriction.maxDaysPerYear) {
            notification('Could not add new learning day. Maximum days in this year reached', 'error');
            return false;
        }

        return true;
    }

    getQuarter(date) {
        return Math.floor((date.getMonth() + 3) / 3);
    }

    checkConsecutiveDays(newDate) {

        if (this.state.userDays == null)
            return;

        var days = []
        this.state.userDays.map((d) => {
            const { date } = d
            days.push(Date.parse(date));
            
        })
        days.push(Date.parse(newDate));
        days.sort();

        var consecutiveDays = 1;

        for (var i = 0; i < days.length - 1; i++) {
            if (days[i + 1] - days[i] == 86400000) {
                consecutiveDays++;
                if (consecutiveDays > this.state.restriction.consecutiveDays) {
                    notification('Cannot add more consecutive dates :(', 'error');
                    return false;
                }
            } else {
                consecutiveDays = 1;
            }
        }
        return true;
    }

    changeDate(newDate) {

        if (!this.checkIfValidForRestriction(new Date(newDate)) ||
            !this.checkConsecutiveDays(new Date(newDate).toLocaleDateString())) {

            return;
        }

        this.setState({ selectedDate: newDate.target.value });
    }

    changeTopic(e) {
        this.setState({ selectedTopicId: parseInt(e.target.value) });
    }

    createDay() {

        if (!this.checkIfValidForRestriction(new Date(this.state.selectedDate)) ||
            !this.checkConsecutiveDays(new Date(this.state.selectedDate).toLocaleDateString())) {

            return;
        }
               
        post('days', {
            date: this.state.selectedDate,
            employeeId: this.props.currentUser.id,
            topicId: this.state.selectedTopicId,
        })
            .then(() => {
                this.props.history.push('/home');
            })
            .catch(reason => {
                console.error(`POST days failed`);
                console.error(reason);
            });

        
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