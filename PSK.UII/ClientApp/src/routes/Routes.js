import React from 'react';
import { connect } from 'react-redux';
import { post } from '../helpers/request';
import { Router, Route, Switch } from 'react-router-dom';
import { getCookie, removeCookie } from '../helpers/cookie';
import * as currentUserActions from '../redux/actions/currentUserActions';
import { createBrowserHistory } from 'history';

//Pages
import Layout from '../components/Layout/Layout';
import LoginPage from '../components/Login/LoginPage';
import Calendar from '../components/Calendar/Calendar';
import TopicPage from '../components/Topic/TopicPage';
import NotFoundPage from '../components/NotFound/NotFoundPage';
import InvitePage from '../components/Invite/InvitePage';
import RecommendationsPage from '../components/Recommendations/RecommendationsPage';
import AddRecommendationPage from '../components/Recommendations/AddRecommendationPage';
import EditRecommendationsPage from '../components/Recommendations/EditRecommendationPage';
import RegistrationPage from '../components/Registration/RegistrationPage';
import CreateTopicPage from '../components/Topic/CreateTopicPage';
import DetailedTopicPage from '../components/Topic/DetailedTopicPage';
import EditTopicPage from '../components/Topic/EditTopicPage';
import NewLearningDayPage from '../components/LearningDay/NewLearningDayPage';
import RestrictionsPage from '../components/Restrictions/RestrictionsPage';
import UserProfile from '../components/UserProfile/UserProfile';
import Loader from '../components/Loader/loader';

const NotFoundPageWraped = () =>
    <Layout>
        <NotFoundPage />
    </Layout>;

const history = createBrowserHistory({
    basename: 'MegstuKumpi'
});

class Routes extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            components: [
                { component: Calendar, path: "/home" },
                { component: InvitePage, path: "/invite" },
                { component: DetailedTopicPage, path: "/topic" },
                { component: TopicPage, path: "/topics" },
                { component: EditTopicPage, path: "/edit-topic" },
                { component: RecommendationsPage, path: "/recommendations" },
                { component: AddRecommendationPage, path: "/add-recommendation" },
                { component: EditRecommendationsPage, path: "/edit-recommendation" },
                { component: CreateTopicPage, path: "/add-topic" },
                { component: NewLearningDayPage, path: "/add-day" },
                { component: RestrictionsPage, path: "/restrictions" },
                { component: UserProfile, path: "/user-profile" },
            ],
            loading: true
        }
    }

    componentDidMount() {
        const token = getCookie('AuthToken');
        if (token) {
            post('login?token=true', { token })
                .then(res => res.json())
                .then(res => {
                    if (res.success) {
                        this.props.login(res.data);
                        if (history.location.pathname === "/")
                            history.push('/home');
                        this.setState({ loading: false });
                    }
                    else {
                        removeCookie('AuthToken');
                        window.location.reload();
                    }
                })
                .catch(error => {
                    console.error('POST login?token=true failed:');
                    console.error(error);
                })
        }

        else if (this.props.currentUser) {
            this.props.logout();
            this.setState({ loading: false });
        }
    }

    render() {
        if (this.state.loading)
            return <Loader />

        const { currentUser } = this.props;
        if (!currentUser || !currentUser.token)
            return (
                <Router history={history}>
                    <Switch>
                        <Route path='/' exact component={LoginPage} />
                        <Route path='/registration/:id' component={RegistrationPage} />
                        <Route component={NotFoundPage} />
                    </Switch>
                </Router>
            )

        return (
            <Router history={history}>
                <Switch>
                    {
                        this.state.components.map((comp, i) => {
                            let wrappedComponent = () =>
                                <Layout>
                                    <comp.component />
                                </Layout>;

                            return (
                                <Route component={wrappedComponent} path={comp.path} key={`route_key_${i}`} />
                            )
                        })
                    }
                    <Route component={NotFoundPageWraped} />
                </Switch>
            </Router>
        )
    }
}

const mapStateToProps = (state, _) => {
    return {
        currentUser: state.currentUser
    }
}

const mapDispatchToProps = (dispatch, _) => {
    return {
        login: (currentUser) => dispatch(currentUserActions.loginSuccess(currentUser)),
        logout: () => dispatch(currentUserActions.logout())
    }
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Routes);
