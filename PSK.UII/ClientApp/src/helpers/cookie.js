import Cookies from 'universal-cookie';

export function getCookie(name){
    let cookies = new Cookies();
    return cookies.get(name);
}

export function setCookie(value, expirationDate){
    let cookies = new Cookies();
    cookies.set('AuthToken', value, {
        path: '/',
        expires: new Date(expirationDate)
    });
}