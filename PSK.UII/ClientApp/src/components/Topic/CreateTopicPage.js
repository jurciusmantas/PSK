﻿import React from 'react';
import { post } from '../../helpers/request'
import './TopicPage.css';

export default class TopicPage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            name: null,
            description: null,
            parentId: null
        };
    }

    componentDidMount() {
        window.addEventListener("keypress", this.handleKeyPress);

        if (!isNaN(window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1))) {
            this.setState({
                parentId: parseInt(window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1))
            });
        }
    }

    componentWillUnmount() {
        window.removeEventListener("keypress", this.handleKeyPress);
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            console.log("abcd");
    }

    create() {
        const {
            name,
            description,
            parentId
        } = this.state;

        

        if (!name || !description) {
            alert("please fill empty fields")
        }
        else {
            post('topic/createtopic', {
                name: name,
                description: description,
                parentId: parentId,
            })
            .then(res => res.json())
            .then(res => {
                alert(res.message)
            })
            .catch(error => {
                console.log(error);
            })
        }   
        
    }

    render() {
        return (
            <div className="topic-wrapper">
                <div className="topic-holder">
                    <h2>Create Topic</h2>
                    <div className='row'>
                        <input
                            type='text'
                            placeholder='Topic name'
                            onChange={e => this.setState({ name: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>
                    <div className='row'>
                        <textarea cols="50"
                            onChange={e => this.setState({ description: e.target.value })}
                            placeholder='Topic description'
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>
                    <div className='row'>
                        <button
                            type="button"
                            className="btn btn-custom"
                            onClick={() => this.create()}
                        >Create</button>
                    </div>
                </div>
            </div>
        );
    }
}
