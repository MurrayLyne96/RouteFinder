import jwtDecode from "jwt-decode";
import Roles from "../constants/roles";
const isTokenExpired = (token: any) => {
    if (!token || !token.accessToken || !token.refreshToken) return true;

    const accessJwt = jwtDecode(token.accessToken) as any;
    const currentTime = new Date().getTime() / 1000;

    if (currentTime < accessJwt.exp) return false;

    const refreshJwt = jwtDecode(token.refreshToken) as any;

    if (currentTime < refreshJwt.exp) return false;

    return true;
};

const hasRole = (accessToken: string, ...roles: Roles[]) => {
    if (!accessToken) return false;
    const {claims: {role_id}} = jwtDecode(accessToken) as any;

    return roles.includes(role_id);
}

const isUser = (accessToken: string) => {
    if (!accessToken) return false;
    const {claims: {role_id}} = jwtDecode(accessToken) as any;
    return role_id === Roles.User;
};

const isAdmin = (accessToken: string) => {
    if (!accessToken) return false;
    const {claims: {role_id}} = jwtDecode(accessToken) as any;
    return role_id === Roles.Admin;
};

const getEmail = (accessToken: any) => {
    if (!accessToken) return false;

    const {claims: {email}} = jwtDecode(accessToken) as any;

    return email;
};

const getUserShortDisplay = (accessToken: any): string => {
    if (!accessToken) return "";

    const {claims: {first_name, last_name}} = jwtDecode(accessToken) as any;

    return `${first_name.charAt(0).toUpperCase()}${last_name.charAt(0).toUpperCase()}`;
};

const getUserId = (accessToken: string) => {
    if (!accessToken) return false;

    const {sub} = jwtDecode(accessToken) as any;

    return sub;
};

const LoginUtils = {
    isTokenExpired,
    hasRole,
    isAdmin,
    getEmail,
    getUserShortDisplay,
    getUserId,
    isUser,
};

export default LoginUtils;