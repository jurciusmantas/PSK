import React from 'react';
import { connect } from 'react-redux';
import Calendar from '../Calendar/Calendar';

class HomePage extends React.Component {
    constructor(props) {
        super(props);
    }

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
