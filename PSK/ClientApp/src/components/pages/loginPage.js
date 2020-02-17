import React from 'react';
import '../../assets/_main.scss';

export default class LoginPage extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return(
            <div className='login-holder'>
                <div className='row'>
                    <label>Login:</label>
                    <input type='text' id='login-input'/>
                </div>
                <div className='row'>
                    <label>Password:</label>
                    <input type='password' id='password-input'/>
                </div>
            </div>
        )
    }
}