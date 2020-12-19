import React, { useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Register from './components/Register';
import Login from './components/Login';
import { AppContext, useAppContext } from './lib/contextLib';

import './custom.css'

export default function App() {
    const [userHasAuthenticated] = useState(false);

    return (
        <AppContext.Provider value={{ userHasAuthenticated }}>
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/register' component={Register} />
                <Route path='/login' component={Login} />
            </Layout>
        </AppContext.Provider>
    );
}
