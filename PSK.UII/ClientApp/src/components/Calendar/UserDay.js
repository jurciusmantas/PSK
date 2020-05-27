import React from 'react';
import { Link } from 'react-router-dom';

export default class UserDay extends React.Component {
    render() {
        return (
            <div className='user-day'>
                <Link to={`topic?=${this.props.topicId}`}>{this.props.topicName}</Link>
            </div>
        )
    }
}