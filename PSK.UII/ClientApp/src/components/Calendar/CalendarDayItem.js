import React from 'react';
import UserDay from './UserDay';
import SubordinateDay from './SubordinateDay';
import AddNewDayButton from './AddNewDayButton';

export default class CalendarDayItem extends React.Component {
    filterDays(dataSource){
        return dataSource.filter(day => day.date === (`${this.props.yearMonth}-` + (this.props.monthDay >= 10 ? `${this.props.monthDay}` : `0${this.props.monthDay}`)));
    }

    render() {
        if (this.props.skipper)
            return<div className='skipper' />;

        return (
            <div className='calendar-day-item'>
                <div>
                    {this.props.monthDay}
                    {this.filterDays(this.props.userDays).map(day =>
                            <UserDay
                                key={`user-day-${day.id}`}
                                topicName={day.topicName}
                                topicId={day.topicId}
                                topicCompleted={day.completed}
                                yearMonth={this.props.yearMonth}
                                monthDay={this.props.monthDay}
                                update={() => this.props.update()}
                                monthDiff={this.props.monthDiff}
                            />
                        )
                    }
                    {this.filterDays(this.props.subordinatesDays).map(day => 
                            <SubordinateDay
                                key={`subordinate-day-${day.id}`}
                                topicId={day.topicId}
                                employeeId={day.employeeId}
                                topicName={day.topicName}
                                employeeName={day.employeeName}
                            />
                        )
                    }
                    { this.filterDays(this.props.userDays).length === 0 && this.filterDays(this.props.subordinatesDays).length === 0 &&
                        <AddNewDayButton 
                            yearMonth={this.props.yearMonth}
                            monthDay={this.props.monthDay}
                        />
                    }
                </div>
            </div>
        )
    }
}