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
import { Button, Card, Checkbox, FormControlLabel, FormGroup, Grid, InputAdornment, MenuItem, Paper, Select, TextField } from '@mui/material';
import { FaSearch } from 'react-icons/fa';
import { CheckBox } from '@mui/icons-material';
import { createNewMapButton, displayInlineFlex, map, margin2, marginBottom2, marginLeft1, marginLeft15, marginLeft5, marginRight2, dashboardRightSide, routeInfo, noMargins, flex, marginBottom3dot8, paddingBottom2, paddingTop05, margin1dot5, padding2, fullWidth } from '../../css/styling';
import { MapService, RoutesService } from '../../services';
import { IRouteModel } from '../../interfaces/IRouteModel';
import { AuthContext } from '../../contexts';
import { LoginUtils } from '../../utils';
import { useNavigate } from 'react-router-dom';
import { GOOGLE_API_KEY } from '../../constants/keys';
import GoogleMapReact from 'google-map-react';
import { Marker } from '../../components';
import { IRouteDetailModel } from '../../interfaces/IRouteDetailModel';
import toast from 'react-hot-toast';
import { defaultProps } from '../../constants/settings';
import { ROUTE_TYPES } from '../../constants/identifiers';
import EditButton from '../../components/editbutton/index';

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

export default function Dashboard() {
  const theme = useTheme();
  const [open, setOpen] = React.useState(false);

  const [routes, setRoutes] = React.useState<IRouteModel[]>([]);
  const [allRoutes, setAllRoutes] = React.useState<IRouteModel[]>([]);
  const [selectedRoute, setSelectedRoute] = React.useState<IRouteDetailModel>();
  const [routeMiles, setSelectedRouteMiles] = React.useState<String>('');

  const [searchQuery, setSearchQuery] = React.useState<string>('');
  const [userRoutesOnly, setUserRoutesOnly] = React.useState<boolean>(false);
  const [routeTypeFilters, setRouteTypeFilters] = React.useState<number>(ROUTE_TYPES.ALL);

  const [routeMap, setRouteMap] = React.useState<google.maps.Map>();
  const [DirectionsService, setDirectionsService] = React.useState<google.maps.DirectionsService>();
  const [DirectionsRenderer, setDirectionsRenderer] = React.useState<google.maps.DirectionsRenderer>();
  const navigate = useNavigate();

  const {state} = AuthContext.useLogin();
  const userId = LoginUtils.getUserId(state.token);

  const [mobileOpen, setMobileOpen] = React.useState(false);

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  React.useEffect(() => {
    (async () => {
      await GetRoutes();
    })();
  }, []);

  React.useEffect(() => {
    filterRoutes();
  }, [searchQuery, userRoutesOnly, routeTypeFilters]);

  const handleApiLoaded = async(map: any) => {
      setRouteMap(map.map);
      setDirectionsService(new map.maps.DirectionsService());
      setDirectionsRenderer(new map.maps.DirectionsRenderer());
  };

  const filterRoutes = () => {
    if (allRoutes != undefined && searchQuery != undefined && userRoutesOnly != undefined && userId != undefined) {
      if (searchQuery == '') {
        setRoutes(allRoutes);
        let filteredRoutesByType = routeTypeFilters == ROUTE_TYPES.ALL ? allRoutes : allRoutes.filter(item => item.typeId == routeTypeFilters);
        checkUserOnlyRoutes(filteredRoutesByType);
      
      } else {
        let filteredRoutes = allRoutes.filter(item => item.routeName.toLowerCase().includes(searchQuery.toLowerCase()));

        if (filteredRoutes != undefined) {
          setRoutes(filteredRoutes);
          let filteredRoutesByType = routeTypeFilters == ROUTE_TYPES.ALL ? filteredRoutes : filteredRoutes.filter(item => item.typeId == routeTypeFilters);
          checkUserOnlyRoutes(filteredRoutesByType);
        } else {
          setRoutes(allRoutes);
          let filteredRoutesByType = routeTypeFilters == ROUTE_TYPES.ALL ? allRoutes : allRoutes.filter(item => item.typeId == routeTypeFilters);
          checkUserOnlyRoutes(filteredRoutesByType);
        }

      }
    }
  }

  const checkUserOnlyRoutes = (routes : IRouteModel[]) => {
    if (userRoutesOnly) {
      let filteredRoutes = routes.filter(item => item.userId === userId);
      setRoutes(filteredRoutes);
    } else {
      setRoutes(routes);
    }
  }

  const handleDrawerOpen = () => {
    setOpen(!open);
  };

  const GetRoutes = async () => {
    var result = await RoutesService.getAllRoutes();
    var json = await result.json();
    setRoutes(json);
    setAllRoutes(json);
  }

  const navigateToRoutePage = (routeId: string) => {
    navigate(`/routes/${routeId}`);
  } 

  const navigateToRouteCreatePage = () => {
    navigate(`/routes/create`);
  }

  const ShowMap = async(routeId: string, event: any) => {
    var response = await RoutesService.getRouteById(routeId);
    let routeToBeSelected : IRouteDetailModel = await response.json();
    
    //If there are only 2 plotpoints, Generate a route using the origin and destination.
    if (routeToBeSelected.plotPoints.length == 2) {
      let origin : google.maps.LatLng = new google.maps.LatLng(routeToBeSelected.plotPoints[0].xCoordinate, routeToBeSelected.plotPoints[0].yCoordinate);
      let destination: google.maps.LatLng = new google.maps.LatLng(routeToBeSelected.plotPoints[1].xCoordinate, routeToBeSelected.plotPoints[1].yCoordinate);
      
      await generateRouteWithStartAndFinish(origin, destination, routeToBeSelected);
      setSelectedRoute(routeToBeSelected);

    } else if (routeToBeSelected.plotPoints.length > 2) {
      //Otherwise, create waypoints for everything after the first one and before the last one.
    } else {
      //invalid route, warn the user.
      toast.error('This route cannot be loaded as it only has a single startpoint.');
    }
  }

  const generateRouteWithStartAndFinish = async(origin: google.maps.LatLng, destination: google.maps.LatLng, route: IRouteDetailModel) => {
    if (DirectionsService != undefined && DirectionsRenderer != undefined && routeMap != undefined) {
      var response = await MapService.generateRouteWithStartAndFinish(origin, destination, DirectionsService, DirectionsRenderer, routeMap, route.typeId)
      if (response != undefined) {
        DirectionsRenderer.setMap(routeMap);
        DirectionsRenderer.setDirections(response);

        let distanceLegs = response.routes[0].legs;
        let totalDistance : number = 0;

        distanceLegs.forEach(function (leg) {
          totalDistance += leg.distance?.value ?? 0;
        });

        let totalDistanceRounded = (totalDistance / 1000).toFixed(2);

        setSelectedRouteMiles(`Distance: ${totalDistanceRounded} KM`);
      }
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

  const ShowEditButton = (model: IRouteModel) => {
    return(<EditButton route={model}></EditButton>)
  }

  const handleCheckBoxChange = (e: any) => {
    let value : boolean = e.target.value;
    setUserRoutesOnly(value);
  }

  const drawer = (
    <div>
      <Divider />
        <IconButton
          color="inherit"
          aria-label="open drawer"
          edge="start"
          onClick={handleDrawerToggle}
          sx={{ mr: 2, ml: 1, display: { md: 'none', sm: 'block', xs: 'block' } }}
        >
          <MenuIcon />
        </IconButton>
        <Typography variant='h4' css={margin2}>Search Routes</Typography>
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
        <div css={margin2}>
            <FormControlLabel control={<Checkbox value={userRoutesOnly} onChange={(e) => handleCheckBoxChange(e)}/>} label="Select my own routes only"></FormControlLabel>
        </div>
        <Typography css={margin2} variant='h6'>Route Type Filters</Typography>
        <div css={margin2}>
          <Select value={routeTypeFilters} onChange={(e) => setRouteTypeFilters(Number(e.target.value))} id='type-select' fullWidth>
              <MenuItem value={ROUTE_TYPES.CYCLING}>Cycling</MenuItem>
              <MenuItem value={ROUTE_TYPES.RUNNING}>Running</MenuItem>
              <MenuItem value={ROUTE_TYPES.ALL}>All</MenuItem>
          </Select>
        </div>
    </div>
  );

  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <Box
        component="nav"
        sx={{ width: { md: drawerWidth }, flexShrink: { sm: 0 } }}
        aria-label="mailbox folders"
      >
        <Drawer
          variant="temporary"
          open={mobileOpen}
          onClose={handleDrawerToggle}
          ModalProps={{
            keepMounted: true, // Better open performance on mobile.
          }}
          sx={{
            display: { xs: 'block', sm: 'block', md: 'none' },
            '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
          }}
        >
          {drawer}
        </Drawer>
        <Drawer
          variant="permanent"
          sx={{
            display: { xs: 'none', sm: 'none', md: 'block' },
            '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth, top: '72px' },
          }}
          open
        >
          {drawer}
        </Drawer>
      </Box>
      <Box component='main' sx={{ flexGrow: 1, p: 3, width: { sm: `calc(100% - ${drawerWidth}px)` } }}>
        <Grid container>
            <Grid item lg={5} md={12} css={fullWidth}>
              <IconButton
                color="inherit"
                aria-label="open drawer"
                edge="start"
                onClick={handleDrawerToggle}
                sx={{ mr: 2, display: { md: 'none', sm: 'block', xs: 'block' } }}
              >
                <MenuIcon />
              </IconButton>
              <Paper elevation={3} css={[margin2, padding2]}>
                <div css={paddingBottom2}>
                  <Box css={{display: 'inline-flex'}}>
                      <Typography variant='h6'>Routes</Typography>
                  </Box>
                  {routes.map((route) => (
                    <Card elevation={5} key={route.id} id={`${route.id}-route-div`} css={[margin2, padding2]}>
                      <Box sx={{marginLeft: '5%'}}>
                        <Typography variant='h5'>{route.routeName}</Typography>
                        <Typography>{route.type.name} Route</Typography>
                        <div css={{displayInlineFlex}}>
                          <Button variant='contained' sx={{marginRight: '1%'}} size='large' onClick={() => navigateToRoutePage(route.id)}>View</Button>
                          {route.userId == userId && ShowEditButton(route)}
                          <Button variant='contained' size='large' onClick={(e) => ShowMap(route.id, e)}>Preview Map</Button>
                        </div>
                      </Box>
                    </Card>
                  ))}
                </div>
              </Paper>
            </Grid>
            <Grid item lg={7} md={12} css={fullWidth}>
              <Paper elevation={3} css={margin1dot5}>
                  <div css={[paddingBottom2, paddingTop05]}>
                    <div css={dashboardRightSide}>
                      <div css={flex}>
                        {selectedRoute != undefined && selectedRouteInfo()}
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
      </Box>
    </Box>
  );
}