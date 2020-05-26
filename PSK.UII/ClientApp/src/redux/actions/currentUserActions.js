import {
    LOGIN_SUCCESS,
    LOGOUT,
} from '../constants';

export function loginSuccess(currentUser) {
    return {
        type: LOGIN_SUCCESS,
        login: currentUser.login,
        name: currentUser.name,
        token: currentUser.token,
		expiredAt: currentUser.expiredAt
    };
}

export function logout() {
    return {
        type: LOGOUT
    }
}