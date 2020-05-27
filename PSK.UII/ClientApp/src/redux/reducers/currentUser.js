import {
    LOGIN_SUCCESS,
    LOGOUT,
} from '../constants';

const initialState = {
    id: null,
    login: null,
    name: null,
    token: null,
    expiredAt: null
}

export default (state = initialState, action) => {
    switch(action.type){
        case LOGIN_SUCCESS: {
            state.id = action.id;
            state.login = action.login;
            state.name = action.name;
            state.token = action.token;
            state.expiredAt = action.expiredAt;
            return { ...state };
        }
        case LOGOUT:{
            state.login = null;
            state.name = null;
            state.token = null;
            return {...state};
        }
        default:
            return state;
    }
}