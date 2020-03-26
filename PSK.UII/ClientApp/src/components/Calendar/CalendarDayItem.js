import React from 'react';

export default class Calendar extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        if (this.props.skipper)
            return (
                <div className='skipper'/>
            );

        return(
            <div className='calendar-day-item'>
                <div>
                    {this.props.monthDay}
                </div>
            </div>
        )
    }
}