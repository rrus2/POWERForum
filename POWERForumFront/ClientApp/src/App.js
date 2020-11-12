import React, { Component, useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import Register from './components/Register';
import Login from './components/Login';
import { AppContext } from './lib/contextLib';

import './custom.css'

export default function App() {
    const displayName = App.name;
    const [isAuthenticated, userHasAuthenticated] = useState([]);

    return (
        <AppContext.Provider value={{ isAuthenticated, userHasAuthenticated }}>
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/register' component={Register} />
                <Route path='/login' component={Login} />
                <Route path='/fetch-data' component={FetchData} />
            </Layout>
        </AppContext.Provider>
    );
}
