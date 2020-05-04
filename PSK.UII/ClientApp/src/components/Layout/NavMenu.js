import React, { Component } from 'react';
import { 
  Button, 
  Collapse, 
  Container, 
  Navbar, 
  NavbarBrand, 
  NavbarToggler, 
  NavItem, 
  NavLink,
  Label,
} from 'reactstrap';
import { Link, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import * as currentUserActions from '../../redux/actions/currentUserActions';
import { removeCookie } from '../../helpers/cookie';
import { get } from '../../helpers/request';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { 
  faSignOutAlt,
  faUser,
} from '@fortawesome/free-solid-svg-icons';
import './NavMenu.css';

class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.state = {
      collapsed: true
    };

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.logout = this.logout.bind(this);
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  logout(){
    this.props.logout();
    removeCookie('AuthToken');
    this.props.history.push('/');
    get("login/logout");
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/home">PSK.UI</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/home">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/topic">Topics</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/recommendations">Recommendations</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/invite">Invite</NavLink>
                </NavItem>
                <NavItem>
                  <Button className="nav-button-with-icon" onClick={() => this.props.history.push('/user_profile')}>
                    <div className="button-with-icon-wrapper">
                      <Label>Profile</Label>
                      <FontAwesomeIcon icon={faUser} />
                    </div>
                  </Button>
                </NavItem>
                <NavItem>
                  <Button className="nav-button-with-icon" onClick={() => this.logout()}>
                    <div className="button-with-icon-wrapper">
                      <Label>Logout</Label>
                      <FontAwesomeIcon icon={faSignOutAlt} />
                    </div>
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
