import React from 'react';
import { post } from '../../helpers/request';
import { connect } from 'react-redux';
import * as currentUserActions from '../../redux/actions/currentUserActions';
import 'bootstrap/dist/css/bootstrap.css';
import './LoginPage.css';
import { setCookie } from '../../helpers/cookie';
import { notification } from '../../helpers/notification';

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

        if (!login || !password) {
            notification('Please fill in username and password', 'error');
            return;
        }

        post('login', {
            login: login,
            password: password,
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    setCookie(res.data.token, res.data.expiredAt);
                    this.props.login(res.data);
                    this.props.history.push('/home');                  
                }
                else {
                    notification('Wrong username or password', 'error');
                    console.warn('Wrong username or password');
                    console.warn(res.message)
                }
            })
            .catch(error => {
                console.error(`POST /api/login failed:`);
                console.error(error);
            });
    }

    render() {
        return (
            <div className='login-wrapper'>
                <div className='login-holder'>
                    <h2>Login</h2>
                    <div className='row'>
                        <input
                            type='text'
                            placeholder='Email'
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