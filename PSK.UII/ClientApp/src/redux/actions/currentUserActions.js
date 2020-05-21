import {
    LOGIN_SUCCESS,
    LOGOUT,
} from '../constants';

export function loginSuccess(currentUser) {
    return {
        type: LOGIN_SUCCESS,
        ...currentUser,
    };
}

export function logout() {
    return {
        type: LOGOUT
    }
}