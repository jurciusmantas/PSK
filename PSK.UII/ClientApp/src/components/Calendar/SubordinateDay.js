import React from 'react';
import { Link } from 'react-router-dom';

export default class SubordinateDay extends React.Component {
    render() {
        return (
            <div className='subordinate-day'>
                <Link className='left' to={`topic?id=${this.props.topicId}`}>{this.props.topicName}</Link>
                <Link className='right' to={`employee?id=${this.props.employeeId}`}>{this.props.employeeName}</Link>
            </div>
        )
    }
}