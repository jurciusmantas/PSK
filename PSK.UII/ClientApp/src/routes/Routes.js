import React from 'react';
import { connect } from 'react-redux';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import LoginPage from '../components/Login/LoginPage';

class Routes extends React.Component{
    constructor(props){
        super(props);
    }

    login = () => <LoginPage/>;


    render(){
        const { currentUser } = this.props;

        if (!currentUser)
            return (
                <BrowserRouter basename={'MegstuKumpi'}>
                    <Switch>
                        <Route component={this.login} path='/' />
                    </Switch>
                </BrowserRouter>
            )
    }
}

const mapStateToProps = (state, ownProps) => {
    return {
        //currentUser: state.currentUser
    }
}

const mapDispatchToProps = (dispatch, ownProps) => {
    return {

    }
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Routes);
