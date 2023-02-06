import {FetchUtils} from "../utils/";

const authenticate = async (email: string, password: string) => {
    return await FetchUtils.fetchInstance("auth", {
        method: "POST",
        body: JSON.stringify({email, password}),
    });
};

const refresh = async () => {
    return await FetchUtils.fetchInstance("auth/refresh", {
        method: "POST",
    });
};

export default {
    authenticate: authenticate,
    refresh: refresh,
};