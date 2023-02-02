/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import { EmotionJSX } from '@emotion/react/types/jsx-namespace';
import { Box, Button, Paper, TextField, Typography } from '@mui/material';
import { margin2, padding2, thirdWidth, formGroup, marginBottom2 } from '../../css/styling';
import { useState } from 'react';
import { AuthContext } from '../../contexts';
import { IRouteModel } from '../../interfaces/IRouteModel';
import { LoginUtils } from '../../utils';
import { IUserDetailModel } from '../../interfaces/IUserDetailModel';
import { UserService } from '../../services';
import React from 'react';
import dayjs from 'dayjs';
import { IUserUpdateModel } from '../../interfaces/IUserUpdateModel';
import toast from 'react-hot-toast';

function Profile() : EmotionJSX.Element {
    const {state} = AuthContext.useLogin();
    const [user, setUser] = useState<IUserDetailModel>(
        {
            id: '', 
            firstName: '', 
            lastName: '', 
            dateOfBirth: '', 
            email: '', 
            roleId: '', 
            role: {
                roleDescription: '', 
                id: '', 
                roleName: ''
            }, 
            created: '', 
            lastmodified: '', 
            routes: [] 
        }
    );

    const userId = LoginUtils.getUserId(state.token);

    const getUser = async() => {
        let response = await UserService.GetUserById(userId);
        let json = await response.json();
        setUser(json);
    }

    const saveUser = async() => {
        let updateModel : IUserUpdateModel = {
            firstName : user.firstName,
            lastName : user.lastName,
            email : user.email,
            password : '',
            roleId : user.roleId,
            dateOfBirth : user.dateOfBirth
        };

        var response = await UserService.SaveUser(user.id, updateModel);
        console.log(response);
        if (response.status == 204) {
            toast.success('Profile Updated');
        }

    }

    function handleFirstNameChange(event : any) {
        setUser(existingValues => ({
            ...existingValues,
            firstName: event.target.value
        }));
    }

    function handleLastNameChange(event : any) {
        setUser(existingValues => ({
            ...existingValues,
            lastName: event.target.value
        }));
    }

    React.useEffect(() => {
        (async() => {
            await getUser();
        })();
    }, []);

    return (
        <>
            <Box>
                <Paper elevation={3} css={[margin2, padding2]}>
                    <Typography variant='h3' css={marginBottom2}>Account Details</Typography>
                    <div css={[formGroup, thirdWidth]}>
                        <TextField label='First Name' value={user.firstName} onChange={(e) => handleFirstNameChange(e)}></TextField>
                    </div>
                    <div css={[formGroup, thirdWidth]}>
                        <TextField label='Last Name' value={user.lastName} onChange={(e) => handleLastNameChange(e)}></TextField>
                    </div>
                    <Typography>Email Address: {user.email}</Typography>
                    <Typography>Date Of Birth: {dayjs(user.dateOfBirth).format('DD/MM/YYYY')}</Typography>

                    <Button variant='contained' size='large' onClick={saveUser}>Save</Button>
                </Paper>
            </Box>
        </>
    )
}

export default Profile;