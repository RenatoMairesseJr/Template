import { FetchApi } from "../../Shared/BaseApi/fetchApi"

export interface IAuthRequest {
    email: string,
    password: string
}

export interface IAuthResponse {
    email : string
    token : string
    menus  : MenuOptions[]
}

export interface MenuOptions{
    name : string
    path : string
    icon : string
    component : string
    showInMenu : boolean
    permissions: string[]
}

export interface IRegistrationRequest {
    fullName: string,
    username: string,
    password: string
}

export interface IRegistrationResponse {
    email: string;
}

export interface IAuthClient {
    login(credentials: string): Promise<IAuthResponse>
    register(credentials: string) : Promise<IRegistrationResponse>
}

export class AuthClient implements IAuthClient {
    async login(credentials: string): Promise<IAuthResponse> {
        let endPoint = "/api/Auth/Login";
        let method = "POST"

        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        headers.set("withCredentials", "true")

        let body = JSON.stringify(credentials);
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers, body)
    }

    register(credentials: string): Promise<IRegistrationResponse> {
        let endPoint = `/api/Auth/register`;
        let method = "POST"
        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        let body = JSON.stringify(credentials)
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers, body)
    }  
    
    refreshToke(): Promise<string> {
        let endPoint = `/api/Auth/refreshToken`;
        let method = "GET"
        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        headers.set("withCredentials", "true")
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers, undefined)
    }

    logOut(): Promise<boolean> {
        let endPoint = `/api/Auth/logOut`;
        let method = "GET"
        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        headers.set("withCredentials", "true")
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers, undefined)
    }
}
