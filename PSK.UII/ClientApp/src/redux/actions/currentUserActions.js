import {
    LOGIN_SUCCESS
} from '../constans';

export function loginSuccess(currentUser) {
    return {
        type: LOGIN_SUCCESS,
        ...currentUser,
    };
}