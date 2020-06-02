import React from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import * as calendarActions from '../../redux/actions/calendarActions';

class AddNewDayButton extends React.Component{
    constructor(props){
        super(props);

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick(){
        this.props.setNewDayDate(this.props.yearMonth + '-' + this.props.monthDay);
        this.props.history.push(`add-day`);
    }

    render(){
        return (
            <FontAwesomeIcon
                className='add-day-icon'
                icon={faPlus}
                onClick={() => this.handleClick()}
            />
        )
    }
}

const mapStateToProps = () => ({ });

const mapDispatchToProps = (dispatch, _) => {
    return {
        setNewDayDate: (date) => dispatch(calendarActions.setDate(date)),
    }
}

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(AddNewDayButton));