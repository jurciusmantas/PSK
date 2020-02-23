import React from 'react';
import { post } from '../../helpers/request'
import { connect } from 'react-redux';
import 'bootstrap/dist/css/bootstrap.css';
import './LoginPage.css';

class LoginPage extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            login: null,
            password: null,
        };
    }

    login(){
        const {
            login,
            password
        } = this.state;

        if (!login || !password)
            return;

        post('login/login', {
            login: login,
            password: password,
        })
            .then(res => res.json())
            .then(res => {
                if (res.success)
                    this.props.history.push('/home');
            })
            .catch(error => {
                console.log(error);
            })
    }

    render(){
        return(
            <div className='login-wrapper'>
                <div className='login-holder'>
                    <div className='row'>
                        <label>Login:</label>
                        <input 
                            type='text'
                            onChange={e => this.setState({ login: e.target.value })}    
                        />
                    </div>
                    <div className='row'>
                        <label>Password:</label>
                        <input 
                            type='password'
                            onChange={e => this.setState({ password: e.target.value })}    
                        />
                    </div>
                    <div className='row'>
                        <button
                            type="button" 
                            className="btn btn-dark"
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
)(LoginPage);