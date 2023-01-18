import React from 'react';
import { ReactDOM } from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import logo from './logo.svg';
import Login from './pages/login';
import Register from './pages/register';
import { ThemeProvider, createTheme } from '@mui/material';
import { finderThemeOptions } from './css/styling';
function App() {
  const theme = createTheme(finderThemeOptions);
  return (
    <>
      <ThemeProvider theme={theme}>
        <Routes>
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
        </Routes>
      </ThemeProvider>
    </>
  );
}

export default App;
