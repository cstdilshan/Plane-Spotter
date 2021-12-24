import { userConstants } from '../_constants';
import { userService } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const userActions = {
    login,
    logout,
    register,
    getAll,
    delete: _delete,
    registerSpotter,
    getAllSpotters,
    getSpotterById,
    updateSpotter
};

function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        userService.login(username, password)
            .then(
                user => { 
                    dispatch(success(user));
                    history.push('/');
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(user) { return { type: userConstants.LOGIN_REQUEST, user } }
    function success(user) { return { type: userConstants.LOGIN_SUCCESS, user } }
    function failure(error) { return { type: userConstants.LOGIN_FAILURE, error } }
}

function logout() {
    userService.logout();
    history.push('/login');
    return { type: userConstants.LOGOUT };
}

function register(user) {
    return dispatch => {
        dispatch(request(user));

        userService.register(user)
            .then(
                () => { 
                    dispatch(success());
                    history.push('/login');
                    dispatch(alertActions.success('Registration successful'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(user) { return { type: userConstants.REGISTER_REQUEST, user } }
    function success(user) { return { type: userConstants.REGISTER_SUCCESS, user } }
    function failure(error) { return { type: userConstants.REGISTER_FAILURE, error } }
}

function getAll() {
    return dispatch => {
        dispatch(request());

        userService.getAll()
            .then(
                users => dispatch(success(users)),
                error => dispatch(failure(error))
            );
    };

    function request() { return { type: userConstants.GETALL_REQUEST } }
    function success(users) { return { type: userConstants.GETALL_SUCCESS, users } }
    function failure(error) { return { type: userConstants.GETALL_FAILURE, error } }
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    return dispatch => {
        dispatch(request(id));

        userService.delete(id)
            .then(
                () => { 
                    dispatch(success(id));
                },
                error => {
                    dispatch(failure(id, error));
                }
            );
    };

    function request(id) { return { type: userConstants.DELETE_REQUEST, id } }
    function success(id) { return { type: userConstants.DELETE_SUCCESS, id } }
    function failure(id, error) { return { type: userConstants.DELETE_FAILURE, id, error } }
}

function registerSpotter(spotter) {
    return dispatch => {
        dispatch(request(spotter));

        userService.registerSpotter(spotter)
            .then(
                spotter => { 
                    dispatch(success(spotter));
                    history.push('/');
                    dispatch(alertActions.success('Spotter Registration successful'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(spotter) { return { type: userConstants.SPOTTER_REGISTER_REQUEST, spotter } }
    function success(spotter) { return { type: userConstants.SPOTTER_REGISTER_SUCCESS, spotter } }
    function failure(error) { return { type: userConstants.SPOTTER_REGISTER_FAILURE, error } }
}

function updateSpotter(spotter) {
    return dispatch => {
        dispatch(request(spotter));

        userService.updateSpotter(spotter)
            .then(
                spotter => { 
                    dispatch(success(spotter));
                    history.push('/');
                    dispatch(alertActions.success('Spotter Registration successful'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(spotter) { return { type: userConstants.SPOTTER_REGISTER_REQUEST, spotter } }
    function success(spotter) { return { type: userConstants.SPOTTER_REGISTER_SUCCESS, spotter } }
    function failure(error) { return { type: userConstants.SPOTTER_REGISTER_FAILURE, error } }
}

function getAllSpotters() {
    return dispatch => {
        dispatch(request());

        userService.getAllSpotters()
            .then(
                spotters => dispatch(success(spotters)),
                error => dispatch(failure(error))
            );
    };

    function request() { return { type: userConstants.SPOTTER_GETALL_REQUEST } }
    function success(spotters) { return { type: userConstants.SPOTTER_GETALL_SUCCESS, spotters } }
    function failure(error) { return { type: userConstants.SPOTTER_GETALL_FAILURE, error } }
}

function getSpotterById(id) {
    return dispatch => {
        dispatch(request(id));

        userService.getSpotterById(id)
            .then(
                spotter => {
                    dispatch(success(spotter)),
                    history.push('/updatespotter');
                },
                error => {
                    dispatch(failure(id, error));
                }
            );
    };

    function request(id) { return { type: userConstants.SPOTTER_GET_REQUEST, id } }
    function success(spotter) { return { type: userConstants.SPOTTER_GET_SUCCESS, spotter } }
    function failure(id, error) { return { type: userConstants.DELETE_FAILURE, id, error } }
}
