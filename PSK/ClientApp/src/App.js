import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import LoginPage from './components/Login/LoginPage';
import Home from './components/Home/Home';

export default () => (
  <Layout>
    <Route exact path='/' component={LoginPage} />
    <Route path='/home' component={Home} />
  </Layout>
);
