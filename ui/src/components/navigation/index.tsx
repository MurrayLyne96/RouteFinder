/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import AdbIcon from '@mui/icons-material/Adb';
import { FaRunning } from 'react-icons/fa';
import { marginLeft5, marginTop2dot5, marginRight1, marginLeft1 } from '../../css/styling';
import { useNavigate, Link } from 'react-router-dom';
import { LoginUtils } from '../../utils';
import { AuthContext } from '../../contexts';
import { StorageService } from '../../services';
import toast from 'react-hot-toast';
import Storage_types from '../../constants/storage_types';

const pages = ['dashboard', 'routes'];
const settings = ['profile', 'logout'];

function Navigation() {
    const {state, dispatch} = AuthContext.useLogin();
    const loggedIn = state.token && !LoginUtils.isTokenExpired(state);
    const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);
    const navigate = useNavigate();
    
    const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElNav(event.currentTarget);
    };

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = (page: string) => {
        if (pages.includes(page)) {
            navigate(`/${page}`);
        }
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = (page: string) => {
        if (page === 'logout') {
            logout();
        } else if (settings.includes(page)) {
            navigate(`/${page}`);
        }
        setAnchorElUser(null);
    };

    const logout = () => {
        StorageService.removeLocalStorage(Storage_types.AUTH);
        dispatch({type: "logout"});
        navigate('/login');
        toast.success("Logout Successful!");
    };
    
    return loggedIn ? (
            <AppBar>
                <div css={[marginLeft1, marginRight1]}>
                    <Toolbar disableGutters>
                    <Typography
                        variant="h6"
                        noWrap
                        component="a"
                        href="/"
                        sx={{
                        mr: 2,
                        display: { xs: 'none', md: 'flex' },
                        fontFamily: 'Balsamiq Sans',
                        color: 'inherit',
                        textDecoration: 'none',
                        }}
                    >
                        RouteFinder <div css={[marginTop2dot5, marginLeft5]}><FaRunning/></div>
                    </Typography>
                    <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
                        <IconButton
                        size="large"
                        aria-label="account of current user"
                        aria-controls="menu-appbar"
                        aria-haspopup="true"
                        onClick={handleOpenNavMenu}
                        color="inherit"
                        >
                        <MenuIcon />
                        </IconButton>
                        <Menu
                        id="menu-appbar"
                        anchorEl={anchorElNav}
                        anchorOrigin={{
                            vertical: 'bottom',
                            horizontal: 'left',
                        }}
                        keepMounted
                        transformOrigin={{
                            vertical: 'top',
                            horizontal: 'left',
                        }}
                        open={Boolean(anchorElNav)}
                        onClose={handleCloseNavMenu}
                        sx={{
                            display: { xs: 'block', md: 'none' },
                        }}
                        >
                        {pages.map((page) => (
                            <MenuItem key={page} onClick={() => handleCloseNavMenu(page)}>
                                <Typography textAlign="center">{page}</Typography>
                            </MenuItem>
                        ))}
                        </Menu>
                    </Box>
                    <Typography
                        variant="h5"
                        noWrap
                        component="a"
                        href=""
                        sx={{
                        mr: 2,
                        display: { xs: 'flex', md: 'none' },
                        flexGrow: 1,
                        fontWeight: 700,
                        color: 'inherit',
                        textDecoration: 'none',
                        }}
                    >
                        RouteFinder
                        <div css={[marginTop2dot5, marginLeft5]}><FaRunning scale={10}/></div>
                    </Typography>
                    <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                        {pages.map((page) => (
                        <Button
                            key={page}
                            onClick={() => handleCloseNavMenu(page)}
                            sx={{ my: 2, color: 'white', display: 'block' }}
                        >
                            {page}
                        </Button>
                        ))}
                    </Box>

                    <Box sx={{ flexGrow: 0 }}>
                        <Tooltip title="Open settings">
                        <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                            <Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg" />
                        </IconButton>
                        </Tooltip>
                        <Menu
                        sx={{ mt: '45px' }}
                        id="menu-appbar"
                        anchorEl={anchorElUser}
                        anchorOrigin={{
                            vertical: 'top',
                            horizontal: 'right',
                        }}
                        keepMounted
                        transformOrigin={{
                            vertical: 'top',
                            horizontal: 'right',
                        }}
                        open={Boolean(anchorElUser)}
                        onClose={handleCloseUserMenu}
                        >
                        {settings.map((setting) => (
                            <MenuItem key={setting} onClick={() => handleCloseUserMenu(setting)}>
                            <Button onClick={() => handleCloseNavMenu(setting)}>{setting}</Button>
                            </MenuItem>
                        ))}
                        </Menu>
                    </Box>
                    </Toolbar>
                </div>
            </AppBar>
        ) : <div/>;
}

export default Navigation;