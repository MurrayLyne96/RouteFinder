/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import { EmotionJSX } from '@emotion/react/types/jsx-namespace';
import { Box, Button, Grid, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import Typography from '@mui/material/Typography';
import { dashboardRightSide, map, marginBottom2, formGroup, formGroupFullWidth, marginBottom3dot8, createRouteHeader, createMap, createRouteSubHeadings } from '../../css/styling';
import React from 'react';
import { IRouteDetailModel } from '../../interfaces/IRouteDetailModel';
import { GOOGLE_API_KEY } from '../../constants/keys';
import GoogleMapReact from 'google-map-react';
import { useNavigate } from 'react-router-dom';
import { MapService, RoutesService } from '../../services';
import { IPlotPointCreateModel } from '../../interfaces/IPlotPointCreateModel';
import { IRouteCreateModel } from '../../interfaces/IRouteCreateModel';
import { toast } from 'react-hot-toast';
import { LoginUtils } from '../../utils';
import { AuthContext } from '../../contexts';

function CreateRoute() : EmotionJSX.Element {
    const {state} = AuthContext.useLogin();
    const [route, setRoute] = React.useState<IRouteCreateModel>({name: '', typeId: 2, userId: LoginUtils.getUserId(state.token), plotPoints: []});
    const [startLocation, setStartLocation] = React.useState('');
    const [endLocation, setEndLocation] = React.useState('');
    const [routeMap, setRouteMap] = React.useState<google.maps.Map>();
    const [DirectionsService, setDirectionsService] = React.useState<google.maps.DirectionsService>();
    const [DirectionsRenderer, setDirectionsRenderer] = React.useState<google.maps.DirectionsRenderer>();
    const [GeoCoder, setgGeoCoder] = React.useState<google.maps.Geocoder>();
    const navigate = useNavigate();
    
    const handleTypeChange = (event: any) => {
        setRoute(existingValues => (
            {
                ...existingValues,
                typeId: event.target.value
            }
        ));
    }

    const handleNameChange = (event: any) => {
        setRoute(existingValues => (
            {
                ...existingValues,
                name: event.target.value
            }
        ));
    }

    const handleApiLoaded = async(map: any) => {
        setRouteMap(map.map);
        setDirectionsService(new map.maps.DirectionsService());
        setgGeoCoder(new map.maps.Geocoder())
        setDirectionsRenderer(new map.maps.DirectionsRenderer());
    };

    const generateRoute = async() => {
        var originResponse = await GeoCoder?.geocode({
            address: startLocation
        });

        var destinationResponse = await GeoCoder?.geocode({
            address: endLocation
        });

        let originLatLng : google.maps.LatLng;
        let destinationLatLng : google.maps.LatLng;

        if (originResponse != undefined && destinationResponse != undefined && DirectionsService != undefined && DirectionsRenderer != undefined && routeMap != undefined) {
            let originLat = originResponse.results[0].geometry.location.lat();
            let originLng = originResponse.results[0].geometry.location.lng();
            originLatLng = new google.maps.LatLng(originLat, originLng);

            let destinationLat = destinationResponse.results[0].geometry.location.lat();
            let destinationLng = destinationResponse.results[0].geometry.location.lng();
            destinationLatLng = new google.maps.LatLng(destinationLat, destinationLng);

            let response = await MapService.generateRouteWithStartAndFinish(originLatLng, destinationLatLng, DirectionsService, DirectionsRenderer, routeMap);

            if (response != undefined) {
                DirectionsRenderer.setMap(routeMap);
                DirectionsRenderer.setDirections(response);
                createPlotPointsFromSprintRoute(originLat, originLng, destinationLat, destinationLng);
            }
        }
    }

    const createPlotPointsFromSprintRoute = (originLat: number, originLng: number, destinationLat: number, destinationLng: number) => {
        //Since this is a single sprint from A to B, set 2 Plotpoints.
        let originPlotPoint : IPlotPointCreateModel = {
            xCoordinate: originLat,
            yCoordinate: originLng,
            description: startLocation,
            plotOrder: 0
        }

        let destinationPlotPoint : IPlotPointCreateModel = {
            xCoordinate: destinationLat,
            yCoordinate: destinationLng,
            description: endLocation,
            plotOrder: 1
        }

        let plotpoints = [originPlotPoint, destinationPlotPoint];

        setRoute(existingValues => (
            {
                ...existingValues,
                plotPoints: plotpoints
            }
        ));
    }

    const saveRoute = async() => {
        let response = await RoutesService.createRoute(route);
        console.log(response);
        if (response.status == 201) {
            toast.success('Route created');
        } else {
            toast.error('There was a problem creating your route')
        }
    }

    const defaultProps = {
        center: {
          lat: 0,
          lng: 0
        },
        zoom: 1
    };

    return (
        <Box>
            <Typography variant='h3' css={createRouteHeader}>Create Route</Typography>
            <Grid container spacing={1}>
                <Grid item md={3}>
                    <Typography variant='h4' css={createRouteSubHeadings}>Route Details</Typography>
                    <div css={formGroupFullWidth}>
                        <TextField margin='dense' value={route.name} onChange={(e) => handleNameChange(e)} label='Route Name'></TextField>
                    </div>
                    <InputLabel id="type-select">Route Type</InputLabel>
                    <Select value={route?.typeId != undefined ? route?.typeId : 1} id='type-select' css={marginBottom2} fullWidth onChange={handleTypeChange} margin='dense'>
                        <MenuItem value={1}>Running</MenuItem>
                        <MenuItem value={2}>Cycling</MenuItem>
                    </Select>
                    <Button variant='contained' size='large' onClick={saveRoute}>Save Route</Button>
                </Grid>
                <Grid item md={3}>
                    <Typography variant='h4' css={createRouteSubHeadings}>Route Generation</Typography>
                    <div css={[formGroupFullWidth, marginBottom3dot8]}>
                        <TextField margin='dense' value={startLocation} onChange={(e) => setStartLocation(e.target.value)} label='Start Location' placeholder='Address, location etc'></TextField>
                    </div>
                    <div css={formGroupFullWidth}>
                        <TextField margin='dense' value={endLocation} onChange={(e) => setEndLocation(e.target.value)} label='End Location' placeholder='Address, location etc'></TextField>
                    </div>
                    <Button variant='contained' size='large' onClick={generateRoute}>Generate Route</Button>
                </Grid>
                <Grid item md={6}>
                    <div css={dashboardRightSide}>
                        <Typography variant='h4'>Preview</Typography>
                        <div css={createMap}>
                        <GoogleMapReact
                            bootstrapURLKeys={{ key: GOOGLE_API_KEY }}
                            defaultCenter={defaultProps.center}
                            defaultZoom={defaultProps.zoom}
                            yesIWantToUseGoogleMapApiInternals
                            onGoogleApiLoaded={(map: any) => handleApiLoaded(map)}
                        />
                        </div>
                    </div>
                </Grid>
            </Grid>
        </Box>
    );
}

export default CreateRoute;