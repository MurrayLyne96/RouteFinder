/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react';
import * as React from 'react'
import { Link, useParams } from 'react-router-dom'
import { RoutesService } from '../../services';
import { IRouteDetailModel } from '../../interfaces/IRouteDetailModel';
import { Button, Grid, Paper, Typography } from '@mui/material';
import { FaBackspace } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';
import { GOOGLE_API_KEY } from '../../constants/keys';
import { defaultProps } from '../../constants/settings';
import { margin1dot5, paddingBottom2, paddingTop05, dashboardRightSide, flex, createNewMapButton, map, createMap, viewMap, padding2, margin2, margin3 } from '../../css/styling';
import MapService from '../../services/map';
import GoogleMapReact from 'google-map-react';
import toast from 'react-hot-toast';

function Route() {
    const navigate = useNavigate();
    const {routeId} = useParams();
    const [route, setRoute] = React.useState<IRouteDetailModel>();
    const [routeMap, setRouteMap] = React.useState<google.maps.Map>();
    const [DirectionsService, setDirectionsService] = React.useState<google.maps.DirectionsService>();
    const [DirectionsRenderer, setDirectionsRenderer] = React.useState<google.maps.DirectionsRenderer>();

    const navigateToRouteEditPage = (routeId: string | undefined) => {
        if (routeId != undefined) {
            navigate(`/routes/${routeId}/edit`);
        }
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

                console.log(response);
            } else if (route.plotPoints.length > 2) {
                //Otherwise, create waypoints for everything after the first one and before the last one.
            } else {
                //invalid route, warn the user.
                toast.error('This route cannot be loaded as it only has a single startpoint.');
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
        
    }, [routeId]);

    React.useEffect(() => {
        (async () => {
            await setMapRoute();
        })()
        
    }, [route, DirectionsRenderer, DirectionsService, DirectionsRenderer, routeMap]);

    return (
        <>
            <Grid container spacing={2}>
                <Grid item md={4}>
                    <Paper css={[margin3, padding2]}>
                        <Typography variant='h4'><Link to={"/routes"}><FaBackspace/></Link></Typography>
                        <Typography variant='h3'>{route?.routeName}</Typography>
                        <Typography>{route?.type.name} Route</Typography>
                        <Button variant='contained' size='large' onClick={() => navigateToRouteEditPage(route?.id)}>Edit</Button>
                    </Paper>
                </Grid>
                <Grid item md={8}>
                    <Paper elevation={3} css={[margin1dot5, padding2]}>
                        <div css={[paddingBottom2, paddingTop05]}>
                            <div css={flex}>
                                <Typography variant='h6'>Route</Typography>
                                <div css={createNewMapButton}>
                                    <Button variant='contained' size='large' onClick={navigateToRouteCreatePage}>Create New Route</Button>
                                </div>
                            </div>
                            <div css={viewMap}>
                                <GoogleMapReact
                                bootstrapURLKeys={{ key: GOOGLE_API_KEY }}
                                defaultCenter={defaultProps.center}
                                defaultZoom={defaultProps.zoom}
                                yesIWantToUseGoogleMapApiInternals
                                onGoogleApiLoaded={(map: any) => handleApiLoaded(map)}
                                />
                            </div>
                        </div>
                    </Paper>
                </Grid>
            </Grid>
        </>
    );
}

export default Route;