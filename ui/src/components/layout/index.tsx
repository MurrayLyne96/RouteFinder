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

function Layout(props:any) {
    return (
        <>
            <Navigation></Navigation>           
            <Box marginTop={'80px'}>
                {props.children}
            </Box>
        </>
    );
}

export default Layout;