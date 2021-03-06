import React from 'react';
import { 
    Form, 
    Row,
    Col,
    Label,
    Button,
} from 'reactstrap';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import './UserProfile.css';
import { get } from '../../helpers/request';
import { notification } from '../../helpers/notification';
import Loader from '../Loader/loader';
import TopicPage from '../Topic/TopicPage';
import '../Topic/TopicPage.css';

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
    }

    componentDidMount() {
        let userId = this.state.userId;
        if (!this.state.userId)
            userId = this.props.currentUser.id;

        get(`employees/${userId}`)
            .then(res => res.json())
            .then(res => {

                if (res.success)
                    this.setState({
                        name: res.data.name,
                        email: res.data.email
                    });
                
                else {
                    notification("Failed to load profile", "error");
                    console.warn("Failed to load profile: ");
                    console.warn(res.message);
                }

            })
            .catch(error => {
                console.log(error);
                notification('Failed to load profile', 'error', 'bottom-center');
                this.setState({ loading: false })
            })

    get(`employees/profile/${userId}?currentEmployeeId=${this.props.currentUser.id}`)
        .then(res => res.json())
        .then(res => {
            if (res.success) {
                this.setState({
                    profile: res.data,
                    loading: false
                })
            }
            else {
                notification("Cannot load employee data :(", "error")
                console.warn("Cannot load employee data")
                console.warn(res.message)
            }
        })
        .catch(error => {
            console.error(`GET employees/profile/${this.props.currentUser.id} failed:`)
            console.error(error);
            this.setState({ loading: false })
        })
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
                        <div className="row">
                            <Link className="btn btn-custom" to={{ pathname: "/edit-user-profile" }}>Edit info</Link>
                        </div>
                       
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
                                                    <Label sm={2}>
                                                        <Link onClick={() => this.setState({ userId: subordinate.id}, () => this.componentDidMount())}>
                                                            {subordinate.name}
                                                        </Link>
                                                    </Label>
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