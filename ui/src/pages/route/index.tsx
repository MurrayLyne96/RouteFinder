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
import { margin1dot5, paddingBottom2, paddingTop05, dashboardRightSide, flex, createNewMapButton, map, createMap, viewMap, padding2, margin2, margin3, marginTop2dot5 } from '../../css/styling';
import MapService from '../../services/map';
import GoogleMapReact from 'google-map-react';
import toast from 'react-hot-toast';
import { IRouteInfoModel } from '../../interfaces/IRouteInfoModel';
import dayjs from 'dayjs';

function Route() {
    const navigate = useNavigate();
    const {routeId} = useParams();
    const [route, setRoute] = React.useState<IRouteDetailModel>();
    const [routeMap, setRouteMap] = React.useState<google.maps.Map>();
    const [DirectionsService, setDirectionsService] = React.useState<google.maps.DirectionsService>();
    const [ElevationService, setElevationService] = React.useState<google.maps.ElevationService>();
    const [DirectionsRenderer, setDirectionsRenderer] = React.useState<google.maps.DirectionsRenderer>();
    const [routeInfo, setRouteInfo] = React.useState<IRouteInfoModel>(
        {
            totalDistance : '',
            totalElevation: '',
            timeToComplete: '',
            lowestElevation: '',
            highestElevation: ''
        }
    );

    const navigateToRouteEditPage = (routeId: string | undefined) => {
        if (routeId != undefined) {
            navigate(`/routes/${routeId}/edit`);
        }
    }

    const handleApiLoaded = async(map: any) => {
        setRouteMap(map.map);
        setDirectionsService(new map.maps.DirectionsService());
        setDirectionsRenderer(new map.maps.DirectionsRenderer());
        setElevationService(new map.maps.ElevationService());
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
                    await setRouteInfoFromResponse(response);
                }

                console.log(response);
            } else if (route.plotPoints.length > 2) {
                //Otherwise, create waypoints for everything after the first one and before the last one. (TODO)
            } else {
                //invalid route, warn the user.
                toast.error('This route cannot be loaded as it only has a single startpoint.');
            }
        }
    }

    const setRouteInfoFromResponse = async(response: google.maps.DirectionsResult) => {
        let routeInfo : IRouteInfoModel = {
            totalDistance : '',
            totalElevation: '',
            timeToComplete: '',
            lowestElevation: '',
            highestElevation: ''
        };

        let totalDistance : number = 0;
        let timeToComplete : number = 0;

        response.routes[0].legs.forEach(leg => {
            totalDistance += leg.distance?.value ?? 0;
            timeToComplete += leg.duration?.value ?? 0;
        });

        routeInfo.totalDistance = (totalDistance / 1000).toFixed(2);
        let timeToCompleteInHours = timeToComplete / 3600;
        let minutesToComplete = (timeToCompleteInHours % 1) * 60;

        routeInfo = await getElevationData(response.routes[0].overview_path, routeInfo);
        
        routeInfo.timeToComplete = `${timeToCompleteInHours.toFixed(0)} Hours and ${minutesToComplete.toFixed(0)} Minutes.`;
        setRouteInfo(routeInfo);
    }

    const getElevationData = async(pathData : google.maps.LatLng[], model: IRouteInfoModel) : Promise<IRouteInfoModel> => {
        var response = await ElevationService?.getElevationAlongPath({
            path: pathData,
            samples: 50
        });

        let totalElevation : number = 0;
        let highestElevation : number = 0;
        let lowestElevation : number = 0;
        let previousElevation: number = 0;
        let currentElevation: number = 0;

        response?.results.forEach(elevationSample => {
            if (currentElevation >= highestElevation) {
                highestElevation = currentElevation;
            }

            if (currentElevation <= lowestElevation || lowestElevation == 0) {
                lowestElevation = currentElevation;
            }

            currentElevation = elevationSample.elevation;
            let elevationChange = currentElevation - previousElevation;
            totalElevation += elevationChange;
            previousElevation = elevationSample.elevation;
        });

        model.totalElevation = `${totalElevation.toFixed(2)} meters`;
        model.lowestElevation = `${lowestElevation.toFixed(2)} meters`;
        model.highestElevation = `${highestElevation.toFixed(2)} meters`;

        return model;  
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
                        <Typography variant='h5' css={marginTop2dot5}>Route Details</Typography>
                        <Typography variant='h6'>Distance</Typography>
                        <Typography>{routeInfo?.totalDistance} KM</Typography>
                        <Typography variant='h6'>Total Elevation Change</Typography>
                        <Typography>{routeInfo?.totalElevation}</Typography>
                        <Typography variant='h6'>Highest Elevation</Typography>
                        <Typography>{routeInfo?.highestElevation}</Typography>
                        <Typography variant='h6'>Lowest Elevation</Typography>
                        <Typography>{routeInfo?.lowestElevation}</Typography>
                        <Typography variant='h6'>Estimated time to complete</Typography>
                        <Typography>{routeInfo?.timeToComplete}</Typography>
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