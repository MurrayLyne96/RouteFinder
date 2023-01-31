import * as React from 'react'
import { Link, useParams } from 'react-router-dom'
import { RoutesService } from '../../services';
import { IRouteDetailModel } from '../../interfaces/IRouteDetailModel';
import { Button, Grid, Typography } from '@mui/material';
import { FaBackspace } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';

function Route() {
    const navigate = useNavigate();
    const {routeId} = useParams();
    const [route, setRoute] = React.useState<IRouteDetailModel>();

    const navigateToRouteEditPage = (routeId: string | undefined) => {
        if (routeId != undefined) {
            navigate(`/routes/${routeId}/edit`);
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
        
    }, [routeId]);

    return (
        <>
            <Grid container spacing={2}>
                <Grid item md={4}>
                    <Typography variant='h4'><Link to={"/routes"}><FaBackspace/></Link></Typography>
                    <Typography variant='h3'>{route?.routeName}</Typography>
                    <Typography>{route?.type.name} Route</Typography>
                    <Button variant='contained' size='large' onClick={() => navigateToRouteEditPage(route?.id)}>Edit</Button>
                </Grid>
                <Grid item md={8}>
                    {/* map goes here */}
                </Grid>
            </Grid>
        </>
    );
}

export default Route;