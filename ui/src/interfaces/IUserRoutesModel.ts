import { IRouteModel } from './IRouteModel';
import { IRoleModel } from './IRoleModel';
export interface IUserRoutesModel {
    id: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    email: string;
    role: IRoleModel;
    roleId: string;
    lastmodified: string;
    created : string;
    routes: IRouteModel[]
}