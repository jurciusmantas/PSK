import React from "react"
import './InvitePage.css';
import { post } from '../../helpers/request'

class InvitePage extends React.Component {
    constructor() {
        super()

        this.state = {
            email: '',
            loading: false
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

        this.setState({ loading: true });

        post('invite', {
            email: email
        })
            .then(res => res.json())
            .then(res => {
                this.setState({ loading: false });
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
                    <h2>Send invite</h2>
                    <p>Send an invitation to who you want to add to your team</p>
                    <div className="row">
                        <input
                            type="email"
                            placeholder="Email"
                            value={this.state.email}
                            onChange={e => this.setState({ email: e.target.value })}
                            onKeyPress={e => this.handleKeyPress(e)}
                        />
                    </div>
                    <div className="row">
                        <button className="btn btn-custom" type="submit" disabled={this.state.loading}>
                            {this.state.loading ? "Submitting..." : "Submit"}
                        </button>
                    </div>
                </div>
            </form>
            )
    }
}

export default InvitePage;
