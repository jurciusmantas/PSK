import {
    LOGIN_SUCCESS,
    LOGOUT,
    AUTH_ERROR
} from '../constants';

const initialState = {
    id: null,
    name: null,
    token: null,
    email: null,
    expiredAt: null,
    authError: false,
}

export default (state = initialState, action) => {
    switch(action.type){
        case LOGIN_SUCCESS: {
            state.id = action.id;
            state.name = action.name;
            state.email = action.email;
            state.token = action.token;
            state.expiredAt = action.expiredAt;
            state.authError = false;
            return { ...state };
        }
        case LOGOUT:{
            state.login = null;
            state.name = null;
            state.token = null;
            return {...state};
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