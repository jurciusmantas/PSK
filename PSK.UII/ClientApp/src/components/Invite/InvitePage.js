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

    handleSubmit() {
        post('invite', {
            email: this.state.email
        })
            .catch(error => {
                console.log(error);
            })
    }

    onChange = e => this.setState({
        data: {...this.state.data, [e.target.name]: e.target.value }
    })

    render() {
        return (
            <form className="invite-wrapper" onSubmit={this.onSubmit}>
                <div className="invite-holder">
                <div className="row">
                    <label>Email:</label>
                    <input
                        type="email"
                        onChange={e => this.setState({ email: e.target.value })}
                    />
                </div>
                <div className="row">
                    <button className="btn btn-dark" type="button"
                            onClick={() => this.handleSubmit()}> Submit</button>
                    </div>
                </div>
            </form>
            )
    }
}

export default InvitePage;
