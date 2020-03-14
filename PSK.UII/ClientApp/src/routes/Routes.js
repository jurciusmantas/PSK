import React from 'react';
import { connect } from 'react-redux';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import Layout from '../components/Layout/Layout';
import LoginPage from '../components/Login/LoginPage';
import HomePage from '../components/Home/HomePage';
import NotFoundPage from '../components/NotFound/NotFoundPage';

class Routes extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            components: [
                { component: HomePage, path: "/home" },
            ]
        }
    }

    render(){
        const { currentUser } = this.props;

        //TODO: change .login to .token later?
        if (!currentUser || !currentUser.login)
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
                    <Route component={NotFoundPage}/>
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

    }
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Routes);
