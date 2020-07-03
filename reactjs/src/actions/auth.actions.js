import { authConstants } from '../constants';
import { authService } from '../services';
import { alertActions } from '.';
import { history } from '../helpers';

const login = (username, password) => async dispatch => {
    const request = user    => ({ type: authConstants.LOGIN_REQUEST, user });
    const success = user    => ({ type: authConstants.LOGIN_SUCCESS, user });
    const failure = error   => ({ type: authConstants.LOGIN_FAILURE, error });

    dispatch( request({ username }) );
    await authService.login( username, password ).then(
        user => {
            dispatch( success(user) );
            history.push('/');
        },
        error => {
            dispatch(failure(error));
            dispatch(alertActions.error(error));
        }
    );
}

const logout = () => {
    authService.logout();
    return { type: authConstants.LOGOUT };
}

export const authActions = {
    login,
    logout
};