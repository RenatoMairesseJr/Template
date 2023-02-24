import { createContext, useState} from "react";
import { AuthClient } from "./authenticationClient";
import { protect} from "../../Shared/BaseApi/protectData";

const AuthContext = createContext<any>({})

export const AuthProvider  = (props : any) => {
    const authClient = new AuthClient()
    const [auth, setAuth] = useState({})    

    const refreshToken = () => { 
        return "";
    }

    const login = async (email: string, password: string) =>  {
        var credentials = protect(JSON.stringify({"Email":email, "Password": password}))

        var response = await authClient.login(credentials)
        setAuth({email: response.email, token : response.token, menus: response.menus})    
    }

    const logOut = () => {
        setAuth({});   
    }

    return( <AuthContext.Provider value={{auth, setAuth, login, refreshToken, logOut}}> 
            {props.children}    
        </AuthContext.Provider>
    );
};

export default (AuthContext);