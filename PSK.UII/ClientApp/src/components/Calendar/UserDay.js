import React from 'react';
import { Link } from 'react-router-dom';
import CompletionModal from './CompletionModal'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import moment from 'moment';

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
                    { !this.props.topicCompleted && (this.props.monthDiff < 0 || (this.props.monthDiff === 0 && moment().date() <= this.props.monthDay)) &&
                        <FontAwesomeIcon
                            icon={faCheck}
                            onClick={() => this.setState({ completionModalOpen: true })}
                        />
                    }
                </div>
                <CompletionModal
                    isOpen={this.state.completionModalOpen}
                    close={() => {
                        this.setState({ completionModalOpen: false })
                        if (this.props.update)
                            this.props.update();
                    }}
                    topicName={this.props.topicName}
                    topicId={this.props.topicId}
                    yearMonth={this.props.yearMonth}
                    monthDay={this.props.monthDay}
                />
            </React.Fragment>
        )
    }
}