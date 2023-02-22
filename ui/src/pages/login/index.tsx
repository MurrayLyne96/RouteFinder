/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import {AuthenticationService, StorageService} from "../../services";
import React from "react";
import {RingLoader} from "react-spinners";
import Storage_types from '../../constants/storage_types';
import toast from 'react-hot-toast';
import { ReactDOM } from "react";
import AuthContext from "../../contexts/authContext"
import { TextField, Button, Grid, Typography } from '@mui/material';
import { IAuthModel } from '../../interfaces/IAuthModel';
import { LoginUtils } from '../../utils';
import { FaRunning } from 'react-icons/fa';
import { headerCss, font52, formGroup, loginButtonsBottom, flexBasis50, alignItemsLeft, justifyItemsRight, centeredItem, thirdWidth, formGroupWidth } from '../../css/styling';
import { useNavigate, Link } from 'react-router-dom';

function Login() : JSX.Element {
    const [auth, setAuth] = React.useState<IAuthModel>({email: '', password: ''})
    const {dispatch} = AuthContext.useLogin();
    const [loading, setLoading] = React.useState(false);
    const navigate = useNavigate();

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

    const override: React.CSSProperties = {
        position: "absolute",
        zIndex: "1000",
      };

    const authentication = async () => {
        setLoading(true);
        let response = await AuthenticationService.authenticate(auth.email, auth.password);
        setLoading(false);
        if (response.status === 200) {
            const loginResult = await response.json();
            let hasRole = LoginUtils.hasRole(loginResult.token);
            if (hasRole) {
                StorageService.setLocalStorage(loginResult, Storage_types.AUTH);
                StorageService.setLocalStorage(auth.email, Storage_types.EMAIL);
                
                dispatch({
                    type: "authentication",
                    ...loginResult,
                });
            }
            navigate('/');
        } else {
            toast.error("Invalid authentication details");
        }
    };

    return (
        <Grid
            container
            flexDirection="column"
            justifyContent="center"
            alignItems="center"
            minHeight="98vh"
        >
            <Typography variant='h1' css={[headerCss, font52]}>RouteFinder <FaRunning /></Typography>
            <div css={[formGroup, formGroupWidth]}>
                <TextField 
                    value={auth.email}
                    onChange={handleEmailChange}
                    label="Email Address"
                    type="email"
                    onKeyUp={(e) => {
                        if (e.key === 'Enter') {
                            authentication();
                        }
                    }}
                    autoFocus
                />
                <Typography variant='body2'>We'll never share your email with anyone else.</Typography>
            </div>
            <div css={[formGroup, formGroupWidth]}>
                <TextField 
                    autoFocus 
                    label="Password"
                    value={auth.password}
                    onChange={handlePasswordChange}
                    type="password"
                    onKeyUp={(e) => {
                        if (e.key === 'Enter') {
                            authentication();
                        }
                    }}
                    className="form-control mb-2"
                />
            </div>
            <div css={[formGroup, formGroupWidth]}>
                <div css={loginButtonsBottom}>
                    <div css={[flexBasis50, alignItemsLeft]}>
                        <Button variant="contained" onClick={authentication}>Login</Button>
                    </div>
                    <div css={[flexBasis50, justifyItemsRight]}>
                        <Link to={"/register"} css={centeredItem}>Register new Account</Link>
                    </div>
                </div>
            </div>
            <RingLoader
                loading={loading}
                size={150}
                cssOverride={override}
                aria-label="Loading Spinner"
                data-testid="loader"
            />
        </Grid>
    );
}

export default Login;