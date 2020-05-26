import { combineReducers } from "redux";

import currentUser from './currentUser';
import {reducer as toastrReducer} from 'react-redux-toastr'

export default combineReducers({
    currentUser,
    toastr: toastrReducer,
})