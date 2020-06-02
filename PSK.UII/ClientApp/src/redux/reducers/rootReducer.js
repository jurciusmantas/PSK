import { combineReducers } from "redux";

import currentUser from './currentUser';
import calendarReducer from './calendarReducer';
import { reducer as toastrReducer } from 'react-redux-toastr'

export default combineReducers({
    currentUser,
    calendarReducer,
    toastr: toastrReducer,
})