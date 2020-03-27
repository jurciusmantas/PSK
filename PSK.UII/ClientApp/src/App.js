import React from 'react';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react'
import Routes from './routes/Routes';
import getStore from './redux/store';

const { store, persistor } = getStore();

const App = () => (
  <Provider store={store}>
    <PersistGate loading={null} persistor={persistor}>
      <Routes/>
    </PersistGate>
  </Provider>
);

export default App;
