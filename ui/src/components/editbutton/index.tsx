import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";
import route from "../../pages/route";
import { IEditbuttonHookProps } from '../../interfaces/IEditButtonHookProps';


function EditButton(props: IEditbuttonHookProps) {
    const navigate = useNavigate();

    const navigateToRouteEditPage = (routeId: string) => {
        navigate(`/routes/${routeId}/edit`);
    }

    return(<Button variant='contained' size='large' sx={{marginRight: '1%'}} onClick={() => navigateToRouteEditPage(props.route.id)}>Edit</Button>)
}

export default EditButton;