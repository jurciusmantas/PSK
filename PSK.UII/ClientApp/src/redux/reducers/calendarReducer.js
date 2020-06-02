import {
    SET_DATE,
    CLEAR_DATE
} from '../constants';

const initialState = {
    date: null,
}

export default (state = initialState, action) => {
    switch(action.type){
        case SET_DATE: {
            state.date = action.date;
            return { ...state };
        }
        case CLEAR_DATE:{
            state.date = null;
            return {...state};
        }
        default:
            return state;
    }
}