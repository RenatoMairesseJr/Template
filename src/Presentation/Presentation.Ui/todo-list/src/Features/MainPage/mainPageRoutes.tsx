import React from "react";
import { Routes, Route } from "react-router";
import AdminPage from '../../Features/Admin/admin';
import AboutPage from '../../Features/About/about';
import RequireAuth from "../../Shared/Components/requireAuth";
import LoginPage from "../Login/login";
import { MenuOptions } from "../../Clients/Authentication/authenticationClient";
import WorkInProgress from "../Common/workInprogress";
import PersistLogin from "../../Clients/Authentication/persistLogin";
import Unauthorized from "../Common/unauthorized";
import MainTable from "../ToDoList/mainTable";

const getMenuComponent = (menuName: string) =>{
    switch (menuName){
        case "Admin": 
            return components['AdminPage']
        case "About":
            return components['AboutPage']
        case "Login":
            return components['LoginPage']
        case "ToDo List":
                return components['MainTable']            
        default:
            return components['PlaceHolder']
    }
}

const PlaceHolder = () => {
    return <></>
}


const components = {
    WorkInProgress,
    AdminPage,
    AboutPage,
    LoginPage,
    PlaceHolder,
    MainTable
};


interface IMainPageRoutes{
    menuOptions : MenuOptions[]
}

const MainPageRoutes = (props : IMainPageRoutes) => {
    return (
        <Routes>
            <Route key="login" path="/login" element={<LoginPage />}></Route>
            <Route key="unautorized" path="/unauthorized" element={<Unauthorized />}></Route>
            
            {
                props.menuOptions.map((menuItem : MenuOptions) => {
                    const RouteComponent = getMenuComponent(menuItem.name ?? "")
                        return (
                            //<Route element={<PersistLogin />} >
                                <Route key={menuItem.path} element={<RequireAuth allowRoles={menuItem?.permissions}/>} >
                                    <Route  path={menuItem.path} element={<RouteComponent />} />
                                </Route>
                            //</Route>
                        )
                })
                
            }
        </Routes>
    )
}

export default (MainPageRoutes)

