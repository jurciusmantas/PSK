import React from 'react';
import { post } from '../../helpers/request';
import { connect } from 'react-redux';
import * as currentUserActions from '../../redux/actions/currentUserActions';
import 'bootstrap/dist/css/bootstrap.css';
import './LoginPage.css';
import { setCookie } from '../../helpers/cookie';

class LoginPage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            login: null,
            password: null,
        };

        this.handleKeyPress = this.handleKeyPress.bind(this);
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.login();
    }

    login() {
        const {
            login,
            password
        } = this.state;

        if (!login || !password)
            return;
        //TODO: Else - to show "no input"

        post('login', {
            login: login,
            password: password,
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    setCookie(res.data.token);
                    this.props.history.push('/home');
                    this.props.login(res.data);
                }
                //TODO: Else - to show "bad credentials"
            })
            .catch(error => console.error(error));
    }

    render() {
        return (
            <div className='login-wrapper'>
                <div className='login-holder'>
                    <h2>Login</h2>
                    <div className='row'>
                        <input
                            type='text'
                            placeholder='Login'
                            onChange={e => this.setState({ login: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>
                    <div className='row'>
                        <input
                            type='password'
                            placeholder='Password'
                            onChange={e => this.setState({ password: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>
                    <div className='row'>
                        <button
                            type="button"
                            className="btn btn-custom"
                            onClick={() => this.login()}
                        >Login</button>
                    </div>
                </div>
            </div>
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
)(LoginPage);