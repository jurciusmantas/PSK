import {
    LOGIN_SUCCESS,
} from '../constans';

const initialState = {
    login: null,
    firstName: null,
    lastName: null,
}

export default (state = initialState, action) => {
    console.log("currentUser - reducer");
    console.log("state - " + JSON.stringify(state));
    console.log("action - " + JSON.stringify(action));
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