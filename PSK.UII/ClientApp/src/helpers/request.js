import { authError } from '../redux/actions/currentUserActions';
import getStore from '../redux/store';

export function get(url, params = {}) {
        return fetch('./api/' + url, {
            method: 'get',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(handleErrors,url)
}
export function post(url, params = {}) {
    return fetch('./api/' + url, {
        method: 'post',
        headers: { 
            'Content-Type': 'application/json'
            //maybe auth token later
        },
        body: JSON.stringify(params)
    }).then(handleErrors)
}

export function put(url, params = {}) {
    return fetch('./api/' + url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(params)
    }).then(handleErrors)
}

const { store } = getStore();
function handleErrors(response) {
    if (response.status === 401) {
        store.dispatch(authError());
        window.location.reload();
        return Promise.reject()
    }
    return response;
}