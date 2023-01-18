import { IAuthModel } from '../interfaces/IAuthModel';

export class AuthModel implements IAuthModel {
    email: string;
    password: string;
    
    constructor() {
        this.email = '';
        this.password = '';
    }
}