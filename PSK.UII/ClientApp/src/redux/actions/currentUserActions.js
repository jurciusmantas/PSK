import {
    LOGIN_SUCCESS
} from '../constans';

export function loginSuccess(currentUser){
    return {
        type: LOGIN_SUCCESS,
        login: currentUser.login,
        firstName: currentUser.firstName,
        lastName: currentUser.lastName,
    }
}