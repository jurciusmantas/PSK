import {
    SET_DATE,
    CLEAR_DATE
} from '../constants';

export function setDate(date) {
    return {
        type: SET_DATE,
        date: date,
    };
}

export function clearDate() {
    return {
        type: CLEAR_DATE
    }
}