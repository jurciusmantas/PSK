import React from 'react';
import { connect } from 'react-redux';
import Calendar from '../Calendar/Calendar';

export default class HomePage extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return (
            <React.Fragment>
                <Calendar/>
            </React.Fragment>
        )
    }
}