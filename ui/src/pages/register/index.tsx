/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import React from "react";
import dayjs, { Dayjs } from 'dayjs'
import { FaRunning } from 'react-icons/fa';
import { Grid, TextField, Typography, Button } from '@mui/material';
import { headerCss, alignItemsLeft, justifyItemsLeft, font52, formGroup, halfWidth, fullWidth, thirdWidth, font36 } from '../../css/styling';
import { DatePicker, DateTimePicker } from '@mui/x-date-pickers';
function Register() : JSX.Element {
    
    const [value, setValue] = React.useState<Dayjs | null>(
        dayjs('2000-08-18T21:11:54'),
    );
    
    const handleChange = (newValue: Dayjs | null) => {
        setValue(newValue);
    };

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
                        placeholder='Forename'
                    />
                </div>
                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Last Name'
                        placeholder='Surname'
                    />
                </div>
                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Email Address'
                        type='password'
                        placeholder='example@email.com'
                    />
                </div>
                <div css={[formGroup, fullWidth]}>
                    <TextField 
                        margin='dense'
                        label='Password'
                        type='password'
                    />
                    <Typography variant='body2' marginBottom={'1.5%'}>Password must be at least 6 characters</Typography>
                </div>
                <div css={[formGroup, fullWidth]}>
                    <DatePicker 
                        onChange={handleChange} 
                        value={value} 
                        renderInput={(params) => <TextField {...params} label="Date Of Birth" margin='dense' />}
                    ></DatePicker>
                </div>
                <Button variant='contained' size='large'>Create Account</Button>
            </div>
        </Grid>
    )
}

export default Register;