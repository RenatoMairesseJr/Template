import { Box, AppBar, Toolbar, IconButton, Typography, Button } from '@mui/material';
import React, {  useEffect, useState } from 'react';
import LoggedUserControl from './loggerUserControl';
import MainPageRoutes from './mainPageRoutes';
import NavigationMenu from './navigationMenu';
import MenuIcon from '@mui/icons-material/Menu';
import { useNavigate } from 'react-router-dom';
import useAuth from '../../Clients/Authentication/useAuth';

const MainPage = () => {
    const {auth, logOut} = useAuth();
    const navigate = useNavigate()

    const [isDrawerOpen, setIsDrawerOpen] = React.useState(false);
    const [navItems, setNavItems] = useState([])
    const [isLogged, setIsLogged] = useState(false)

    useEffect( () => { 
        if(auth?.email){
            setIsLogged(true)
            setNavItems(auth?.menus)
        }            
        else{
            setIsLogged(false)
            setNavItems([])
        }   
    }, [auth]);

    const openDrawer = () => setIsDrawerOpen(true);
    const closeDrawer = () => setIsDrawerOpen(false);

    const toggleDrawer = () =>{
        if(isDrawerOpen)
            closeDrawer()
        else
            openDrawer()
    }

    const openLogin = () =>{
        navigate('/login')    
    }

    const logout = () => {
        logOut();
        navigate('/login')  
    }

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Toolbar>
                <IconButton size="large" edge="start" color="inherit" aria-label="menu"sx={{ mr: 2 }} onClick={toggleDrawer}>
                    <MenuIcon />
                </IconButton>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    ToDo List
                </Typography>
                {isLogged ? <LoggedUserControl logOut={logout} userName={auth?.email}/> : <Button color="inherit" onClick={openLogin}>Login</Button>}
                </Toolbar>
            </AppBar>
            <NavigationMenu isDrawerOpen={isDrawerOpen} closeDrawer={closeDrawer} menuOptions={navItems}/>
            <MainPageRoutes menuOptions={navItems}/>
        </Box>
    );
}

export default (MainPage)