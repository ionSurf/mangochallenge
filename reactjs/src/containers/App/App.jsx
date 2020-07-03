import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';

import { history } from '../../helpers';
import { alertActions } from '../../actions';
import { PrivateRoute } from '../../components';
import { LoginPage } from '../LoginPage';
import PortraitList from '../PortraitList/PortraitList';
import PortraitCreate from '../PortraitCreate/PortraitCreate';
import PortraitEdit from '../PortraitEdit/PortraitEdit';
import PortraitDelete from '../PortraitDelete/PortraitDelete';

//import Header from '../Header/Header';

class App extends React.Component {
    constructor(props) {
        super(props);

        const { dispatch } = this.props;
        history.listen((location, action) => {
            // clear alert on location change
            dispatch(alertActions.clear());
        });
    }

    render() {
        const { alert } = this.props;
        //console.log('rendering');
        return (
            <div className="ui container">
                <div className="jumbotron">
                    <div className="container">
                        <div className="col-sm-8 col-sm-offset-2">
                            {alert.message &&
                                <div className={`alert ${alert.type}`}>{alert.message}</div>
                            }
                            <Router history={history}>
                                <div>
                                    <PrivateRoute exact path="/" component={PortraitList} />
                                    <PrivateRoute exact path="/portraits/" component={PortraitList} />
                                    <PrivateRoute exact path="/portraits/new" component={PortraitCreate} />
                                    <PrivateRoute exact path="/portraits/edit/:id" component={PortraitEdit} />
                                    <PrivateRoute exact path="/portraits/delete/:id" component={PortraitDelete} />
                                    <Route path="/login" component={LoginPage} />
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