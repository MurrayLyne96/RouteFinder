import jwtDecode from "jwt-decode";
import Roles from "../constants/roles";
import { access } from "fs";
const isTokenExpired = (token: any) => {
    if (!token.token) return true;
    // if (!token || !token.token || !token.refreshToken) return true; TODO: bring this back once mark has gone through how refresh tokens work.
    const accessJwt = jwtDecode(token.token) as any;

    const currentTime = new Date().getTime() / 1000;

    if (currentTime < accessJwt.exp) return false;

    // const refreshJwt = jwtDecode(token.refreshToken) as any;

    // if (currentTime < refreshJwt.exp) return false; //TODO: Bring these lines back once refresh token is implemented.

    return true;
};

const hasRole = (accessToken: string) => {
    if (!accessToken) return false;
    const obj = jwtDecode(accessToken) as any;

    return Object.values(Roles).includes(obj.role);
}

const getRole = (accessToken: string) : boolean | string => {
    if (!accessToken) return false;
    const obj = jwtDecode(accessToken) as any;
    return obj.role
}

const isUser = (accessToken: string) => {
    if (!accessToken) return false;
    const obj = jwtDecode(accessToken) as any;
    return obj.role === Roles.User;
};

const getUserId = (accessToken: string) => {
    if (!accessToken) return false;
    const obj = jwtDecode(accessToken) as any;
    return obj.nameid;
}

const isAdmin = (accessToken: string) => {
    if (!accessToken) return false;
    const obj = jwtDecode(accessToken) as any;
    return obj.role === Roles.Admin;
};

const getEmail = (accessToken: any) => {
    if (!accessToken) return false;

    const obj = jwtDecode(accessToken) as any;

    return obj.email;
};

const LoginUtils = {
    isTokenExpired,
    hasRole,
    isAdmin,
    getRole,
    getEmail,
    getUserId,
    isUser,
};

export default LoginUtils;