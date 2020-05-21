import React from 'react';
import { post } from '../../helpers/request';

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
            console.log("abcd");
    }

    create() {
        const {
            name,
            description,
            parentId
        } = this.state;

        if (!name || !description) {
            alert("please fill empty fields");
        }
        else {
            post('topics', {
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
            <div>
                <h2>Create Topic</h2>
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
                        onClick={this.create}
                    >Create</button>
                </div>
            </div>

        );
    }
}
