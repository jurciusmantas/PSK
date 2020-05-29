import React from "react"
import './RegistrationPage.css';
import { post } from '../../helpers/request'
import { get } from '../../helpers/request'
import { notification } from "../../helpers/notification";
import Loader from '../Loader/loader';

class RegistrationPage extends React.Component {
    constructor() {
        super()

        this.state = {
            firstName: '',
            lastName: '',
            password: '',
            repeatedPassword: '',
            email: '',
            token: '',
            loading: true
        }

        this.onSubmit = this.handleSubmit.bind(this)
    }

    handleSubmit(e) {
        e.preventDefault();

        const {
            firstName,
            lastName,
            password,
            repeatedPassword,
            email,
        } = this.state;

        if (password !== repeatedPassword) {
            notification('Password does not match repeated password', 'error');
            return;
        }
        post('registration', {
            fullName: firstName + " " + lastName,
            password: password,
            email: email,
            token: window.location.pathname.split('/').pop()
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.props.history.push('/');
                }
                else {
                    console.warn('Registration failed:');
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error('POST registration failed:')
                console.error(error);
            })
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.handleSubmit(e);
    }

    componentDidMount = () => {
        var token = window.location.pathname.split('/').pop();

        get('registration/' + token)
            .then(res => res.json())
            .then(res => {
                this.setState({ loading: false })
                if (res.success) {
                    this.setState({ email: res.data });
                }
                else {
                    notification("Registration not found", 'error');
                    console.warn('Could not get registration')
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error(`GET registration/${token} failed:`)
                console.error(error);
            })
    }

    render() {
        if (this.state.email === '' && !this.state.loading) {
            return (
                <div className="error-container">
                    <div className="error-wrapper">
                        <h2>uh-oh</h2>
                        <hr />
                        <h3>Token used or doesn't exist</h3>
                    </div>
                </div>
            )
        }
        if (this.state.loading) {
            return <Loader/>
        }

        return (
            <form className="registration-wrapper" onSubmit={this.onSubmit}>
                <div className="registration-holder">
                    <h2>Registration</h2>
                    <div className="row">
                        <input value={this.state.email} disabled />
                    </div>
                    <div className="row">
                        <input
                            type="text"
                            name="firstName"
                            placeholder="First name"
                            value={this.state.firstName}
                            onChange={e => this.setState({ firstName: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)} />
                    </div>
                    <div className="row">
                        <input
                            type="text"
                            name="lastName"
                            placeholder="Last name"
                            value={this.state.lastName}
                            onChange={e => this.setState({ lastName: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)} />
                    </div>
                    <div className="row">
                        <input
                            type="password"
                            name="password"
                            placeholder="Password"
                            value={this.state.password}
                            onChange={e => this.setState({ password: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)} />
                    </div>
                    <div className="row">
                        <input
                            type="password"
                            name="repeatedPassword"
                            placeholder="Confirm password"
                            value={this.state.repeatedPassword}
                            onChange={e => this.setState({ repeatedPassword: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)} />
                    </div>
                    <div className="row">
                        <button className="btn btn-custom" type="submit">Submit</button>
                    </div>
                </div>
            </form>
        )
    }
}

export default RegistrationPage;
