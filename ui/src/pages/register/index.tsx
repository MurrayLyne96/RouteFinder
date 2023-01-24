/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import React from "react";
import dayjs, { Dayjs } from 'dayjs'
import { FaRunning } from 'react-icons/fa';
import { Grid, TextField, Typography, Button } from '@mui/material';
import { headerCss, alignItemsLeft, justifyItemsLeft, font52, formGroup, halfWidth, fullWidth, thirdWidth, font36 } from '../../css/styling';
import { DatePicker, DateTimePicker } from '@mui/x-date-pickers';
import { IUserCreateModel } from '../../interfaces/IUserCreateModel';
import { UserService } from '../../services';
import { useNavigate } from 'react-router-dom';
import toast from 'react-hot-toast';
function Register() : JSX.Element {

    const [userModel, setUsermodel] = React.useState<IUserCreateModel>({firstName: '', lastName: '', dateOfBirth: dayjs('2000-08-18T21:11:54'), email: '', password: ''});
    const [repeatPassword, setRepeatPassword] = React.useState('');
    const navigate = useNavigate();

    const handleDateOfBirthChange = (newValue : Dayjs | null) => {
        if (newValue != null) {     
            setUsermodel(existingValues => ({
                ...existingValues,
                dateOfBirth: newValue
            }));
        }
    };

    function handleFirstNameChange(event : any) {
        setUsermodel(existingValues => ({
            ...existingValues,
            firstName: event.target.value
        }));
    }

    function handleLastNameChange(event : any) {
        setUsermodel(existingValues => ({
            ...existingValues,
            lastName: event.target.value
        }));
    }

    function handleEmailChange(event : any) {
        setUsermodel(existingValues => ({
            ...existingValues,
            email: event.target.value
        }));
    }

    function handlePasswordChange(event : any) {
        setUsermodel(existingValues => ({
            ...existingValues,
            password: event.target.value
        }));
    }

    const createAccount = async () => {
        //2023-01-23T10:48:08.787Z
        let dateOfBirthFormatted = userModel.dateOfBirth.format("YYYY-MM-DD");
        console.log(dateOfBirthFormatted);
        if (userModel.password === repeatPassword) {
            const response = await UserService.RegisterNewUser(userModel.firstName, userModel.lastName, userModel.email, dateOfBirthFormatted, userModel.password);
            if (response.status == 201) {
                navigate('/login');
            } else {
                console.log(await response.json());
            }
        } else {
            toast.error("Please check your details");
        }
    }

    return (
        <Grid
            container
            flexDirection='column'
            justifyContent='center'
            alignItems='center'
            minHeight='35vh'
        >
            <Typography css={[headerCss, font52]} variant='h1'>RouteFinder <FaRunning /></Typography>  
            <div css={thirdWidth}>
                <Typography css={font36} variant='h2'>Create Account</Typography>
                
                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='First Name'
                        value={userModel.firstName}
                        onChange={handleFirstNameChange}
                        placeholder='Forename'
                    />
                </div>

                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Last Name'
                        value={userModel.lastName}
                        onChange={handleLastNameChange}
                        placeholder='Surname'
                    />
                </div>

                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Email Address'
                        type='email'
                        value={userModel.email}
                        onChange={handleEmailChange}
                        placeholder='example@email.com'
                    />
                </div>

                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Password'
                        type='password'
                        value={userModel.password}
                        onChange={handlePasswordChange}
                    />
                    <Typography variant='body2' marginBottom={'1.5%'}>Password must be at least 6 characters</Typography>
                </div>

                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Repeat Password'
                        type='password'
                        value={repeatPassword}
                        onChange={e => setRepeatPassword(e.target.value)}
                    />
                </div>

                <div css={[formGroup, fullWidth]}>
                    <DatePicker 
                        onChange={handleDateOfBirthChange} 
                        value={userModel.dateOfBirth}
                        renderInput={(params) => <TextField {...params} label="Date Of Birth" margin='dense' />}
                    ></DatePicker>
                </div>

                <Button variant='contained' size='large' onClick={createAccount}>Create Account</Button>
            </div>
        </Grid>
    )
}

export default Register;