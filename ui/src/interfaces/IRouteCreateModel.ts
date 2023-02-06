import { IPlotPointCreateModel } from './IPlotPointCreateModel';
import { ITypeModel } from './ITypeModel';
export interface IRouteCreateModel {
    name : string;
    typeId: number;
    userId: string;
    plotPoints: IPlotPointCreateModel[];
}