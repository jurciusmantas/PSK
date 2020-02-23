import { compose, applyMiddleware, createStore } from 'redux';
import rootReducer from './reducers/rootReducer';
import thunk from 'redux-thunk';

const store = createStore(
    rootReducer,
    {},
    compose(applyMiddleware(thunk))
)

export default store;