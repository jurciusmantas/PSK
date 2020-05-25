import React from 'react';
import UserDay from './UserDay';
import SubordinateDay from './SubordinateDay';

export default class CalendarDayItem extends React.Component {
    render() {
        if (this.props.skipper)
            return (
                <div className='skipper' />
            );

        return (
            <div className='calendar-day-item'>
                <div>
                    {this.props.monthDay}
                    {this.props.userDays.map(day =>
                        (<UserDay
                            key={`user-day-${day.id}`}
                            topicName={`topic name: ${day.topicName}`}
                            topicId={day.topicId}
                        />)
                    )}
                    {this.props.subordinatesDays.map(day => (
                        <SubordinateDay
                            key={`subordinate-day-${day.id}`}
                            topicId={day.topicId}
                            employeeId={day.employeeId}
                            topicName={day.topicName ? day.topicName : '???'}
                            employeeName={day.employeeName ? day.employeeName : '???'}
                        />
                        ))}
                </div>
            </div>
        )
    }
}