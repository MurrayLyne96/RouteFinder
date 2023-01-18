import React from "react";
import { FaRunning } from 'react-icons/fa';
function Register() : JSX.Element {

    return (
        <div className="center-screen">
            <h1 className="routefinder-header font-32">RouteFinder <FaRunning /></h1>
            <div className="flex-align-left col-6">
                <h2>Register New Account</h2>
            </div>
        </div>
    )
}

export default Register;