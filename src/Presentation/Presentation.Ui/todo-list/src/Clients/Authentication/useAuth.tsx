import { useContext } from "react";
import  AuthContext  from "./authenticationProvider";

const useAuth = () =>{
    return useContext(AuthContext)
}

export default useAuth