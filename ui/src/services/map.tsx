
const generateRouteWithStartAndFinish = async(
origin: google.maps.LatLng, 
destination: google.maps.LatLng, 
DirectionsService: google.maps.DirectionsService,
DirectionsRenderer: google.maps.DirectionsRenderer,
routeMap: google.maps.Map) => {
    if (DirectionsService != undefined && DirectionsRenderer != undefined && routeMap != undefined) {
      var response = await DirectionsService.route({
          origin: origin,
          destination: destination,
          optimizeWaypoints: true,
          unitSystem: google.maps.UnitSystem.IMPERIAL,
          travelMode: google.maps.TravelMode.BICYCLING,
      });

      return  response;
    }
}

export default {
    generateRouteWithStartAndFinish
}