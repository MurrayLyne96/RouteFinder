import { IRouteModel } from './IRouteModel';
import { IRoleModel } from './IRoleModel';
export interface IUserUpdateModel {
    firstName: string;
    lastName: string;
    password: string;
    email: string;
    roleId: string;
    dateOfBirth: string;
}