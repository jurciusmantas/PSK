import Cookies from 'universal-cookie';

const cookies = new Cookies();

export function getCookie(name){
    return cookies.get(name);
}

export function setCookie(value, expirationDate){
    cookies.set('AuthToken', value, {
        path: '/',
        expires: new Date(expirationDate),
    });
}

export function removeCookie(name) {
    cookies.remove(name, {
        path: '/',
    });
}