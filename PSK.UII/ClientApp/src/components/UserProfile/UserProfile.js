import React from 'react';
import moment from 'moment';
import {
    Button, 
    Form, 
    FormGroup,
    Row,
    Col,
    Label, 
    Input, 
    FormText 
} from 'reactstrap';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import './UserProfile.css';

class UserProfile extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return(
            <React.Fragment>
                <Form className="general-info-wrapper">
                    <Row>
                        <Col sm={3}>
                            <div className="picture-holder">
                                { /* TODO: take avatar from database? Encoded in base64 */}
                                <img src={"https://www.pngitem.com/pimgs/m/292-2921556_rook-chess-piece-chest-game-piece-png-transparent.png"}/>
                            </div>
                        </Col>
                        <Col sm={1}/>
                        <Col sm={8}>
                            <Row sm={6}>
                                <Label sm={2}><b>First Name:</b></Label>
                                <Label sm={4}>{this.props.currentUser.firstName}</Label>
                            </Row>
                            <Row sm={6}>
                                <Label sm={2}><b>Last Name:</b></Label>
                                <Label sm={4}>{this.props.currentUser.lastName}</Label>
                            </Row>
                            <Row sm={6}>
                                <Label sm={2}><b>Login:</b></Label>
                                <Label sm={4}>{this.props.currentUser.login}</Label>
                            </Row>
                        </Col>
                    </Row>
                </Form>
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
  
  export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserProfile));