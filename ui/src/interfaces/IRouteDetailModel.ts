import { IPlotPointModel } from './IPlotPointModel';
import { ITypeModel } from './ITypeModel';
export interface IRouteDetailModel {
    id : string;
    routeName : string;
    type: ITypeModel;
    typeId: number;
    userId: string;
    plotPoints: IPlotPointModel[];
}