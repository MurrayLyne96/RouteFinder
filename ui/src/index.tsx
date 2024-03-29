import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import {
  BrowserRouter as Router
} from "react-router-dom";
import { AuthContext } from './contexts';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <Router>
    <AuthContext.AuthProvider>
      <App />
    </AuthContext.AuthProvider>
  </Router>

);
