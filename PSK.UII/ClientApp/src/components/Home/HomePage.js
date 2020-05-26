import React from 'react';
import Calendar from '../Calendar/Calendar';
import { connect } from 'react-redux';

class HomePage extends React.Component {
    render() {
        return (
            <React.Fragment>
                <Calendar />
            </React.Fragment>
        )
    }
}
const mapStateToProps = (state, ownProps) => {
    return {
        currentUser: state.currentUser
    }
}

const mapDispatchToProps = (dispatch, ownProps) => ({});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(HomePage);
