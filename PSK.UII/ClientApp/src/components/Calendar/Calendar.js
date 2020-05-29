import React from 'react';
import moment from 'moment';
import CalendarDayItem from './CalendarDayItem';
import './Calendar.css';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { get } from '../../helpers/request';
import { notification } from '../../helpers/notification';

class Calendar extends React.Component {
    constructor(props) {
        super(props);

        const monthBefore = moment().subtract(1, 'month').format("YYYY-MM");
        const currentMonth = moment().format("YYYY-MM");
        const monthAfter = moment().add(1, 'month').format("YYYY-MM");

        this.state = {
            calendar: this.generateCalendar(moment()),
            monthBefore,
            currentMonth,
            monthAfter,
            monthDiff: 0,
            userDays: [],
            userDaysLoaded: false,
            subordinatesDays: [],
            subordinatesDaysLoaded: false,
        };

        this.openCreateDayPage = this.openCreateDayPage.bind(this);
    }

    componentDidMount() {
        console.warn("This thing likes running. If you notice that this message appears twice, this is not good");
        get(`days?employeeId=${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                console.log(res.data)
                if (res.success)
                    this.setState({ userDays: res.data, userDaysLoaded: true });
                else {
                    notification('Could not load your learning days', 'warning');
                    console.warn('Could not load employee days:');
                    console.warn(res.message);
                }
            })
            .catch(reason => {
                console.error(`GET days?employeeId=${this.props.currentUser.id} failed`)
                console.error(reason)
            });
        get(`employees/${this.props.currentUser.id}/subordinates`)
            .then(res => res.json())
            .then(res => {
                if (res.success)
                    for (let employee of res.data) {
                        get(`days?employeeId=${employee.id}`)
                            .then(r => r.json())
                            .then(r => {
                                if (res.success)
                                    this.setState({ subordinatesDays: this.state.subordinatesDays.concat(r.data) });
                                else {
                                    console.warn(`Failed to load employee id=${employee.id} name=${employee.name} days:`)
                                    console.warn(res.message);
                                }
                            })
                            .catch(reason => {
                                console.error(`GET days?employeeId=${this.props.currentUser.id} failed`)
                                console.error(reason)
                            });

                    }
                else {
                    notification('Could not load your subordinates days', 'warning');
                    console.warn('Could not load subordinates days:')
                    console.warn(res.message);
                }
            })
            .then(_ => { this.setState({ subordinatesDaysLoaded: true }); })
            .catch(reason => {
                console.error(`GET employees/${this.props.currentUser.id}/subordinates failed`)
                console.error(reason)
            });
    }

    generateCalendar(now) {
        let days = [];
        let skipFirsts = [];
        let addLasts = [];

        let monthStartedWith = now.startOf('month').weekday();
        if (monthStartedWith === 0)
            monthStartedWith = 7;

        let daysInMonth = now.daysInMonth();
        for (let i = 0; i < daysInMonth; i++) {
            let weekDay = now.startOf('month').add(i, 'days').weekday();
            if (weekDay === 0)
                weekDay = 7;

            if (i < 7 && weekDay < monthStartedWith)
                skipFirsts.push(weekDay);

            if (i === daysInMonth - 1)
                for (let j = weekDay + 1; j <= 7; j++)
                    addLasts.push(j);

            days.push({
                monthDay: i + 1,
                weekDay: weekDay,
            })
        };

        let res = {
            days: days,
            monthStartedWith: monthStartedWith,
            skipFirsts: skipFirsts,
            addLasts: addLasts,
        };

        return res;
    }

    changeMonth(forward = false) {
        let { monthDiff } = this.state;

        if (forward)
            monthDiff++;
        else
            monthDiff--;

        const now = moment().add(monthDiff, 'month');
        const calendar = this.generateCalendar(now);
        const monthBefore = moment().add(monthDiff, 'month').subtract(1, 'month').format('YYYY-MM');
        const currentMonth = moment().add(monthDiff, 'month').format('YYYY-MM');
        const monthAfter = moment().add(monthDiff, 'month').add(1, 'month').format('YYYY-MM');

        this.setState({
            calendar,
            monthBefore,
            currentMonth,
            monthAfter,
            monthDiff,
        })
    }

    selectDay(dayList, dateString) {
        return dayList.filter(d => d.date === dateString);
    }

    openCreateDayPage() {
        this.props.history.push(`add-day`);
    }

    render() {
        const {
            calendar,
            monthBefore,
            currentMonth,
            monthAfter
        } = this.state;

        return (
            <React.Fragment>
                <div className="calendar-container">
                    <div className='calendar-navbar'>
                        <button
                            type="button"
                            className="btn btn-custom"
                            onClick={(e) => this.changeMonth()}
                        >{'<<  ' + monthBefore}</button>
                        <b>{currentMonth}</b>
                        <button
                            type="button"
                            className="btn btn-custom"
                            onClick={() => this.changeMonth(true)}
                        >{monthAfter + '  >>'}</button>
                        <button
                            type="button"
                            className="btn btn-custom"
                            onClick={this.openCreateDayPage}
                        >Add learning day</button>
                    </div>
                    <div className='calendar-holder'>
                        {[1, 2, 3, 4, 5, 6, 7].map(index => {
                            const items = calendar.days.filter(i => i.weekDay === index);

                            return (
                                <div
                                    className='weekday-column'
                                    key={`weekday-column-${index}`}
                                >
                                    {calendar.skipFirsts.includes(items[0].weekDay) &&
                                        <CalendarDayItem
                                            key={`skipper-before-${calendar.skipFirsts.indexOf(items[0].weekDay)}`}
                                            skipper
                                        />
                                    }
                                    {items.map(i => (
                                        <CalendarDayItem
                                            key={`calendar-day-item-${i.monthDay}`}
                                            monthDay={i.monthDay}
                                            yearMonth={currentMonth}
                                            userDays={this.state.userDays}
                                            userDaysLoaded={this.state.userDaysLoaded}
                                            subordinatesDays={this.state.subordinatesDays}
                                            subordinatesDaysLoaded={this.state.subordinatesDaysLoaded}
                                        />
                                    ))}
                                    {calendar.addLasts.includes(items[0].weekDay) &&
                                        <CalendarDayItem
                                            key={`skipper-after-${calendar.addLasts.indexOf(items[0].weekDay)}`}
                                            skipper
                                        />
                                    }
                                </div>
                            )
                        })
                        }
                    </div>
                </div>
            </React.Fragment>
        )
    }
}

const mapStateToProps = (state) => ({
    currentUser: state.currentUser
});

const mapDispatchToProps = () => ({})

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(Calendar));