import fetchIntercept from "fetch-intercept";
import {AuthenticationService, StorageService} from "../services"
import LoginUtils from "./login";
import StorageTypes from "../constants/storage_types";
import toast from "react-hot-toast";
import { ITokenModel } from '../interfaces/ITokenModel';
import { AuthContext } from "../contexts";
import { GOOGLE_API_KEY } from "../constants/keys";

const baseUrl = process.env.REACT_APP_API_URL ?? "http://localhost:7241";
const configureUrl = (url: string) => `${baseUrl}/${url}`;
const refreshUrl = "/auth/refresh";
let isRefreshing = false;

fetchIntercept.register({
    request: function (url, config) {
        config = {
            ...config,
            headers: {
                'Content-Type': "application/json"
            },
        };

        if (config.inferHeaders) {
            delete config.headers;
        }

        const token = StorageService.getLocalStorage(StorageTypes.AUTH);
        if (token && !LoginUtils.isTokenExpired(token)) {
            const bearerToken = url === `${baseUrl}${refreshUrl}` ? token.refreshToken : token.token;
            config.headers = {
                ...config.headers,
                Authorization: `Bearer ${bearerToken}`,
            };
        }
        return [url, config];
    },
    response: function (response) {
        if (response.status === 401 && response.request.url !== `${baseUrl}${refreshUrl}`) {
            const refreshToken = StorageService.getLocalStorage(StorageTypes.AUTH).refreshToken;

            let tokenModel : ITokenModel = {
                token: StorageService.getLocalStorage(StorageTypes.AUTH).token,
                refreshToken: StorageService.getLocalStorage(StorageTypes.AUTH).refreshToken
            }

            if (refreshToken && !isRefreshing) {
                isRefreshing = true;
                const originalConfig = response.request;
                AuthenticationService.refresh(tokenModel)
                    .then((response: { json: () => Promise<any>; }) => {
                        response.json().then((content: any) => {
                            toast.success("session expired and was successfully refreshed");
                            StorageService.setLocalStorage(content, StorageTypes.AUTH);
                            setTimeout(() => {
                                window.location.reload();
                            }, 2000);
                        });
                    })
                    .catch(() => {
                        toast.error("something has gone wrong with authentication. logging out...");
                        StorageService.removeLocalStorage(StorageTypes.AUTH);
                        setTimeout(() => {
                            window.location.reload();
                        }, 2000);
                    })
                    .finally(() => {
                        isRefreshing = false;
                    });
            }
        }

        return response;
    },
});

const fetchInstance = async (url: any, ...params: any[]) => {
    return await fetch(configureUrl(url), ...params);
};

export default {fetchInstance};