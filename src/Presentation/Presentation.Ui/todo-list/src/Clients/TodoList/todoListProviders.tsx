import { createContext, useState} from "react";
import { IToDoListDto, ToDoListClient } from "./toDoListClient"
import useAuth from "../Authentication/useAuth";

const ToDoListContext = createContext<any>({})

export const ToDoListProvider  = (props : any) => {
    const toDoListClient = new ToDoListClient()
    const { email } = useAuth()
    const [todoList, setTTodoList] = useState<IToDoListDto[]>([])

    const getById = async () => {
        const response = await toDoListClient.getByUserId(email)
        setTTodoList(response)
    }

    const completeTask = async (id: number) => {
        let task = todoList.find(a => a.id == id);
        let response : IToDoListDto

        if(!task?.dateCompleted)
            response = await toDoListClient.completeTask(id);
        else
            response = await toDoListClient.resetTask(id);

        let updatedList = todoList.map(item => 
        {
            if (item.id == id){
                return {...item, response}; 
            }
            return item; 
        });

        setTTodoList(updatedList)
    }

    const addTask = async (newTask: IToDoListDto) => {
        let response = await toDoListClient.addItemToList(newTask);
        setTTodoList(prev => [...prev, response])
    }

    const updateDescription = async (id: number, description: string) => {
        let response = await toDoListClient.updateDescription(id, description);
        setTTodoList(prev => [...prev, response])
    }

    const removeTask = async (id: number) => {
        let response = await toDoListClient.deleteTask(id);

        // let updatedList : IToDoListDto[] =  []
        
        // updatedList = todoList.map(item => 
        // {
        //     if (item.id != id)
        //         return item; 
        // });
    
        // setTTodoList(updatedList) ;            
    }

    return( <ToDoListContext.Provider value={{todoList, getById, addTask, completeTask, updateDescription, removeTask}}> 
            {props.children}    
        </ToDoListContext.Provider>
    );
};

export default (ToDoListContext);