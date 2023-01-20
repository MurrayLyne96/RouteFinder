/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import React from "react";
import dayjs, { Dayjs } from 'dayjs'
import { FaRunning } from 'react-icons/fa';
import { Grid, TextField, Typography, Button } from '@mui/material';
import { headerCss, alignItemsLeft, justifyItemsLeft, font52, formGroup, halfWidth, fullWidth, thirdWidth, font36 } from '../../css/styling';
import { DatePicker, DateTimePicker } from '@mui/x-date-pickers';
import { IUserCreateModel } from '../../interfaces/IUserCreateModel';
function Register() : JSX.Element {

    const [userModel, setUsermodel] = React.useState<IUserCreateModel>({firstName: '', lastName: '', dateOfBirth: dayjs('2000-08-18T21:11:54'), email: '', password: ''});
    
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
                        onChange={handleLastNameChange}
                        placeholder='Surname'
                    />
                </div>

                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Email Address'
                        type='email'
                        onChange={handleEmailChange}
                        placeholder='example@email.com'
                    />
                </div>

                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Password'
                        type='password'
                        onChange={handlePasswordChange}
                    />
                    <Typography variant='body2' marginBottom={'1.5%'}>Password must be at least 6 characters</Typography>
                </div>

                <div css={[formGroup, fullWidth]}>
                    <DatePicker 
                        onChange={handleDateOfBirthChange} 
                        value={userModel.dateOfBirth}
                        renderInput={(params) => <TextField {...params} label="Date Of Birth" margin='dense' />}
                    ></DatePicker>
                </div>

                <Button variant='contained' size='large'>Create Account</Button>
            </div>
        </Grid>
    )
}

export default Register;