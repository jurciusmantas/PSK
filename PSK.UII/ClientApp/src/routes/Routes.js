import React from 'react';
import { connect } from 'react-redux';
import { post } from '../helpers/request';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { getCookie } from '../helpers/cookie';
import * as currentUserActions from '../redux/actions/currentUserActions';

//Pages
import Layout from '../components/Layout/Layout';
import LoginPage from '../components/Login/LoginPage';
import HomePage from '../components/Home/HomePage';
import TopicPage from '../components/Topic/TopicPage';
import NotFoundPage from '../components/NotFound/NotFoundPage';
import InvitePage from '../components/Invite/InvitePage';
import RecommendationsPage from '../components/Recommendations/RecommendationsPage';
import AddRecommendationPage from '../components/Recommendations/AddRecommendationPage';
import EditRecommendationsPage from '../components/Recommendations/EditRecommendationPage';
import RegistrationPage from '../components/Registration/RegistrationPage';
import CreateTopicPage from '../components/Topic/CreateTopicPage';
import DetailedTopicPage from '../components/Topic/DetailedTopicPage';
import NewLearningDayPage from '../components/LearningDay/NewLearningDayPage';
import RestrictionsPage from '../components/Restrictions/RestrictionsPage';
import UserProfile from '../components/UserProfile/UserProfile';

const NotFoundPageWraped = () =>
    <Layout>
        <NotFoundPage />
    </Layout>;

class Routes extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            components: [
                { component: HomePage, path: "/home" },
                { component: InvitePage, path: "/invite" },
                { component: DetailedTopicPage, path: "/topic" },
                { component: TopicPage, path: "/topics" },
                { component: RecommendationsPage, path: "/recommendations" },
                { component: AddRecommendationPage, path: "/add-recommendation" },
                { component: EditRecommendationsPage, path: "/edit-recommendation" },
                { component: CreateTopicPage, path: "/add-topic" },
                { component: NewLearningDayPage, path: "/add-day" },
                { component: RestrictionsPage, path: "/restrictions"},
                { component: UserProfile, path: "/user-profile" },
            ]
        }
    }

    componentDidMount() {
        const token = getCookie('AuthToken');
        if (token)
            post('login?token=true', { token })
                .then(res => res.json())
                .then(res => {
                    if (res.success) {
                        this.props.login(res.data);
                    }
                })
                .catch(error => {
                    console.error('POST login?token=true failed:');
                    console.error(error);
                })

        else if (this.props.currentUser)
            this.props.logout();
    }

    render() {
        const { currentUser } = this.props;
        if (!currentUser || !currentUser.token)
            return (
                <BrowserRouter basename={'MegstuKumpi'}>
                    <Switch>
                        <Route path='/' exact component={LoginPage} />
                        <Route path='/registration/:id' component={RegistrationPage} />
                        <Route component={NotFoundPage} />
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
                                    <comp.component />
                                </Layout>;

                            return (
                                <Route component={wrappedComponent} path={comp.path} key={`route_key_${i}`} />
                            )
                        })
                    }
                    <Route component={NotFoundPageWraped} />
                </Switch>
            </BrowserRouter>
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
