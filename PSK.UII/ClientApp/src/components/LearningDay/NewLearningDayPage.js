import React from 'react';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import moment from 'moment';
import { get, post } from '../../helpers/request';
import { notification } from '../../helpers/notification';
import Loader from '../Loader/loader';
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
            loading: false,
        };
        
        this.changeTopic = this.changeTopic.bind(this);
        this.createDay = this.createDay.bind(this);
        this.makeTopicOptionList = this.makeTopicOptionList.bind(this);
    }

    componentDidMount() {
        get("topics").then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ topics: res.data, selectedTopicId: res.data[0].id });
            else {
                notification("Cannot load topics :(","error")
                console.warn("Cannot load topics")
                console.warn(res.message)
            }
        }).catch(err => {
            console.error(`GET /api/topics failed:`);
            console.error(err)
            this.setState({ topics: [] });
        });
        get(`recommendations?receiverId=${this.props.currentUser.id}`).then(res => res.json()).then(res => {
            if (res.success)
                this.setState({ recommendations: res.data });
            else {
                console.warn(`Cannot load recommendations for receiver id=${this.props.currentUser.id}:`)
                console.warn(res.message)
            }
        }).catch(err => {
            console.error(`GET /api/recommendations failed:`);
            console.error(err)
        })
        get(`restrictions/${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ restriction: res.data })
                }
                else {
                    notification("Cannot load restrictions", "error")
                    console.warn("Cannot load restrictions")
                    console.warn(res.message)
                }
            })
            .catch(err => {
                console.error(`GET /api/restrictions failed:`);
                console.error(err)
            })
        get(`days?employeeId=${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ userDays: res.data });
                }
                else {
                    notification("Cannot load your learning days :(", "error")
                    console.warn("Cannot load employee days")
                    console.warn(res.message)
                }
            })
            .catch(reason => {
                console.error(`GET days?employeeId=${this.props.currentUser.id} failed`)
                console.error(reason)
            });
    }


    checkIfValidForRestriction(newDate) {
        if (this.state.restriction === null) {
            return true;
        }
        const dateObject = new Date(newDate)
        var monthCounter = 1;
        var yearCounter = 1;
        var quarterCounter = 1;

        var quarter = this.getQuarter(dateObject)

        for (let d of this.state.userDays) {
            const { date } = d
            const dObj = new Date(date)
            if (dObj.getMonth() === dateObject.getMonth())
                monthCounter++;

            if (dObj.getFullYear() === dateObject.getFullYear())
                yearCounter++;

            if (this.getQuarter(dObj) === quarter)
                quarterCounter++;
        }

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
        if (this.state.userDays === null) {
            return true;
        }

        if (this.state.restriction === null) {
            return true;
        }

        const parsed = Date.parse(newDate)
        const days = this.state.userDays.map((d) => Date.parse(d.date))
        days.push(parsed);
        days.sort();

        let consecutiveDays = 1;

        for (var i = 0; i < days.length - 1; i++) {
            if (days[i + 1] - days[i] === 86400000) {
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

    changeTopic(e) {
        this.setState({ selectedTopicId: parseInt(e.target.value) });
    }

    createDay() {
        if (!this.checkIfValidForRestriction(this.state.selectedDate) ||
            !this.checkConsecutiveDays(this.state.selectedDate)) {
            return;
        }

        this.setState({ loading: true });
        post('days', {
            date: this.state.selectedDate,
            employeeId: this.props.currentUser.id,
            topicId: this.state.selectedTopicId,
        })
            .then(res => res.json())
            .then(res => {
                if (res.success){
                    notification('Day successfuly added');
                    this.props.history.push('/home');
                }
                else {
                    notification('Error adding day - ' + res.message, 'error');
                    this.setState({ loading: false });
                }        
            })
            .catch(reason => {
                this.setState({ loading: false });
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
        const { 
            selectedDate,
            loading
        } = this.state;

        return (
            <div className='day-wrapper'>
                <div className='day-holder'>
                    <h2>Create new learning day</h2>
                    { loading &&
                        <Loader />
                    }
                    { !loading &&
                        <form>
                            <div className='row'>
                                <label htmlFor="learn-date">Date:</label>
                            </div>
                            <div className='row'>
                                <input
                                    type="date"
                                    id="learn-date"
                                    defaultValue={selectedDate}
                                    onChange={e => this.setState({ selectedDate: e.target.value })}
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
                                <button type="button" className="btn btn-custom" onClick={this.createDay}>Create</button>
                            </div>
                        </form>
                    }
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