import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import PlaylistAddCheckIcon from '@mui/icons-material/PlaylistAddCheck';
import HomeIcon from '@mui/icons-material/Home';
import InfoIcon from '@mui/icons-material/Info';
import AdminPanelSettingsIcon from '@mui/icons-material/AdminPanelSettings';
import { makeStyles } from "tss-react/mui";
import { Link } from 'react-router-dom';
import { MenuOptions } from '../../Clients/Authentication/authenticationClient';

interface IDrawerProps {
    isDrawerOpen: boolean,
    closeDrawer: () => any
    menuOptions: MenuOptions[]
}

const useStyles = makeStyles()(() => ({
    list: {
        width: 250,
    },
    listItem: {
        color: 'black',
    }
}))

const getIconComponent = (iconName: string) =>{
    switch (iconName){
        case "Home":
            return <HomeIcon />
        case "About":
            return <InfoIcon />
        case "Admin":
            return <AdminPanelSettingsIcon />
        case "ToDoList" :
            return <PlaylistAddCheckIcon />
    }
}

const NavigationMenu = (props: IDrawerProps) => {
    const {classes} = useStyles();

    function activeRoute(routeName: string) {
        return window.location.pathname.indexOf(routeName) > -1 ? true : false
    }

    return (
        <Drawer open={props.isDrawerOpen} onClose={props.closeDrawer} >
            <Box
                role="presentation"
                onClick={props.closeDrawer}
                onKeyDown={props.closeDrawer}
                className={classes.list}
            >
                <List>
                    {props.menuOptions.map((menuItem: MenuOptions, key: any)  => 
                        {
                            if(menuItem.showInMenu){
                                return  <Link to={menuItem.path ?? ""} key={menuItem.name} >
                                            <ListItemButton selected={activeRoute(menuItem.path ?? "")} key={key}>
                                                <ListItemIcon>
                                                    {getIconComponent(menuItem.icon)}
                                                </ListItemIcon>

                                                <ListItemText className={classes.listItem} primary={menuItem.name} />
                                            </ListItemButton>
                                        </Link>
                            }
                        }
                    )}
                </List>
            </Box>
        </Drawer>
    )
}

export default (NavigationMenu)
