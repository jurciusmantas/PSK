const initialState = {
    login: null,
    password: null,
}

export default (state = initialState, action) => {
    console.log("action - " + JSON.stringify(action));
    return state;
}