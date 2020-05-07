import {
    LOGIN_SUCCESS
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