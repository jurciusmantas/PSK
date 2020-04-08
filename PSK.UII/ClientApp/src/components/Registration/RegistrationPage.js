import React from "react"
import './RegistrationPage.css';

import { post } from '../../helpers/request'
import { get } from '../../helpers/request'

class RegistrationPage extends React.Component {
    constructor() {
        super()

        this.state = {
            firstName: '',
            lastName: '',
            password: '',
            repeatedPassword: '',
            email: '',
            token: ''
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
            alert("Passwords don't match");
        }
        else {
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
                        console.log(res.message);
                    }
                })
                .catch(error => {
                    console.log(error);
                })
        }
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.handleSubmit(e);
    }

    componentDidMount = () => {
        var token = window.location.pathname.split('/').pop();

        get('registration/' + token )
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    this.setState({ email: res.data });
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    render() {
        const { email } = this.state;

        if (this.state.email === "") {
            return <h6> Token used or doesn't exist</h6>
        }

        return (
            <form className="invite-wrapper" id="asd" onSubmit={this.onSubmit}>
                <h3>Hello, {email}</h3>
                
                <div className="invite-holder">
                    <div className="row">
                        <label>First name: </label>
                        <input
                            type="text"
                            name="firstName"
                            value={this.state.firstName}
                            onChange={e => this.setState({ firstName: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>

                    <div className="row">
                        <label>Last name:</label>
                        <input
                            type="text"
                            name="lastName"
                            value={this.state.lastName}
                            onChange={e => this.setState({ lastName: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>

                    <div className="row">
                        <label>Password:</label>
                        <input
                            type="password"
                            name="password"
                            value={this.state.password}
                            onChange={e => this.setState({ password: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>

                    <div className="row">
                        <label>Repeat password:</label>
                        <input
                            type="password"
                            name="repeatedPassword"
                            value={this.state.repeatedPassword}
                            onChange={e => this.setState({ repeatedPassword: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>

                    <div className="row">
                        <button className="btn btn-dark" type="submit">Submit</button>
                    </div>
                </div>
            </form>
        )
    }
}

export default RegistrationPage;
