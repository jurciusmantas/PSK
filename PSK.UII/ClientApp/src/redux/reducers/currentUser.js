import {
    LOGIN_SUCCESS,
    LOGOUT,
} from '../constans';

const initialState = {
    login: null,
    firstName: null,
    lastName: null,
    token: null,
}

export default (state = initialState, action) => {
    switch(action.type){
        case LOGIN_SUCCESS:{
            state.login = action.login;
            state.firstName = action.firstName;
            state.lastName = action.lastName;
            state.token = action.token;
            return { ...state };
        }
        case LOGOUT:{
            state.login = null;
            state.firstName = null;
            state.lastName = null;
            state.token = null;
            return {...state};
        }
        default:
            return state;
    }
}