import React from 'react';

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
                </div>
            </div>
        )
    }
}