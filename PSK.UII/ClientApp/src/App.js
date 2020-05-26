import React from 'react';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react'
import Routes from './routes/Routes';
import getStore from './redux/store';
import ReduxToastr from 'react-redux-toastr'

const { store, persistor } = getStore();

const App = () => (
  <Provider store={store}>
    <PersistGate loading={null} persistor={persistor}>
      <Routes />
    </PersistGate>
    <ReduxToastr
      timeOut={4000}
      newestOnTop={false}
      preventDuplicates
      position="bottom-center"
      getState={(state) => state.toastr}
      transitionIn="fadeIn"
      transitionOut="fadeOut"
      closeOnToastrClick
    />
  </Provider>
);

export default App;
