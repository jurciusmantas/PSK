export function post(url, params = {}){
    return fetch('http://localhost:55107/api/' + url, {
        method: 'post',
        headers: { 
            'Content-Type': 'application/json'
            //maybe auth token later
        },
        body: JSON.stringify(params)
    })
}