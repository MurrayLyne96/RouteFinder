/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react';
import * as React from 'react'
import { Link, useParams } from 'react-router-dom'
import { RoutesService } from '../../services';
import { IRouteDetailModel } from '../../interfaces/IRouteDetailModel';
import { Button, Grid, Typography, TextField, MenuItem, Select, InputLabel } from '@mui/material';
import { FaBackspace } from 'react-icons/fa';
import { margin2, marginBottom2 } from '../../css/styling';
import { ITypeModel } from '../../interfaces/ITypeModel';
import { IRouteUpdateModel } from '../../interfaces/IRouteUpdateModel';
import toast from 'react-hot-toast';

function EditRoute() {
    const {routeId} = useParams();
    const [route, setRoute] = React.useState<IRouteDetailModel>({id: '', routeName: '', typeId: 0, type: {id: 0, name: ''}, userId: '', plotPoints: []});


    const handleTypeChange = (event: any) => {
        setRoute(existingValues => (
            {
                ...existingValues,
                typeId: event.target.value
            }
        ));
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
                }
            }
        })()
        
    }, []);

    return (
        <>
            <Grid container spacing={2}>
                <Grid item md={4}>
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
                </Grid>
                <Grid item md={8}>
                    {/* map goes here */}
                </Grid>
            </Grid>
        </>
    );
}

export default EditRoute;