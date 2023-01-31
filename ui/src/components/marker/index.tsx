/** @jsxImportSource @emotion/react */
import { Stack } from '@mui/material';
import { LocationOn } from '@mui/icons-material'
import { red } from '@mui/material/colors'
import { IMarkerHookProps } from '../../interfaces/IMarkerHookProps';


function Navigation(props: IMarkerHookProps) {
    
    return (
    <div>
        <Stack direction="row">
            <LocationOn sx={{color: red[900]}} />
            <b>{props.text}</b>
        </Stack>
    </div>)
}

export default Navigation;