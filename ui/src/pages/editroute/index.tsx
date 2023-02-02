/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react';
import * as React from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { MapService, RoutesService } from '../../services';
import { IRouteDetailModel } from '../../interfaces/IRouteDetailModel';
import { Button, Grid, Typography, TextField, MenuItem, Select, InputLabel, Paper } from '@mui/material';
import { FaBackspace } from 'react-icons/fa';
import { createNewMapButton, dashboardRightSide, flex, map, margin1dot5, margin2, margin3, marginBottom2, paddingBottom2, paddingTop05, padding2 } from '../../css/styling';
import { ITypeModel } from '../../interfaces/ITypeModel';
import { IRouteUpdateModel } from '../../interfaces/IRouteUpdateModel';
import toast from 'react-hot-toast';
import { GOOGLE_API_KEY } from '../../constants/keys';
import GoogleMapReact from 'google-map-react';
import { defaultProps } from '../../constants/settings';

function EditRoute() {
    const {routeId} = useParams();
    const [route, setRoute] = React.useState<IRouteDetailModel>({id: '', routeName: '', typeId: 0, type: {id: 0, name: ''}, userId: '', plotPoints: []});
    const [routeMap, setRouteMap] = React.useState<google.maps.Map>();
    const [DirectionsService, setDirectionsService] = React.useState<google.maps.DirectionsService>();
    const [DirectionsRenderer, setDirectionsRenderer] = React.useState<google.maps.DirectionsRenderer>();
    const navigate = useNavigate();

    const handleTypeChange = (event: any) => {
        setRoute(existingValues => (
            {
                ...existingValues,
                typeId: event.target.value
            }
        ));
    }

    const handleApiLoaded = async(map: any) => {
        setRouteMap(map.map);
        setDirectionsService(new map.maps.DirectionsService());
        setDirectionsRenderer(new map.maps.DirectionsRenderer());
    };

    const navigateToRouteCreatePage = () => {
        navigate(`/routes/create`);
    }

    const setMapRoute = async() => {
        //If there are only 2 plotpoints, Generate a route using the origin and destination.
        if (route != undefined && routeMap != undefined && DirectionsService != undefined && DirectionsRenderer != undefined) {
            if (route.plotPoints.length == 2) {
                let origin : google.maps.LatLng = new google.maps.LatLng(route.plotPoints[0].xCoordinate, route.plotPoints[0].yCoordinate);
                let destination: google.maps.LatLng = new google.maps.LatLng(route.plotPoints[1].xCoordinate, route.plotPoints[1].yCoordinate);
                
                var response = await MapService.generateRouteWithStartAndFinish(origin, destination, DirectionsService, DirectionsRenderer, routeMap);
                if (response != undefined) {
                    DirectionsRenderer.setMap(routeMap);
                    DirectionsRenderer.setDirections(response);
                }
            } else if (route.plotPoints.length > 2) {
                //Otherwise, create waypoints for everything after the first one and before the last one.
            } else if (route.plotPoints.length == 1) {
                //invalid route, warn the user.
                toast.error('This route cannot be loaded as it only has a single startpoint.');
            }
        }
    }

    const handleRouteNameChange = (event: any) => {
        setRoute(existingValues => (
            {
                ...existingValues,
                routeName: event.target.value
            }
        ));
    }
    
    const updateRoute = async() => {
        if (routeId != undefined) {

            let updateModel: IRouteUpdateModel = {
                name: route.routeName,
                typeId: route.typeId
            }

            var response = await RoutesService.updateRoute(updateModel, routeId);
            if (response.status == 204) {
                toast.success("Route updated successfully");
            } else {
                var json = await response.json();
                toast.error("There was a problem updating your route, please check you've set your fields correctly.")
            }
        }
    }

    React.useEffect(() => {
        (async () => {
            if (routeId != undefined) {
                var response = await RoutesService.getRouteById(routeId);
                if (response.status == 200) {
                    var json = await response.json();
                    setRoute(json);
                    await setMapRoute();
                }
            }
        })()
        
    }, []);

    React.useEffect(() => {
        (async () => {
            await setMapRoute();
        })()
        
    }, [route, DirectionsRenderer, DirectionsService, DirectionsRenderer, routeMap]);

    return (
        <>
            <Grid container spacing={2}>
                <Grid item md={4}>
                    <Paper elevation={3} css={[margin3, padding2]}>
                        <Typography variant='h4'><Link to={"/routes"}><FaBackspace/></Link></Typography>
                        <Typography variant='h3'>Edit Route</Typography>
                        <div css={margin2}>
                            <TextField value={route?.routeName} onChange={handleRouteNameChange} fullWidth css={marginBottom2} label='Route Name'></TextField>
                            <InputLabel id="type-select">Route Type</InputLabel>
                            <Select value={route?.typeId != undefined ? route?.typeId : 1} id='type-select' css={marginBottom2} fullWidth onChange={handleTypeChange} margin='dense'>
                                <MenuItem value={1}>Running</MenuItem>
                                <MenuItem value={2}>Cycling</MenuItem>
                            </Select>
                        </div>
                        <Button variant='contained' onClick={updateRoute} size='large'>Save</Button>
                    </Paper>
                </Grid>
                <Grid item md={8}>
                    <Paper elevation={3} css={margin1dot5}>
                        <div css={[paddingBottom2, paddingTop05]}>
                            <div css={dashboardRightSide}>
                                <div css={flex}>
                                    <Typography variant='h6'>Route</Typography>
                                    <div css={createNewMapButton}>
                                        <Button variant='contained' size='large' onClick={navigateToRouteCreatePage}>Create New Route</Button>
                                    </div>
                                </div>
                                <div css={map}>
                                    <GoogleMapReact
                                    bootstrapURLKeys={{ key: GOOGLE_API_KEY }}
                                    defaultCenter={defaultProps.center}
                                    defaultZoom={defaultProps.zoom}
                                    yesIWantToUseGoogleMapApiInternals
                                    onGoogleApiLoaded={(map: any) => handleApiLoaded(map)}
                                    />
                                </div>
                            </div>
                        </div>
                    </Paper>
                </Grid>
            </Grid>
        </>
    );
}

export default EditRoute;