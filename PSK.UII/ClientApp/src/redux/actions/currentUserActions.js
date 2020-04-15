import {
    LOGIN_SUCCESS,
    LOGOUT,
} from '../constans';

export function loginSuccess(currentUser){
    return {
        type: LOGIN_SUCCESS,
        login: currentUser.login,
        firstName: currentUser.firstName,
        lastName: currentUser.lastName,
        token: currentUser.token,
    }
}

export function logout(){
    return {
        type: LOGOUT
    }
}