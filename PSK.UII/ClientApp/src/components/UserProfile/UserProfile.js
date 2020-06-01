import React from 'react';
import { 
    Form, 
    Row,
    Col,
    Label,
    Button,
} from 'reactstrap';
import { connect } from 'react-redux';
import './UserProfile.css';
import { get } from '../../helpers/request';
import { notification } from '../../helpers/notification';
import Loader from '../Loader/loader';
import TopicPage from '../Topic/TopicPage';
import '../Topic/TopicPage.css';
import { Link } from 'react-router-dom';

class UserProfile extends React.Component{
    constructor(props){
        super(props);
        const queryParams = new URLSearchParams(window.location.search);
        
        this.state = {
            loading: true,
            profile: null,
            buttons: ['Subordinates', 'Topics'],
            activeButton: 'Subordinates',
            userId: queryParams.get("id"),
          name  : null,
            email: null

        }
        this.updateProfile = this.updateProfile.bind(this)
    }

    componentDidMount() {
        if (this.state.userId == null) {
            this.state.userId = this.props.currentUser.id;
            this.state.name = this.props.currentUser.name;
            this.state.email = this.props.currentUser.email;
        } else {
            get(`employees/${this.state.userId}`)
                .then(res => res.json())
                .then(res => {

                    if (res.success) {
                        this.state.name = res.data.name;
                        this.state.email = res.data.email;
                    }

                })
                .catch(error => {
                    console.log(error);
                    notification('Failed to load profile', 'error', 'bottom-center');
                    this.setState({ loading: false })
                })
        }

        get(`employees/profile/${this.state.userId}?currentEmployeeId=${this.props.currentUser.id}`)
            .then(res => res.json())
            .then(res => {
                let profile = this.state.profile;

                if (res.success)
                    profile = res.data;

                this.setState({ 
                    profile: profile,
                    loading: false
                })
            })
            .catch(error => {
                console.log(error);
                notification('Failed to load your profile', 'error', 'bottom-center');
                this.setState({ loading: false })
            })
    }

    updateProfile() {

    }

 

    render(){
        const {
            profile,
            loading,
            buttons,
            activeButton,
        } = this.state;

        if (loading)
            return <Loader/>

        return(
            <React.Fragment>
                <div className='topic-wrapper'>
                    <div className='topic-holder'>
                        <Form className="general-info-wrapper">
                            <Row>
                                <Col sm={4}>
                                    <Row sm={4}>
                                        <Label sm={2}><b>Name: </b></Label>
                                        <Label sm={2}>{this.state.name}</Label>
                                    </Row>
                                </Col>
                            </Row>
                            <Row>
                                <Col sm={4}>
                                    <Row sm={4}>
                                        <Label sm={2}><b>Email: </b></Label>
                                        <Label sm={2}>{this.state.email}</Label>
                                    </Row>
                                </Col>
                        </Row>
                        { profile &&
                            <Row>
                                <Col sm={4}>
                                    <Row sm={4}>
                                        <Label sm={2}><b>Leader name: </b></Label>
                                        <Label sm={2}>{profile.leaderName}</Label>
                                    </Row>
                                </Col>
                            </Row>
                        }
                        </Form>
                    </div>
                </div>
                <div>
                    <div className='user-profile-buttons-holder'>
                        { buttons.map((button, index) => (
                            <Button
                                className={'user-profile-button ' + (activeButton === button ? 'active' : '')}
                                key={`user-profile-tab-button-${index}`}
                                onClick={() => this.setState({ activeButton: button })} 
                            >
                                {button}
                            </Button>
                        ))} 
                    </div>
                    { activeButton === 'Subordinates' && profile &&
                        <div className='topic-wrapper'>
                            <div className='topic-holder'>
                                <Form className="general-info-wrapper">
                                    { profile.subordinates.map(subordinate => (
                                        <Row>
                                            <Col sm={4}>
                                                <Row sm={4}>
                                                    <Label sm={2}><b>Name: </b></Label>
                                                    <Label sm={2}><Link onClick={this.updateProfile} to={`user-profile?id=${subordinate.id}`}>{subordinate.name}</Link></Label>
                                                </Row>
                                            </Col>
                                        </Row>
                                    ))}
                                </Form>
                            </div>
                        </div>
                    }
                    { activeButton === 'Topics' && profile &&
                        <TopicPage data={profile.topics} />
                    }
                </div>
            </React.Fragment>
        )
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
  
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UserProfile);