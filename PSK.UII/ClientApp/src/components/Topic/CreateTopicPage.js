import React from 'react';
import './TopicPage.css';
import { post } from '../../helpers/request';
import { notification } from '../../helpers/notification';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

class TopicPage extends React.Component {
    constructor(props) {
        super(props);

        const query = new URLSearchParams(window.location.search);
        this.state = {
            name: null,
            description: null,
            parentId: parseInt(query.get("parent")),
        };
    }

    create() {
        if (!this.state.name || !this.state.description) {
            notification('Please fill in empty fields', 'error');
            return;
        }

        post('topics', {
            name: this.state.name,
            description: this.state.description,
            parentId: this.state.parentId,
        })
            .then(res => res.json())
            .then(res => {
                if (res.success){
                    notification('Topic created successfully');
                    this.props.history.push('topics');
                }
                else 
                    notification('Topic creation failed: ' + res.message, 'error');
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
                        />
                    </div>
                    <div className='row'>
                        <textarea cols="50"
                            onChange={e => this.setState({ description: e.target.value })}
                            placeholder='Topic description'
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

const mapStateToProps = (state, ownProps) => {
    return {
        currentUser: state.currentUser,
    }
}
  
  const mapDispatchToProps = (dispatch, ownProps) => {
    return {

    }
}
  
export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(TopicPage));
