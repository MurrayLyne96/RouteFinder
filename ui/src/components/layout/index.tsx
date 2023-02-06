/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import { Container } from '@mui/material';
import { FaRunning } from 'react-icons/fa';
import { useNavigate, Link } from 'react-router-dom';
import { fullWidth, navbarLink, marginRight2, marginRight05 } from '../../css/styling';
import Navigation from '../navigation';
import { LoginUtils } from '../../utils';
import { AuthContext } from '../../contexts';

function Layout(props:any) {
    const {state} = AuthContext.useLogin();
    const loggedIn = state.token && !LoginUtils.isTokenExpired(state);
    return (
        <>
            <Navigation></Navigation>           
            <Box marginTop={loggedIn ? '100px' : '0'}>
                {props.children}
            </Box>
        </>
    );
}

export default Layout;