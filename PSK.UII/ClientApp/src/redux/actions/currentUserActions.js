import {
    LOGIN_SUCCESS,
    LOGOUT,
    AUTH_ERROR
} from '../constants';

export function loginSuccess(currentUser) {
    return {
        type: LOGIN_SUCCESS,
        id: currentUser.employee.id,
        name: currentUser.employee.name,
        email: currentUser.employee.email,
        token: currentUser.token,
        expiredAt: currentUser.expiredAt
    };
}

export function logout() {
    return {
        type: LOGOUT
    }
}

export function authError() {
    return {
        type: AUTH_ERROR
    }
}