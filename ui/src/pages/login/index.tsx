/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import React from "react";
import { ReactDOM } from "react";
import { TextField, Link, Button, Grid, Typography } from '@mui/material';
import { IAuthModel } from '../../interfaces/IAuthModel';
import { FaRunning } from 'react-icons/fa';
import { headerCss, font52, formGroup, loginButtonsBottom, flexBasis50, alignItemsLeft, alignItemsRight } from '../../css/styling';
function Login() : JSX.Element {
    const [auth, setAuth] = React.useState<IAuthModel>({email: '', password: ''})

    function handleEmailChange(event : any) {
        setAuth(existingValues => (
            {
                ...existingValues, 
                email : event.target.value
            }
        ));
    }

    function handlePasswordChange(event : any) {
        setAuth(existingValues => (
            {
                ...existingValues, 
                password : event.target.value
            }
        ));
    }

    return (
        <Grid
            container
            flexDirection="column"
            justifyContent="center"
            alignItems="center"
            minHeight="100vh"
        >
            <Typography variant='h1' css={[headerCss, font52]}>RouteFinder <FaRunning /></Typography>
            <div css={formGroup}>
                <TextField 
                    value={auth.email}
                    onChange={handleEmailChange}
                    label="Email Address"
                    type="email"
                    autoFocus
                />
                <Typography variant='body2'>We'll never share your email with anyone else.</Typography>
            </div>
            <div css={formGroup}>
                <TextField 
                    autoFocus 
                    label="Password"
                    value={auth.password}
                    onChange={handlePasswordChange}
                    type="password"
                    className="form-control mb-2"
                />
            </div>
            <div css={formGroup}>
                <div css={loginButtonsBottom}>
                    <div css={[flexBasis50, alignItemsLeft]}>
                        <Button variant="contained">Login</Button>
                    </div>
                    <div css={[flexBasis50, alignItemsRight]}>
                        <Link href="register" className="centred-item">Register a new Account</Link>
                    </div>
                </div>
            </div>
        </Grid>
    );
}

export default Login;