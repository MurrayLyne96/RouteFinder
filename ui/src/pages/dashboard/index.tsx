/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import * as React from 'react';
import { styled, useTheme } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import CssBaseline from '@mui/material/CssBaseline';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import { Button, Checkbox, FormControlLabel, FormGroup, Grid, InputAdornment, MenuItem, Select, TextField } from '@mui/material';
import { FaSearch } from 'react-icons/fa';
import { CheckBox } from '@mui/icons-material';
import { createNewMapButton, displayInlineFlex, map, margin2, marginBottom2, marginLeft1, marginLeft15, marginLeft5, marginRight2, dashboardRightSide, routeInfo, noMargins, flex } from '../../css/styling';
import { RoutesService } from '../../services';
import { IRouteModel } from '../../interfaces/IRouteModel';
import { AuthContext } from '../../contexts';
import { LoginUtils } from '../../utils';
import { useNavigate } from 'react-router-dom';
import { GOOGLE_API_KEY } from '../../constants/keys';
import GoogleMapReact from 'google-map-react';
import { Marker } from '../../components';
import { IRouteDetailModel } from '../../interfaces/IRouteDetailModel';
import toast from 'react-hot-toast';
const drawerWidth = 500;

const Main = styled('main', { shouldForwardProp: (prop) => prop !== 'open' })<{
  open?: boolean;
  }>(({ theme, open }) => ({
    flexGrow: 1,
    transition: theme.transitions.create('margin', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    marginLeft: `-${drawerWidth}px`,
    ...(open && {
      transition: theme.transitions.create('margin', {
        easing: theme.transitions.easing.easeOut,
        duration: theme.transitions.duration.enteringScreen,
      }),
      marginLeft: 0,
    }),
  }));

const DrawerHeader = styled('div')(({ theme }) => ({
  display: 'flex',
  alignItems: 'center',
  padding: theme.spacing(0, 1),
  // necessary for content to be below app bar
  ...theme.mixins.toolbar,
  justifyContent: 'flex-end',
}));

export default function PersistentDrawerLeft() {
  const theme = useTheme();
  const [open, setOpen] = React.useState(false);
  const [routes, setRoutes] = React.useState<IRouteModel[]>([]);
  const [selectedRoute, setSelectedRoute] = React.useState<IRouteDetailModel>();
  const [routeMiles, setSelectedRouteMiles] = React.useState<String>('');
  const [routeMap, setRouteMap] = React.useState<google.maps.Map>();
  const [DirectionsService, setDirectionsService] = React.useState<google.maps.DirectionsService>();
  const [DirectionsRenderer, setDirectionsRenderer] = React.useState<google.maps.DirectionsRenderer>();
  const navigate = useNavigate();

  const defaultProps = {
    center: {
      lat: 0,
      lng: 0
    },
    zoom: 11
  };

  React.useEffect(() => {
    (async () => {
      await GetRoutes();
    })();
  }, []);

  const handleApiLoaded = async(map: any) => {
      setRouteMap(map.map);
      setDirectionsService(new map.maps.DirectionsService());
      setDirectionsRenderer(new map.maps.DirectionsRenderer());
  };

  const handleDrawerOpen = () => {
    setOpen(!open);
  };

  const GetRoutes = async () => {
    var result = await RoutesService.getAllRoutes();
    var json = await result.json();
    setRoutes(json);
  }

  const navigateToRoutePage = (routeId: string) => {
      navigate(`/routes/${routeId}`);
  } 

  const navigateToRouteEditPage = (routeId: string) => {
      navigate(`/routes/${routeId}/edit`);
  }

  const navigateToRouteCreatePage = (routeId: string) => {
    navigate(`/routes/${routeId}/edit`);
  }

  const ShowMap = async(routeId: string, event: any) => {
    var response = await RoutesService.getRouteById(routeId);
    let routeToBeSelected : IRouteDetailModel = await response.json();
    
    //If there are only 2 plotpoints, Generate a route using the origin and destination.
    if (routeToBeSelected.plotPoints.length == 2) {
      let origin : google.maps.LatLng = new google.maps.LatLng(routeToBeSelected.plotPoints[0].xCoordinate, routeToBeSelected.plotPoints[0].yCoordinate);
      let destination: google.maps.LatLng = new google.maps.LatLng(routeToBeSelected.plotPoints[1].xCoordinate, routeToBeSelected.plotPoints[1].yCoordinate);
      
      await generateRouteWithStartAndFinish(origin, destination);
      setSelectedRoute(routeToBeSelected);

    } else if (routeToBeSelected.plotPoints.length > 2) {
      //Otherwise, create waypoints for everything after the first one and before the last one.
    } else {
      //invalid route, warn the user.
      toast.error('This route cannot be loaded as it only has a single startpoint.');
    }
  }

  const generateRouteWithStartAndFinish = async(origin: google.maps.LatLng, destination: google.maps.LatLng) => {
    if (DirectionsService != undefined && DirectionsRenderer != undefined && routeMap != undefined) {
      var response = await DirectionsService.route({
          origin: origin,
          destination: destination,
          optimizeWaypoints: true,
          travelMode: google.maps.TravelMode.BICYCLING,
      });

      DirectionsRenderer.setMap(routeMap);
      DirectionsRenderer.setDirections(response);

      let distanceLegs = response.routes[0].legs;
      let totalDistance : number = 0;

      distanceLegs.forEach(function (leg) {
        totalDistance += leg.distance?.value ?? 0;
      });

      totalDistance = totalDistance / 1000;

      setSelectedRouteMiles(`Distance: ${totalDistance} km`);
    }
  }

  const selectedRouteInfo = () => {
    return(
      <div css={routeInfo}>
        <Typography variant='h4' css={noMargins}>Selected Route: <b>{selectedRoute?.routeName}</b></Typography>
        <Typography>{routeMiles}</Typography>
      </div>
    )
  }

  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <Drawer
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          '& .MuiDrawer-paper': {
            width: drawerWidth,
            boxSizing: 'border-box',
            marginTop: '71.5px'
          },
        }}
        variant="persistent"
        anchor="left"
        open={open}
      >
        <Divider />
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
        <div css={margin2}>
            <FormControlLabel control={<Checkbox/>} label="Select my own routes only"></FormControlLabel>
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
      </Drawer>

      <Main open={open}>
        <Grid container spacing={1}>
            <Grid item md={5}>
            <Box css={{display: 'inline-flex'}}>
                <IconButton onClick={handleDrawerOpen}>
                    {open ? <ChevronLeftIcon /> : <ChevronRightIcon />}
                </IconButton>
                <Typography variant='h4'>Routes</Typography>
            </Box>
            {routes.map((route) => (
              <div key={route.id} id={`${route.id}-route-div`}>
                <Box sx={{marginLeft: '5%'}}>
                  <Typography variant='h3'>{route.routeName}</Typography>
                  <Typography>{route.type.name} Route</Typography>
                  <div css={{displayInlineFlex}}>
                    <Button variant='contained' sx={{marginRight: '1%'}} size='large' onClick={() => navigateToRoutePage(route.id)}>View</Button>
                    <Button variant='contained' size='large' sx={{marginRight: '1%'}} onClick={() => navigateToRouteEditPage(route.id)}>Edit</Button>
                    <Button variant='contained' size='large' onClick={(e) => ShowMap(route.id, e)}>Preview Map</Button>
                  </div>
                </Box>
              </div>
            ))}
            </Grid>
            <Grid item md={7}>
              <div css={dashboardRightSide}>
                <div css={flex}>
                  {selectedRoute != undefined && selectedRouteInfo()}
                  <div css={createNewMapButton}>
                    <Button variant='contained' size='large'>Create New Route</Button>
                  </div>
                </div>
                <div css={map}>
                  <GoogleMapReact
                    bootstrapURLKeys={{ key: GOOGLE_API_KEY }}
                    defaultCenter={defaultProps.center}
                    defaultZoom={defaultProps.zoom}
                    yesIWantToUseGoogleMapApiInternals
                    onGoogleApiLoaded={(map: any, maps: any) => handleApiLoaded(map)}
                  >
                  </GoogleMapReact>
                </div>
              </div>
            </Grid>
        </Grid>
      </Main>
    </Box>
  );
}