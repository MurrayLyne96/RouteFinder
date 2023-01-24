/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react'
import {AuthenticationService, StorageService} from "../../services";
import React from "react";
import {RingLoader} from "react-spinners";
import Storage_types from '../../constants/storage_types';
import toast from 'react-hot-toast';
import { ReactDOM } from "react";
import AuthContext from "../../contexts/authContext"
import { TextField, Link, Button, Grid, Typography, Toolbar, Box, IconButton, Drawer, List, Divider, ListItem, ListItemButton, ListItemIcon, ListItemText, useTheme, styled } from '@mui/material';
import { IAuthModel } from '../../interfaces/IAuthModel';
import { LoginUtils } from '../../utils';
import { FaAlignJustify, FaChevronLeft, FaChevronRight, FaHamburger, FaInbox, FaMailBulk, FaRunning } from 'react-icons/fa';
import { headerCss, font52, formGroup, loginButtonsBottom, flexBasis50, alignItemsLeft, justifyItemsRight, centeredItem, thirdWidth } from '../../css/styling';
import { useNavigate } from 'react-router-dom';

const drawerWidth = 240;

function Dashboard() : JSX.Element {

    const handleDrawerOpen = () => {
        setOpen(true);
    }

    const handleDrawerClose = () => {
        setOpen(false);
    };

    const theme = useTheme();

    const DrawerHeader = styled('div')(({ theme }) => ({
        display: 'flex',
        alignItems: 'center',
        padding: theme.spacing(0, 1),
        // necessary for content to be below app bar
        ...theme.mixins.toolbar,
        justifyContent: 'flex-end',
    }));

    const [open, setOpen] = React.useState(false);
    return (
        <Box sx={{display: "flex"}}>
            <Drawer
                sx={{
                width: drawerWidth,
                flexShrink: 0,
                '& .MuiDrawer-paper': {
                    width: drawerWidth,
                    boxSizing: 'border-box',
                },
                }}
                variant="persistent"
                anchor="left"
                open={open}
            >
                <DrawerHeader>
                <IconButton onClick={handleDrawerClose}>
                    {theme.direction === 'ltr' ? <FaChevronLeft /> : <FaChevronRight />}
                </IconButton>
                </DrawerHeader>
                <Divider />
                <List>
                {['Inbox', 'Starred', 'Send email', 'Drafts'].map((text, index) => (
                    <ListItem key={text} disablePadding>
                    <ListItemButton>
                        <ListItemIcon>
                        {index % 2 === 0 ? <FaInbox /> : <FaMailBulk />}
                        </ListItemIcon>
                        <ListItemText primary={text} />
                    </ListItemButton>
                    </ListItem>
                ))}
                </List>
                <Divider />
                <List>
                {['All mail', 'Trash', 'Spam'].map((text, index) => (
                    <ListItem key={text} disablePadding>
                    <ListItemButton>
                        <ListItemIcon>
                        {index % 2 === 0 ? <FaInbox /> : <FaMailBulk />}
                        </ListItemIcon>
                        <ListItemText primary={text} />
                    </ListItemButton>
                    </ListItem>
                ))}
                </List>
            </Drawer>
            <Grid
                container
                spacing={2}
            >
                <Grid item md={4}>
                <IconButton
                    color="inherit"
                    aria-label="open drawer"
                    onClick={handleDrawerOpen}
                    edge="start"
                    sx={{ mr: 2, ...(open && { display: 'none' }) }}
                >
                    <FaAlignJustify />
                </IconButton>
                </Grid>
                <Grid item md={8}>
                    <p>Test</p>
                </Grid>
            </Grid>
        </Box>
    )
}

export default Dashboard;