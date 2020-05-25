import React from 'react';

export default class UserDay extends React.Component {
    render() {
        return (
            <div className='user-day'
                //onClick={() => this.props.history.push(`topic?=${this.props.topicId}`)}
                onClick={() => console.log(`topic?=${this.props.topicId}`)}
            >
                {this.props.topicName}
            </div>
        )
    }
}