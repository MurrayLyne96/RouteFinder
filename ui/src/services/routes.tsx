import {FetchUtils} from "../utils/";
import { IRouteUpdateModel } from '../interfaces/IRouteUpdateModel';
import { json } from "stream/consumers";

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

const updateRoute = async (routeModel: IRouteUpdateModel, routeId: string) => {
    return await FetchUtils.fetchInstance(`routes/${routeId}`, {
        method: 'PUT',
        body: JSON.stringify(routeModel)
    });
}

export default {
    getAllRoutes,
    getRouteById,
    updateRoute
}