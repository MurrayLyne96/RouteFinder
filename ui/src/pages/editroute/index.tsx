/** @jsxImportSource @emotion/react */
import { jsx, css, Global, ClassNames } from '@emotion/react';
import * as React from 'react'
import { Link, useParams } from 'react-router-dom'
import { RoutesService } from '../../services';
import { IRouteDetailModel } from '../../interfaces/IRouteDetailModel';
import { Button, Grid, Typography, TextField, MenuItem, Select } from '@mui/material';
import { FaBackspace } from 'react-icons/fa';
import { margin2 } from '../../css/styling';
import { ITypeModel } from '../../interfaces/ITypeModel';
function EditRoute() {
    const {routeId} = useParams();
    const [route, setRoute] = React.useState<IRouteDetailModel>({id: '', routeName: '', typeId: 0, type: {id: 0, name: ''}, userId: '', plotPoints: []});
    const handleChange = (event: any) => {
        setRoute(existingValues => (
            {
                ...existingValues,
                typeId: event.target.value
            }
        ));
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
        
    }, [routeId]);
    return (
        <>
            <Grid container spacing={2}>
                <Grid item md={4}>
                    <Typography variant='h4'><Link to={"/routes"}><FaBackspace/></Link></Typography>
                    <Typography variant='h3'>Edit Route</Typography>
                    <TextField value={route?.routeName}></TextField>
                    <div css={margin2}>
                        <Select value={route?.typeId} id='type-select' fullWidth onChange={handleChange}>
                            <MenuItem value={1}>Cycling</MenuItem>
                            <MenuItem value={2}>Running</MenuItem>
                        </Select>
                    </div>
                    <Button variant='contained' size='large'>Save</Button>
                </Grid>
                <Grid item md={8}>
                    {/* map goes here */}
                </Grid>
            </Grid>
        </>
    );
}

export default EditRoute;