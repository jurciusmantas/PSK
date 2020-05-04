import {
    LOGIN_SUCCESS,
} from '../constants';

const initialState = {
    id: null,
    login: null,
    name: null,
    token: null,
}

export default (state = initialState, action) => {
    switch(action.type){
        case LOGIN_SUCCESS: {
            state.id = action.id;
            state.login = action.login;
            state.name = action.name;
            state.token = action.token;
            return { ...state };
        }
        default:
            return state;
    }
}