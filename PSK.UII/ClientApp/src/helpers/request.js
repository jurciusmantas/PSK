import { authError } from '../redux/actions/currentUserActions';
import getStore from '../redux/store';
import { getCookie } from './cookie';

var token = 'Token ' + getCookie('AuthToken');
const { store } = getStore();

export function get(url, params = {}) {
    return fetch('./api/' + url, {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': token,
        }
    })
    .then(res => handleErrors(res));
}

export function post(url, params = {}) {
    return fetch('./api/' + url, {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': token,
        },
        body: JSON.stringify(params)
    })
    .then(res => handleErrors(res))
}

export function put(url, params = {}) {
    return fetch('./api/' + url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': token,
        },
        body: JSON.stringify(params)
    })
    .then(res => handleErrors(res))
}

export function del(url, params = {}) {
    return fetch(`./api/${url}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
}

function handleErrors(response) {
    if (response.status === 401) {
        store.dispatch(authError());
        window.location.reload();
        return Promise.reject()
    }
    return response;
}