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
                    {this.props.userDays
                        .filter(day => day.date === (`${this.props.yearMonth}-` + (this.props.monthDay > 10 ? `${this.props.monthDay}` : `0${this.props.monthDay}`)))
                        .map(day =>
                        (<UserDay
                            key={`user-day-${day.id}`}
                            topicName={day.topicName}
                            topicId={day.topicId}
                            topicCompleted={day.completed}
                            yearMonth={this.props.yearMonth}
                            monthDay={this.props.monthDay}
                            update={() => this.props.update()}
                            monthDiff={this.props.monthDiff}
                        />)
                    )}
                    {this.props.subordinatesDays
                        .filter(day => day.date === (`${this.props.yearMonth}-` + (this.props.monthDay > 10 ? `${this.props.monthDay}` : `0${this.props.monthDay}`)))
                        .map(day => (
                        <SubordinateDay
                            key={`subordinate-day-${day.id}`}
                            topicId={day.topicId}
                            employeeId={day.employeeId}
                            topicName={day.topicName}
                            employeeName={day.employeeName}
                        />
                        ))}
                </div>
            </div>
        )
    }
}