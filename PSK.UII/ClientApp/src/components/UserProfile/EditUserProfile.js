import React from 'react';
import { get } from '../../helpers/request'
import { Link, withRouter } from "react-router-dom";
import TreeView from 'devextreme-react/tree-view';
import '../Topic/TopicPage.css';
import Loader from '../Loader/loader';
import { notification } from '../../helpers/notification';
import { connect } from 'react-redux';


class EditUserProfile extends React.Component {
    constructor() {
        super()
        this.state = {
            name: null,
            password: null,
            confirmPassword: null,

            loadingTopic: false,
        }
        this.onSubmit = this.handleSubmit.bind(this);

    }

    componentDidMount() {
        this.state.name = this.props.currentUser.name;
    }

    handleKeyPress(e) {
        if (e.key === "Enter")
            this.handleSubmit(e);
    }

    handleSubmit(e) {
        e.preventDefault();

        const {
            name,
            password,
            confirmPassword,
        } = this.state;

        if (password != confirmPassword) {
            notification("Passwords do not match", "error");
        }
        
    }

    render() {
        return (
            <form className="topic-wrapper" onSubmit={this.onSubmit}>
                <div className="topic-holder">
                    <h2>Edit user information</h2>
                    <div className='info'>
                        <div className="row">
                            {this.state.loadingTopic
                                ? <Loader />
                                : <div>
                                    <h5>Current data:</h5>
                                    <p><b>Name: </b>{this.props.currentUser.name}</p>
                                    <p><b>Email: </b>{this.props.currentUser.email}</p>
                                </div>
                            }
                        </div>
                    </div>
                    <div className='row'>
                        <div className="newData">
                            {this.state.loadingTopic
                                ? <Loader />
                                : <div>
                                    <h5>Enter new information:</h5>
                                    
                                    <div className='row'>
                                        <input
                                            type="text"
                                            defaultValue={this.props.currentUser.name}
                                            onChange={e => this.setState({ name: e.target.value })}
                                            onKeyPress={e => this.handleKeyPress(e)}
                                            placeholder="Name"
                                            required />
                                    </div>
                                    <div className='row'>
                                        <input type="password"
                                            placeholder="Password"
                                            onChange={e => this.setState({ password: e.target.value })}
                                            onKeyPress={e => this.handleKeyPress(e)}
                                            required />
                                    </div>
                                    <div className='row'>
                                        <input type="password"
                                            placeholder="Confirm password"
                                            onChange={e => this.setState({ confirmPassword: e.target.value })}
                                            onKeyPress={e => this.handleKeyPress(e)}
                                            required />
                                    </div>
                                </div>
                            }
                        </div>
                    </div>
                    <div className="row">
                        <button className="btn btn-custom" type="submit">Submit</button>
                        <Link to="/user-profile" className="btn btn-custom">Return</Link>
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

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(EditUserProfile));