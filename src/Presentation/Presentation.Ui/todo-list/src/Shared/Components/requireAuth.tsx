import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../../Clients/Authentication/useAuth";
import jwt_decode from "jwt-decode"

interface IRequiredAuth{
    allowRoles: string[]
}

const RequireAuth = (props: IRequiredAuth) => {
    const { auth } = useAuth();
    const location = useLocation()
    const decode : any = jwt_decode(auth?.token)
    
    var roles: string[] = Array.isArray(decode?.roles) ? decode?.roles : [decode?.roles.toString()]

    return roles.some((role: string) => props.allowRoles.includes(role)) 
        ? <Outlet />
        : auth?.email
            ? <Navigate to="/unauthorized" state={{from: location}} replace />
            : <Navigate to="/login" state={{from: location}} replace />
}

export default RequireAuth;