import React from "react";
import { ReactDOM } from "react";
import { TextField, Link } from "@mui/material";
import { AuthModel } from '../../classes/AuthModel';
import { IAuthModel } from '../../interfaces/IAuthModel';
function Login() {
    const [auth, setAuth] = React.useState<IAuthModel>({email: '', password: ''})

    return (
        <p>test</p>
    );
}

export default Login();