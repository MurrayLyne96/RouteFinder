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
import { AuthRoute, Layout } from './components';
import { useNavigate } from 'react-router-dom';
import { CreateRoutePage, EditRoutePage, ProfilePage, RoutePage, RoutesPage } from './pages';
const UnauthenticatedRoutes = () => {
  return (
    <Routes>
      <Route path="login" element={<Login />} />
      <Route path="register" element={<Register />} />
    </Routes>
  )
}

const AuthenticatedRoutes = () => {
  return (
    <AuthRoute>
      <Routes>
        <Route path="" element={<Dashboard />} />
        <Route path="routes" element={<RoutesPage />} />
        <Route path="routes/:routeId" element={<RoutePage />} />
        <Route path="routes/:routeId/edit" element={<EditRoutePage />} />
        <Route path="routes/create" element={<CreateRoutePage />} />
        <Route path="profile" element={<ProfilePage />} />
      </Routes>
    </AuthRoute>
  )
}

function App() {
  const theme = createTheme(finderThemeOptions);
  const {state} = AuthContext.useLogin();
  const loggedIn = state.token && !LoginUtils.isTokenExpired(state);
  const isUser = loggedIn && LoginUtils.isAdmin(state.token);
  const isAdmin = loggedIn && LoginUtils.isUser(state.token);
  const userId = LoginUtils.getUserId(state.token);
  return (
    <>
      <Toaster></Toaster>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <ThemeProvider theme={theme}>
          <Box>
            <Layout>
              {AuthenticatedRoutes()}
              {!loggedIn && UnauthenticatedRoutes()}
            </Layout>
          </Box>
        </ThemeProvider>
      </LocalizationProvider>
    </>
  );
}

export default App;
