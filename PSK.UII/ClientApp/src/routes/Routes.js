import React from 'react';
import { connect } from 'react-redux';
import { post } from '../helpers/request';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { getCookie } from '../helpers/cookie';
import * as currentUserActions from '../redux/actions/currentUserActions';

import Layout from '../components/Layout/Layout';
import LoginPage from '../components/Login/LoginPage';
import HomePage from '../components/Home/HomePage';
import TopicPage from '../components/Topic/TopicPage';
import DetailedTopicPage from '../components/Topic/DetailedTopicPage';
import NotFoundPage from '../components/NotFound/NotFoundPage';

const NotFoundPageWraped = () =>
    <Layout>
        <NotFoundPage/>
    </Layout>;

class Routes extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            components: [
                { component: HomePage, path: "/home" },
                { component: DetailedTopicPage, path: "/topic/:id"},
                { component: TopicPage, path: "/topic" }
            ]
        }
    }

    componentDidMount(){
        let token = getCookie('AuthToken');
        if (token)
            post('login/login_token', token)
                .then(res => res.json())
                .then(res => {
                    if (res.success)
                        this.props.login(res.data);
                })
                .catch(error => {
                    console.log(error);
                })
    }

    render(){
        const { currentUser } = this.props;

        /* Do not show login page when logging-in with token */
        if (!currentUser.token && getCookie('AuthToken'))
            return <div/>;

        if (!currentUser || !currentUser.token)
            return (
                <BrowserRouter basename={'MegstuKumpi'}>
                    <Switch>
                        <Route path='/' exact component={LoginPage}/>
                        <Route component={NotFoundPage}/>
                    </Switch>
                </BrowserRouter>
            )

        return (
            <BrowserRouter basename={'MegstuKumpi'}>
                <Switch>
                    {   
                        this.state.components.map((comp, i) => {
                            let wrappedComponent = () =>
                                <Layout>
                                    <comp.component/>
                                </Layout>;
                            
                            return (
                                <Route component={wrappedComponent} path={comp.path} key={`route_key_${i}`}/>
                            )
                        })
                    }
                    <Route component={NotFoundPageWraped}/>
                </Switch>
            </BrowserRouter>
        )
    }
}

const mapStateToProps = (state, ownProps) => {
    return {
        currentUser: state.currentUser
    }
}

const mapDispatchToProps = (dispatch, ownProps) => {
    return {
        login: (currentUser) => dispatch(currentUserActions.loginSuccess(currentUser))
    }
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Routes);
