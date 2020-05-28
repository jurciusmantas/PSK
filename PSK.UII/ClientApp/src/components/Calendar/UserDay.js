﻿import React from 'react';
import { Link } from 'react-router-dom';
import CompletionModal from './CompletionModal'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck } from '@fortawesome/free-solid-svg-icons';

export default class UserDay extends React.Component {
    constructor(props){
        super(props);

        this.state = {
            completionModalOpen: false,
        }
    }

    render() {
        return (
            <React.Fragment>
                <div className='user-day'>
                    <Link to={`topic?id=${this.props.topicId}`}>{this.props.topicName}</Link>
                    <FontAwesomeIcon 
                        icon={faCheck}
                        onClick={() => this.setState({ completionModalOpen: true })}
                    />
                </div>
                <CompletionModal
                    isOpen={this.state.completionModalOpen}
                    close={() => this.setState({ completionModalOpen: false })}
                    topicName={this.props.topicName}
                    topicId={this.topicId}
                    yearMonth={this.props.yearMonth}
                    monthDay={this.props.monthDay}
                />
            </React.Fragment>
        )
    }
}