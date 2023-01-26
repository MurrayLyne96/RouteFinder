import {FetchUtils} from "../utils/";
import { IUserCreateModel } from '../interfaces/IUserCreateModel';
import dayjs from "dayjs";
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

export default {
    RegisterNewUser: RegisterNewUser,
    GetAllRoutesByUserId: GetAllRoutesByUserId
}