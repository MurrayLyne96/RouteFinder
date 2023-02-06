import {FetchUtils} from "../utils/";
import { IUserCreateModel } from '../interfaces/IUserCreateModel';
import dayjs from "dayjs";
import { IUserUpdateModel } from '../interfaces/IUserUpdateModel';
const RegisterNewUser = async(firstName: string, lastName : string, email : string, dateOfBirth: string, password: string ) => {
    return await FetchUtils.fetchInstance("users", {
        method: "POST",
        body: JSON.stringify({
            firstName,
            lastName,
            email,
            dateOfBirth,
            password
        })
    });
}

const GetAllRoutesByUserId = async (userId: string) => {
    return await FetchUtils.fetchInstance(`users/${userId}/routes`, {
        method: "GET",
    });
}

const GetUserById = async (userId: string) => {
    return await FetchUtils.fetchInstance(`users/${userId}`, {
        method: "GET",
    });
}

const SaveUser = async (userId: string, model : IUserUpdateModel) => {
    return await FetchUtils.fetchInstance(`users/${userId}`, {
        method: "PUT",
        body : JSON.stringify(model)
    });
}

export default {
    RegisterNewUser: RegisterNewUser,
    GetAllRoutesByUserId: GetAllRoutesByUserId,
    GetUserById,
    SaveUser
}