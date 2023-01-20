import { Dayjs } from "dayjs";

export interface IUserCreateModel {
    firstName: string;
    lastName: string;
    email : string;
    password : string;
    dateOfBirth: Dayjs;
}