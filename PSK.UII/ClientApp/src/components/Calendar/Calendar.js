import React from 'react';
import moment from 'moment';
import CalendarDayItem from './CalendarDayItem';
import './Calendar.css';

import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

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
        };

        this.openCreateDayPage = this.openCreateDayPage.bind(this);
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

const mapStateToProps = (state, ownProps) => {
    return {
        currentUser: state.currentUser
    };
}

const mapDispatchToProps = (dispatch, ownProps) => ({})

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(Calendar));