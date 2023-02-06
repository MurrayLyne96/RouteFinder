import { ROUTE_TYPES } from "../constants/identifiers";

const generateRouteWithStartAndFinish = async(
origin: google.maps.LatLng, 
destination: google.maps.LatLng, 
DirectionsService: google.maps.DirectionsService,
DirectionsRenderer: google.maps.DirectionsRenderer,
routeMap: google.maps.Map,
type: ROUTE_TYPES) => {
    if (DirectionsService != undefined && DirectionsRenderer != undefined && routeMap != undefined) {
        let travelMode : google.maps.TravelMode;
        if (type == 1) {
            travelMode = google.maps.TravelMode.WALKING;
        } else if (type == 2) {
            travelMode = google.maps.TravelMode.BICYCLING;
        } else {
            travelMode = google.maps.TravelMode.WALKING;
        }

        var response = await DirectionsService.route({
            origin: origin,
            destination: destination,
            optimizeWaypoints: true,
            unitSystem: google.maps.UnitSystem.IMPERIAL,
            travelMode: travelMode,
        });

        return  response;
    }
}

export default {
    generateRouteWithStartAndFinish
}