import { Outlet } from "react-router-dom";
import { useState, useEffect } from "react";
import useAuth from "./useAuth";

const PersistLogin = () => {
    const [isLoading, setIsLoading] = useState(true);
    const { auth, refresh, persist } = useAuth();

    useEffect(() => {
        const verifyToken = async () => {
            try{
                !auth?.accesstoken ? await refresh() : setIsLoading(false);
            }
            catch(err: any){
                console.log(err);
            }
            finally{
                setIsLoading(false);
            }
        }

        verifyToken()
    }, [])

    return (
        <>
            {!persist 
                ? <Outlet />
                : isLoading 
                    ? <p>Loading...</p> 
                    : <Outlet />}
        </>
    )
}

export default PersistLogin;