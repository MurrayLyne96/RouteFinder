import {FetchUtils} from "../utils/";

const getAllRoutes = async () => {
    return await FetchUtils.fetchInstance("routes", {
        method: "GET",
    });
}

const getRouteById = async (routeId: string) => {
    return await FetchUtils.fetchInstance(`routes/${routeId}`, {
        method: "GET",
    });
}

export default {
    getAllRoutes,
    getRouteById
}