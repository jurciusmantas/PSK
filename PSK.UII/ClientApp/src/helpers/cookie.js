import Cookies from 'universal-cookie';
import moment from 'moment';

const cookies = new Cookies();

export function getCookie(name){
    return cookies.get(name);
}

export function setCookie(value){
    let expirationDate = moment().add(15, 'minute').toDate();
    cookies.set('AuthToken', value, { 
        path: '/',
        expires: expirationDate,
    });
}

export function removeCookie(name){
    cookies.remove(name, {
        path: '/',
    });
}