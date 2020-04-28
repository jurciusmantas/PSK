export function post(url, params = {}){
    return fetch('./api/' + url, {
        method: 'post',
        headers: { 
            'Content-Type': 'application/json'
            //maybe auth token later
        },
        body: JSON.stringify(params)
    })
}

export function get(url) {
    return fetch('./api/' + url, {
        method: 'get',
        headers: { 
            'Content-Type': 'application/json'
        }
    })
}

export function put(url, params = {}) {
    return fetch('./api/' + url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(params)
    })
}