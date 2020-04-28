import React from 'react';
import { post } from '../../helpers/request'

export default class TopicPage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            name: null,
            description: null,
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
            console.log("abcd");
    }

    create() {
        const {
            name,
            description
        } = this.state;

        if (!name || !description)
            alert("please fill empty fields")

        post('topic/createtopic', {
            name: name,
            description: description,
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    alert("Added successfully")
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    render() {
        return (
            <div>
                <h2>
                    Create Topic
                </h2>
                <div>
                    <label>Name:</label>
                    <input
                        type='text'
                        onChange={e => this.setState({ name: e.target.value })}
                        onKeyPress={e => this.handleKeyPress(e)}
                    />
                </div>
                <div>
                    <label>Description:</label>
                    <textarea cols="50"
                        onChange={e => this.setState({ description: e.target.value })}
                        onKeyPress={e => this.handleKeyPress(e)}
                    />
                </div>
                <div>
                    <button
                        type="button"
                        className="btn btn-dark"
                        onClick={() => this.create()}
                    >Create</button>
                </div>
            </div>
            
        );
    }
}
