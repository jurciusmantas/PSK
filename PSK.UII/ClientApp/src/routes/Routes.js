import React from 'react';
import { connect } from 'react-redux';
import { post } from '../helpers/request';
import { BrowserRouter, Route, Switch, Redirect } from 'react-router-dom';
import { getCookie } from '../helpers/cookie';
import * as currentUserActions from '../redux/actions/currentUserActions';

//Pages
import Layout from '../components/Layout/Layout';
import LoginPage from '../components/Login/LoginPage';
import HomePage from '../components/Home/HomePage';
import TopicPage from '../components/Topic/TopicPage';
import DetailedTopicPage from '../components/Topic/DetailedTopicPage';
import NotFoundPage from '../components/NotFound/NotFoundPage';
import InvitePage from '../components/Invite/InvitePage';
import RegistrationPage from '../components/Registration/RegistrationPage';

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
                { component: InvitePage, path: "/invite" },
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

        else if (this.props.currentUser)
            this.props.logout();
    }

    render(){
        const { currentUser } = this.props;

        if (!currentUser || !currentUser.token)
            return (
                <BrowserRouter basename={'MegstuKumpi'}>
                    <Switch>
                        <Route path='/' exact component={LoginPage} />
                        <Route path='/registration/:id' component={RegistrationPage} />
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
        login: (currentUser) => dispatch(currentUserActions.loginSuccess(currentUser)),
        logout: () => dispatch(currentUserActions.logout())
    }
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Routes);
