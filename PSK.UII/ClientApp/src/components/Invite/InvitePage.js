import React from "react"
import './InvitePage.css';
import { post } from '../../helpers/request'

class InvitePage extends React.Component {
    constructor() {
        super()

        this.state = {
            email: ''
        }

        this.onSubmit = this.handleSubmit.bind(this)
    }

    handleSubmit(e) {
        e.preventDefault();
        const {
            email
        } = this.state;

        if (!email) {
            return;
        }

        post('invite', {
            email: email
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {

                    this.setState({
                        email: ''
                    });
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    handleKeyPress(e) {
        if (e.key === "Enter") 
            this.handleSubmit(e);
    }

    render() {
        return (
            <form className="invite-wrapper" onSubmit={this.onSubmit}>
                <div className="invite-holder">
                <div className="row">
                    <label>Email:</label>
                    <input
                            type="email"
                            name="email"
                            value={this.state.email}
                            onChange={e => this.setState({ email: e.target.value })}
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

export default InvitePage;
