import React, { Children } from 'react';
import { ReactDOM } from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import logo from './logo.svg';
import Login from './pages/login';
import Register from './pages/register';
import { ThemeProvider, createTheme, Box } from '@mui/material';
import { finderThemeOptions } from './css/styling';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { AdapterLuxon } from '@mui/x-date-pickers/AdapterLuxon';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { Toaster } from 'react-hot-toast';
import { AuthContext } from './contexts';
import { LoginUtils } from './utils';
import Dashboard from './pages/dashboard';
import { Layout } from './components';

const UnauthenticatedRoutes = () => {
  return (
    <>
      <Route path="login" element={<Login />} />
      <Route path="register" element={<Register />} />
    </>
  )
}

const AuthenticatedRoutes = () => {
  return (
  <>
    <Route path="dashboard" element={<Dashboard />} />
  </>
  )
}

function App() {
  const theme = createTheme(finderThemeOptions);
  const {state} = AuthContext.useLogin();
  const loggedIn = state.token && !LoginUtils.isTokenExpired(state);
  const isUser = loggedIn && LoginUtils.isAdmin(state.token);
  const isAdmin = loggedIn && LoginUtils.isUser(state.token);
  return (
    <>
      <Toaster></Toaster>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <ThemeProvider theme={theme}>
          <Box>
            <Layout>
              <Routes>
                {loggedIn && AuthenticatedRoutes()}
                {!loggedIn && UnauthenticatedRoutes()}
              </Routes>
            </Layout>
          </Box>
        </ThemeProvider>
      </LocalizationProvider>
    </>
  );
}

export default App;
