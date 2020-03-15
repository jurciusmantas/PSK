import React from 'react';
import moment from 'moment';
import CalendarDayItem from './CalendarDayItem';
import './Calendar.css';

export default class Calendar extends React.Component{
    constructor(props){
        super(props);

        let monthBefore = moment().subtract(1, 'month').format("YYYY-MM");
        let currentMonth = moment().format("YYYY-MM");
        let monthAfter = moment().add(1, 'month').format("YYYY-MM");

        this.state = {
            calendar: this.generateCalendar(moment()),
            monthBefore: monthBefore,
            currentMonth: currentMonth,
            monthAfter: monthAfter,
            monthDiff: 0,
        }
    }

    generateCalendar(now){
        let days = [];
        let skipFirsts = [];
        let addLasts = [];

        let monthStartedWith = now.startOf('month').weekday();
        if (monthStartedWith === 0)
            monthStartedWith = 7;

        let daysInMonth = now.daysInMonth();
        for (let i = 0; i < daysInMonth; i++){
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

    changeMonth(forward = false){
        let { monthDiff } = this.state;

        if (forward)
            monthDiff++;
        else
            monthDiff--;

        let now = moment().add(monthDiff, 'month');
        let calendar = this.generateCalendar(now);
        let monthBefore = moment().add(monthDiff, 'month').subtract(1, 'month').format('YYYY-MM');
        let currentMonth = moment().add(monthDiff, 'month').format('YYYY-MM');
        let monthAfter = moment().add(monthDiff, 'month').add(1, 'month').format('YYYY-MM');

        this.setState({
            calendar: calendar,
            monthBefore: monthBefore,
            currentMonth: currentMonth,
            monthAfter: monthAfter,
            monthDiff: monthDiff,
        })
    }

    render(){
        let {
            calendar,
            monthBefore,
            currentMonth,
            monthAfter
        } = this.state;

        return(
            <React.Fragment>
                <div className='calendar-navbar'>
                    <button 
                        type="button" 
                        className="btn btn-dark"
                        onClick={(e) => this.changeMonth()}
                    >{'<<  ' + monthBefore}</button>
                    <b>{currentMonth}</b>
                    <button 
                        type="button"   
                        className="btn btn-dark"
                        onClick={(e) => this.changeMonth(true)}
                    >{monthAfter + '  >>'}</button>
                </div>
                <div className='calendar-holder'>
                    { [1,2,3,4,5,6,7].map(index => {
                        let items = calendar.days.filter(i => i.weekDay === index);

                        return (
                            <div 
                                className='weekday-column'
                                key={`weekday-column-${index}`}    
                            >
                                { calendar.skipFirsts.includes(items[0].weekDay) &&
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
                                { calendar.addLasts.includes(items[0].weekDay) &&
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
            </React.Fragment>
        )
    }
}