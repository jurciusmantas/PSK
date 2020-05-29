﻿import React from 'react';
import './TopicPage.css';
import { post } from '../../helpers/request';
import { notification } from '../../helpers/notification';

export default class TopicPage extends React.Component {
    constructor(props) {
        super(props);

        const query = new URLSearchParams(window.location.search);
        this.state = {
            name: null,
            description: null,
            parentId: query.get("parent"),
        };
    }

    componentDidMount() {
        window.addEventListener("keypress", this.handleKeyPress);
    }

    componentWillUnmount() {
        window.removeEventListener("keypress", this.handleKeyPress);
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            alert("Topic is not actually created");
    }

    create() {
        const {
            name,
            description,
            parentId
        } = this.state;

        if (!name || !description) {
            notification('Please fill in empty fields');
            return;
        }
        post('topics', {
            name: name,
            description: description,
            parentId: parentId,
        })
            .then(res => res.json())
            .then(res => {
                if (res.success)
                    alert("TOPIC POSTED SUCCESSFULLY");
                else {
                    console.warn('Topic creation failed:')
                    console.warn(res.message);
                }
            })
            .catch(error => {
                console.error(`POST topics failed:`);
                console.error(error);
            })
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
