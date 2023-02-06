import { LoginUtils } from '../../utils';
import { AuthContext } from '../../contexts';
import React from 'react';
import { useNavigate } from 'react-router-dom';

function AuthRoute(props: any) {
    const {state} = AuthContext.useLogin();
    const loggedIn = state.token && !LoginUtils.isTokenExpired(state);
    const navigate = useNavigate();
    
    React.useEffect(() => {
        if (!loggedIn) {
            navigate('/login');
        }
    }, []);

    return(<>{props.children}</>)
}

export default AuthRoute;