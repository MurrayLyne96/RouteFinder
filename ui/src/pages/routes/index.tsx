/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react';
import { Checkbox, FormControlLabel, Grid, InputAdornment, MenuItem, Select, TextField, Typography, Box, Divider, Button, Paper, Card } from "@mui/material";
import { FaSearch } from "react-icons/fa";
import { displayInlineFlex, margin1dot5, margin2, marginBottom2, marginTop2dot5, padding2, paddingBottom2, marginRight2, fullWidth } from '../../css/styling';
import { AuthContext } from '../../contexts';
import { LoginUtils } from '../../utils';
import { useEffect, useState } from 'react';
import { UserService } from '../../services';
import { IRouteModel } from '../../interfaces/IRouteModel';
import { useNavigate } from 'react-router-dom';
import { ROUTE_TYPES } from '../../constants/identifiers';
import { EditButton } from '../../components';

function Routes() {
    const {state} = AuthContext.useLogin();
    const [userRoutes, setUserRoutes] = useState<IRouteModel[]>([]);
    const userId = LoginUtils.getUserId(state.token);
    const [searchQuery, setSearchQuery] = useState<string>('');
    const [routeTypeFilters, setRouteTypeFilters] = useState<number>(ROUTE_TYPES.ALL);
    const [allRoutes, setAllRoutes] = useState<IRouteModel[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        (async () => {
            var response = await UserService.GetAllRoutesByUserId(userId);
            if (response.status === 200) {
                var json = await response.json();
                setUserRoutes(json);
                setAllRoutes(json);
            }
        })();
    }, []);

    useEffect(() => {
        filterRoutes();
    }, [searchQuery, routeTypeFilters]);

    const ShowEditButton = (model: IRouteModel) => {
        return(<EditButton route={model}></EditButton>)
    }

    const filterRoutes = () => {
        if (allRoutes != undefined && searchQuery != undefined && userId != undefined) {
          if (searchQuery == '') {
            setUserRoutes(allRoutes);
            let filteredRoutesByType = routeTypeFilters == ROUTE_TYPES.ALL ? allRoutes : allRoutes.filter(item => item.typeId == routeTypeFilters);
            setUserRoutes(filteredRoutesByType);
          
          } else {
            let filteredRoutes = allRoutes.filter(item => item.routeName.toLowerCase().includes(searchQuery.toLowerCase()));
    
            if (filteredRoutes != undefined) {
                setUserRoutes(filteredRoutes);
                let filteredRoutesByType = routeTypeFilters == ROUTE_TYPES.ALL ? filteredRoutes : filteredRoutes.filter(item => item.typeId == routeTypeFilters);
                setUserRoutes(filteredRoutesByType);
            } else {
                setUserRoutes(allRoutes);
                let filteredRoutesByType = routeTypeFilters == ROUTE_TYPES.ALL ? allRoutes : allRoutes.filter(item => item.typeId == routeTypeFilters);
                setUserRoutes(filteredRoutesByType);
            }
          }
        }
      }

    const navigateToRoutePage = (routeId: string) => {
        navigate(`/routes/${routeId}`);
    }

    return <>
        <Grid container spacing={6}>
            <Grid item md={3} sm={12} xs={12}>
                <Paper elevation={3} css={[padding2, margin2]}>
                    <Typography variant='h5' css={margin2}>Search Routes</Typography>
                    <div css={margin2}>
                        <TextField
                            id="routeSearch"
                            label="Search"
                            type="search"
                            fullWidth
                            value={searchQuery}
                            onChange={(e) => setSearchQuery(e.target.value)}
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
                        <Select value={routeTypeFilters} onChange={(e) => setRouteTypeFilters(Number(e.target.value))} id='type-select' fullWidth>
                            <MenuItem value={ROUTE_TYPES.CYCLING}>Cycling</MenuItem>
                            <MenuItem value={ROUTE_TYPES.RUNNING}>Running</MenuItem>
                            <MenuItem value={ROUTE_TYPES.ALL}>All</MenuItem>
                        </Select>
                    </div>
                </Paper>
            </Grid>
            <Grid item md={9} sm={12}>
                <Paper elevation={3} css={[padding2, marginRight2]}>
                    <Box sx={{height: '800px', overflowY: 'auto'}}>
                        <Typography variant='h3'>My Routes</Typography>
                        {userRoutes.map((route) => (
                            <Card elevation={5} key={route.id} id={`${route.id}-route-div`} css={[margin2, padding2]}>
                                <Typography variant='h4'>{route.routeName}</Typography>
                                <Typography>{route.type.name} Route</Typography>
                                <div css={[marginBottom2, marginTop2dot5]}>
                                    <Button onClick={() => navigateToRoutePage(route.id)} variant='contained' sx={{marginRight: '1%'}} size='large'>View</Button>
                                    {route.userId == userId && ShowEditButton(route)}
                                </div>
                            </Card>
                        ))}
                    </Box>
                </Paper>
            </Grid>
        </Grid>
    </>
}

export default Routes;