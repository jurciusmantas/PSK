import React, { Component } from 'react';
import { 
  Button, 
  Collapse, 
  Container, 
  Navbar, 
  NavbarBrand, 
  NavbarToggler, 
  NavItem, 
  NavLink 
} from 'reactstrap';
import { Link, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import * as currentUserActions from '../../redux/actions/currentUserActions';
import { removeCookie } from '../../helpers/cookie';
import { post } from '../../helpers/request';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons'
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.logout = this.logout.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    logout() {
        this.props.logout();
        removeCookie('AuthToken');
        this.props.history.push('/');
        post("login/logout");
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">PSK.UI</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/home">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/topics">Topics</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/recommendations">Recommendations</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/invite">Invite</NavLink>
                                </NavItem>
                                <NavItem>
                                    <Button className="sign-out-button" onClick={() => this.logout()}>
                                        <FontAwesomeIcon icon={faSignOutAlt} />
                                    </Button>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}

const mapStateToProps = (state, ownProps) => {
  return {
      
  }
}

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
      logout: () => dispatch(currentUserActions.logout())
  }
}

export default withRouter(connect(
  mapStateToProps,
  mapDispatchToProps
)(NavMenu));
