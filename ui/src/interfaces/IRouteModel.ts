import { ITypeModel } from './ITypeModel';
export interface IRouteModel {
    id : string;
    routeName : string;
    type: ITypeModel;
    typeId: number;
    userId: string;
}