import React from 'react';
import moment from 'moment';
import CalendarDayItem from './CalendarDayItem';
import './Calendar.css';

export default class Calendar extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            calendar: this.generateCalendar(),
        }
    }

    generateCalendar(){
        let now = moment();
        let days = [];
        let skipFirsts = [];
        let addLasts = [];

        let monthStartedWith = moment().startOf('month').weekday();
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

    render(){
        let {
            calendar,
        } = this.state;

        return(
            <div className='calendar-holder'>
                { [1,2,3,4,5,6,7].map(index => {
                    let items = calendar.days.filter(i => i.weekDay === index);

                    return (
                        <div className='weekday-column'>
                            { calendar.skipFirsts.includes(items[0].weekDay) &&
                                <CalendarDayItem skipper/>
                            }
                            {items.map(i => (
                                <CalendarDayItem
                                    monthDay={i.monthDay}
                                />
                            ))}
                            { calendar.addLasts.includes(items[0].weekDay) &&
                                <CalendarDayItem skipper/>
                            }
                        </div>
                    )
                })
                }
            </div>
        )
    }
}