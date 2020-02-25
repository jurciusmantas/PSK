import {
    LOGIN_SUCCESS,
} from '../constans';

const initialState = {
    login: null,
    firstName: null,
    lastName: null,
}

export default (state = initialState, action) => {
    switch(action.type){
        case LOGIN_SUCCESS:{
            state.login = action.login;
            state.firstName = action.firstName;
            state.lastName = action.lastName;
            return { ...state };
        }
        default:
            return state;
    }
}