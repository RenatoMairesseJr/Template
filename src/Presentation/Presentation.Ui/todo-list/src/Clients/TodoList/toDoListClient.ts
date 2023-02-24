import {FetchApi} from "../../Shared/BaseApi/fetchApi"

export interface IToDoListDto {
    id: number,
    userId: number
    description: string
    dateCreated: Date
    dateCompleted?: Date | null
}

export interface IToDoListClient {
    getAll(): Promise<IToDoListDto[]>
    getByUserId(userId : string): Promise<IToDoListDto[]>
    addItemToList(newTask: IToDoListDto): Promise<IToDoListDto>
    completeTask(id: number): Promise<IToDoListDto>
    resetTask(id: number): Promise<IToDoListDto>
    updateDescription(id: number, description: string): Promise<IToDoListDto>
    deleteTask(id: number): Promise<boolean>;
}

export class ToDoListClient implements IToDoListClient {
    getAll(): Promise<IToDoListDto[]> {
        let endPoint = "/api/ToDoList/GetAll";
        let method = "GET"
        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers)
    };
    
    getByUserId(userId: string): Promise<IToDoListDto[]> {
        throw new Error("Method not implemented.")
    }
    addItemToList(newTask: IToDoListDto): Promise<IToDoListDto> {
        throw new Error("Method not implemented.")
    }
    completeTask(id: number): Promise<IToDoListDto> {
        let endPoint = `/api/ToDoList/CompleteTask/${id}`;
        let method = "PUT"
        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers)
    }
    resetTask(id: number): Promise<IToDoListDto> {
        let endPoint = `/api/ToDoList/ResetCompleteTask/${id}`;
        let method = "PUT"
        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers)
    }
    updateDescription(id: number, description: string): Promise<IToDoListDto> {
        let endPoint = `/api/ToDoList/ResetCompleteTask/${id}`;
        let method = "POST"
        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers)
    }
    deleteTask(id: number): Promise<boolean> {
        let endPoint = `/api/ToDoList/ResetCompleteTask/${id}`;
        let method = "DELETE"
        var headers = new Headers()
        headers.append("Content-Type", "application/json");
        
        var fetchData = new FetchApi();

        return fetchData.apiCall(endPoint, method, headers)
    }
}

