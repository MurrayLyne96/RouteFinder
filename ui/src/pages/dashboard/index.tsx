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
import { displayInlineFlex, margin2, marginBottom2, marginLeft1, marginLeft15, marginLeft5, marginRight2 } from '../../css/styling';
import { RoutesService } from '../../services';
import { IRouteModel } from '../../interfaces/IRouteModel';
import { AuthContext } from '../../contexts';
import { LoginUtils } from '../../utils';

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
  const handleDrawerOpen = () => {
    setOpen(!open);
  };

  const routesResponse = async () => {
    var result = await RoutesService.getAllRoutes();
    var json = await result.json();
    setRoutes(json);
  }

  React.useEffect(() => {
    (async () => {
      await routesResponse();
    })();
  }, []);

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
              <Box sx={{marginLeft: '5%'}} key={route.id}>
                <Typography variant='h3'>{route.routeName}</Typography>
                <Typography>{route.type.name} Route</Typography>
                <div css={{displayInlineFlex}}>
                  <Button variant='contained' sx={{marginRight: '1%'}} size='large'>View</Button>
                  <Button variant='contained' size='large'>Edit</Button>
                </div>
              </Box>
            ))}
            </Grid>
            <Grid item md={7}>
                <Typography paragraph>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
                    tempor incididunt ut labore et dolore magna aliqua. Rhoncus dolor purus non
                    enim praesent elementum facilisis leo vel. Risus at ultrices mi tempus
                    imperdiet. Semper risus in hendrerit gravida rutrum quisque non tellus.
                    Convallis convallis tellus id interdum velit laoreet id donec ultrices.
                    Odio morbi quis commodo odio aenean sed adipiscing. Amet nisl suscipit
                    adipiscing bibendum est ultricies integer quis. Cursus euismod quis viverra
                    nibh cras. Metus vulputate eu scelerisque felis imperdiet proin fermentum
                    leo. Mauris commodo quis imperdiet massa tincidunt. Cras tincidunt lobortis
                    feugiat vivamus at augue. At augue eget arcu dictum varius duis at
                    consectetur lorem. Velit sed ullamcorper morbi tincidunt. Lorem donec massa
                    sapien faucibus et molestie ac.
                </Typography>
                <Typography paragraph>
                    Consequat mauris nunc congue nisi vitae suscipit. Fringilla est ullamcorper
                    eget nulla facilisi etiam dignissim diam. Pulvinar elementum integer enim
                    neque volutpat ac tincidunt. Ornare suspendisse sed nisi lacus sed viverra
                    tellus. Purus sit amet volutpat consequat mauris. Elementum eu facilisis
                    sed odio morbi. Euismod lacinia at quis risus sed vulputate odio. Morbi
                    tincidunt ornare massa eget egestas purus viverra accumsan in. In hendrerit
                    gravida rutrum quisque non tellus orci ac. Pellentesque nec nam aliquam sem
                    et tortor. Habitant morbi tristique senectus et. Adipiscing elit duis
                    tristique sollicitudin nibh sit. Ornare aenean euismod elementum nisi quis
                    eleifend. Commodo viverra maecenas accumsan lacus vel facilisis. Nulla
                    posuere sollicitudin aliquam ultrices sagittis orci a.
                </Typography>
            </Grid>
        </Grid>
      </Main>
    </Box>
  );
}