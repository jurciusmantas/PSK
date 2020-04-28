import React from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

class CalendarDayItem extends React.Component {
    constructor(props) {
        super(props);
        this.hoverOn = this.hoverOn.bind(this);
        this.hoverOff = this.hoverOff.bind(this);
        this.openAddDay = this.openAddDay.bind(this);
        this.state = { showAddDay: false };
    }

    hoverOn() {
        const now = new Date();
        const elementDate = new Date(`${this.props.currentMonth}-${this.props.monthDay}`);
        if (now < elementDate)
            this.setState(s => ({ showAddDay: true }));
    }

    hoverOff() {
        this.setState(s => ({ showAddDay: false }));
    }

    openAddDay() {
        this.props.history.push(`/addDay?date=${this.props.currentMonth}-` + (this.props.monthDay < 10 ? "0" + this.props.monthDay : this.props.monthDay));
    }

    render() {
        if (this.props.skipper)
            return (
                <div className='skipper' />
            );

        return (
            <div className='calendar-day-item'
                onMouseEnter={this.hoverOn}
                onMouseLeave={this.hoverOff}
            >
                <div>
                    {this.props.monthDay}
                </div>
                <button
                    className="add-day-btn"
                    style={this.state.showAddDay ? {} : { display: 'none' }}
                    onClick={() => this.openAddDay()}
                >Image of +</button>
            </div>
        )
    }
}

const mapStateToProps = (state, ownProps) => {
    return {
        currentUser: state.currentUser
    };
}

const mapDispatchToProps = (dispatch, ownProps) => ({})

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(CalendarDayItem));