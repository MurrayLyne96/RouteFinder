import { ITokenModel } from "../interfaces/ITokenModel";
import {FetchUtils} from "../utils/";

const authenticate = async (email: string, password: string) => {
    return await FetchUtils.fetchInstance("auth", {
        method: "POST",
        body: JSON.stringify({email, password}),
    });
};

const refresh = async (model: ITokenModel) => {
    return await FetchUtils.fetchInstance("auth/refresh", {
        method: "POST",
        body: JSON.stringify(model),
    });
};

export default {
    authenticate: authenticate,
    refresh: refresh,
};