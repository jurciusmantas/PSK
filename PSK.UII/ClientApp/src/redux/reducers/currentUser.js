import {
    LOGIN_SUCCESS,
    AUTH_ERROR
} from '../constans';

const initialState = {
    login: null,
    name: null,
    token: null,
    expiredAt: null
}

export default (state = initialState, action) => {
    switch(action.type){
        case LOGIN_SUCCESS:{
            state.login = action.login;
            state.name = action.name;
            state.token = action.token;
            state.expiredAt = action.expiredAt;
            return { ...state };
        }
        case AUTH_ERROR: {
            state.login = null;
            state.name = null;
            state.token = null;
            state.expiredAt = null;
            state.authError = true;
            return { ...state };
        }
        default:
            return state;
    }
}