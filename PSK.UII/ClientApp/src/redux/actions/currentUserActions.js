import {
    LOGIN_SUCCESS,
    AUTH_ERROR
} from '../constans';

export function loginSuccess(currentUser){
    return {
        type: LOGIN_SUCCESS,
        login: currentUser.login,
        name: currentUser.name,
        token: currentUser.token,
        expiredAt: currentUser.expiredAt
    }
}

export function authError() {
    return {
        type: AUTH_ERROR
    }
}