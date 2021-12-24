import React from 'react';
import { Router, Route, Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { history } from '../_helpers';
import { alertActions } from '../_actions';
import { PrivateRoute } from '../_components';
import { HomePage } from '../HomePage';
import { LoginPage } from '../LoginPage';
import { RegisterPage } from '../RegisterPage';
import { SpotterRegisterPage } from '../PlaneSpotter/SpotterRegisterPage';
import { SpotterUpdatePage } from '../PlaneSpotter/SpotteUpdatePage';
import { userActions } from '../_actions';

class App extends React.Component {
    constructor(props) {
        super(props);

        const { dispatch } = this.props;
        history.listen((location, action) => {
            // clear alert on location change
            dispatch(alertActions.clear());
        });
        
    }
    handleLogoutUser() {
        return (e) => this.props.dispatch(userActions.logout());
    }
    render() {
        const { alert } = this.props;
        return (
            <div>
            <nav className="navbar navbar-expand-lg bg-dark navbar-dark">
                <a className="navbar-brand" href="#">Plane Spotter</a>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav mr-auto">
                    <li className="nav-item active">
                    <a className="nav-link" href="/registerspotter">Regiter Plane</a>
                    </li>
                    
                    </ul>
                    <div className="form-inline my-2 my-lg-0">
                        <button className="btn btn-outline-success my-2 my-sm-0" onClick={this.handleLogoutUser()}>Logout</button>
                    </div>
                </div>
            </nav>
            <div className="jumbotron">
                    <div className="container">
                        <div className="col-md-12 col-sm-offset-2">
                            {alert.message &&
                                <div className={`alert ${alert.type}`}>{alert.message}</div>
                            }
                            <Router history={history}>
                                <div>
                                    <PrivateRoute exact path="/" component={HomePage} />
                                    <Route path="/login" component={LoginPage} />
                                    <Route path="/register" component={RegisterPage} />
                                    <PrivateRoute exact path="/registerspotter" component={SpotterRegisterPage} />
                                    <PrivateRoute exact path="/updatespotter" component={SpotterUpdatePage} />
                                </div>
                            </Router>
                        </div>
                    </div>
                </div>
            </div>
            
        );
    }
}

function mapStateToProps(state) {
    const { alert } = state;
    return {
        alert
    };
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App }; 