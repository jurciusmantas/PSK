import React from "react"
import './InvitePage.css';
import { post } from '../../helpers/request'
import { connect } from 'react-redux';
import { notification } from "../../helpers/notification";

class InvitePage extends React.Component {
    constructor() {
        super()

        this.state = {
            email: '',
            leaderId: null,
            submitting: false
        }

        this.onSubmit = this.handleSubmit.bind(this)
    }

    handleSubmit(e) {
        e.preventDefault();
        var leaderId = this.props.currentUser.id;

        const {
            email
        } = this.state;

        if (!email) {
            return;
        }

        this.setState({ submitting: true });

        post('invite', {
            email: email,
            leaderId: leaderId
        })
            .then(res => res.json())
            .then(res => {
                this.setState({ submitting: false });
                if (res.success) {
                    notification("Sent!");
                    this.setState({
                        email: ''
                    });
                }
                else {
                    notification('Invite could not be sent', 'error');
                }
            })
            .catch(error => console.error(error));
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
                        <button className="btn btn-custom" type="submit" disabled={this.state.submitting}>
                            {this.state.submitting ? "Submitting..." : "Submit"}
                        </button>
                    </div>
                </div>
            </form>
        )
    }
}

const mapStateToProps = (state) => ({
    currentUser: state.currentUser
});

const mapDispatchToProps = () => ({})

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(InvitePage);