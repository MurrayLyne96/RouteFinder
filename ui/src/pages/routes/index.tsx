/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react';
import { Checkbox, FormControlLabel, Grid, InputAdornment, MenuItem, Select, TextField, Typography, Box, Divider, Button, Paper } from "@mui/material";
import { FaSearch } from "react-icons/fa";
import { displayInlineFlex, margin2, marginBottom2, marginTop2dot5, padding2, paddingBottom2 } from '../../css/styling';
import { AuthContext } from '../../contexts';
import { LoginUtils } from '../../utils';
import { useEffect, useState } from 'react';
import { UserService } from '../../services';
import { IRouteModel } from '../../interfaces/IRouteModel';
import { useNavigate } from 'react-router-dom';

function Routes() {
    const {state} = AuthContext.useLogin();
    const [userRoutes, setUserRoutes] = useState<IRouteModel[]>([]);
    const userId = LoginUtils.getUserId(state.token);
    const navigate = useNavigate();

    useEffect(() => {
        (async () => {
            var response = await UserService.GetAllRoutesByUserId(userId);
            if (response.status === 200) {
                var json = await response.json();
                setUserRoutes(json);
            }
        })();
    }, []);

    const navigateToRoutePage = (routeId: string) => {
        navigate(`/routes/${routeId}`);
    }

    const navigateToRouteEditPage = (routeId: string) => {
        navigate(`/routes/${routeId}/edit`);
    }

    return <>
        <Grid container spacing={6}>
            <Grid item md={3}>
                <Paper elevation={3} css={padding2}>
                    <Typography variant='h5' css={margin2}>Search Routes</Typography>
                    <div css={margin2}>
                        <TextField
                            id="routeSearch"
                            label="Search"
                            type="search"
                            fullWidth
                            variant="filled"
                            InputProps={{
                                startAdornment: (
                                    <InputAdornment position="start">
                                        <FaSearch/>
                                    </InputAdornment>
                                ),
                            }}
                        />
                    </div>
                    <Typography css={margin2} variant='h6'>Route Type Filters</Typography>
                    <div css={margin2}>
                        <Select value={3} id='type-select' fullWidth>
                            <MenuItem value={1}>Cycling</MenuItem>
                            <MenuItem value={2}>Running</MenuItem>
                            <MenuItem value={3}>All</MenuItem>
                        </Select>
                    </div>
                    <Typography variant='h6' css={margin2}>Distance Filters</Typography>
                    <div css={margin2}>
                        <TextField fullWidth label='Minimum Length'></TextField>
                    </div>
                    <div css={margin2}>
                        <TextField fullWidth label='Maximum Length'></TextField>
                    </div>
                    <div css={margin2}>
                        <TextField fullWidth label='Minimum Elevation'></TextField>
                    </div>
                    <div css={margin2}>
                        <TextField fullWidth label='Maximum Elevation'></TextField>
                    </div>
                </Paper>
            </Grid>
            <Grid item md={8}>
                <Paper elevation={3} css={padding2}>
                    <Box sx={{height: '800px', overflowY: 'auto'}}>
                        <Typography variant='h3'>My Routes</Typography>
                        {userRoutes.map((route) => (
                            <div key={route.id}>
                                <Typography variant='h4'>{route.routeName}</Typography>
                                <Typography>{route.type.name} Route</Typography>
                                <div css={[marginBottom2, marginTop2dot5]}>
                                    <Button onClick={() => navigateToRoutePage(route.id)} variant='contained' sx={{marginRight: '1%'}} size='large'>View</Button>
                                    <Button onClick={() => navigateToRouteEditPage(route.id)} variant='contained' size='large'>Edit</Button>
                                </div>
                                <Divider></Divider>
                            </div>
                        ))}
                    </Box>
                </Paper>
            </Grid>
        </Grid>
    </>
}

export default Routes;