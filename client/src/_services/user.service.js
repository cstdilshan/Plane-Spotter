import { authHeader, config } from '../_helpers';

export const userService = {
    login,
    logout,
    register,
    getAll,
    getById,
    update,
    delete: _delete,
    registerSpotter,
    getAllSpotters,
    getSpotterById,
    updateSpotter
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };

    return fetch(config.apiUrl + '/auth/authenticate', requestOptions)
        .then(handleResponse, handleError)
        .then(user => {
            // login successful if there's a jwt token in the response
            if (user && user.token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
            }

            return user;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    // return fetch(config.apiUrl + '/users', requestOptions).then(handleResponse, handleError);
    return fetch(config.apiUrl + '/account/getAll', requestOptions).then(handleResponse, handleError);
}

function getById(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(config.apiUrl + '/users/' + _id, requestOptions).then(handleResponse, handleError);
}

function register(user) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    // return fetch(config.apiUrl + '/users/register', requestOptions).then(handleResponse, handleError);
    return fetch(config.apiUrl + '/account/register', requestOptions).then(handleResponse, handleError);
}

function update(user) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(config.apiUrl + '/users/' + user.id, requestOptions).then(handleResponse, handleError);
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    return fetch(config.apiUrl + '/users/' + id, requestOptions).then(handleResponse, handleError);
}

function handleResponse(response) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            // return json if it was returned in the response
            var contentType = response.headers.get("content-type");
            if (contentType && contentType.includes("application/json")) {
                response.json().then(json => resolve(json));
            } else {
                resolve();
            }
        } else {
            // return error message from response body
            response.text().then(text => reject(text));
        }
    });
}

function handleError(error) {
    return Promise.reject(error && error.message);
}

function registerSpotter(user) {
    // const formData = new FormData();
    // formData.append('Make', 'make');
    // formData.append('Model', 'make');
    // formData.append('Registration', 'make');
    // formData.append('Location', 'make');
    // formData.append('DateTime', 'make');
    // formData.append('Image', null);
    // formData.append('ImageUrl', 'make');

    // let options = {
    //     headers: {
    //        'Content-Type': 'multipart/form-data'
    //     },
    //     method: 'POST'
    //  };
  
    //  options.body = new FormData();
    //  for (let key in user) {
    //     options.body.append(key, user[key]);
    //  }

    const requestOptions = {
        method: 'POST',
        headers: authHeader(),
        headers: {...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(config.apiUrl + '/planeSpotter/create', requestOptions).then(handleResponse, handleError);
}

function updateSpotter(user) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(),'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(config.apiUrl + '/planeSpotter/update', requestOptions).then(handleResponse, handleError);
}

function getAllSpotters() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(config.apiUrl + '/planeSpotter/getAll', requestOptions).then(handleResponse, handleError);
}

function getSpotterById(id) {
    console.log('getSpotterById service', id)
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(config.apiUrl + '/planeSpotter/' + id, requestOptions).then(handleResponse, handleError);
}