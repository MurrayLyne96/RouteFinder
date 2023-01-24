/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import {AuthenticationService, StorageService} from "../../services";
import React from "react";
import {RingLoader} from "react-spinners";
import Storage_types from '../../constants/storage_types';
import toast from 'react-hot-toast';
import { ReactDOM } from "react";
import AuthContext from "../../contexts/authContext"
import { TextField, Link, Button, Grid, Typography, Toolbar } from '@mui/material';
import { IAuthModel } from '../../interfaces/IAuthModel';
import { LoginUtils } from '../../utils';
import { FaRunning } from 'react-icons/fa';
import { headerCss, font52, formGroup, loginButtonsBottom, flexBasis50, alignItemsLeft, justifyItemsRight, centeredItem, thirdWidth } from '../../css/styling';
import { useNavigate } from 'react-router-dom';

function Dashboard() : JSX.Element {
    return (
        <>
            <Grid
                container
                spacing={2}
            >
                <Grid item md={4}>
                    <Typography>Test</Typography>
                </Grid>
                <Grid item md={8}>
                    <p>Test</p>
                </Grid>
            </Grid>
        </>
    )
}

export default Dashboard;