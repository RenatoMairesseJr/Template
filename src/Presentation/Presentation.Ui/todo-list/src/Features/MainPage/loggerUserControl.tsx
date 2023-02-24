import { AccountCircle } from "@mui/icons-material";
import { ClickAwayListener, MenuItem, MenuList, Paper, Popper } from "@mui/material";
import IconButton from "@mui/material/IconButton";
import React, { useState } from "react";

export interface ILoggedUser {
    logOut: () => any
    userName: string
}

const LoggedUserControl = (props: ILoggedUser) => {

    const anchorRef = React.useRef<HTMLButtonElement>(null)
    const [open, setOpen] = useState(false)

    return (
        <React.Fragment>
            <IconButton
                ref={anchorRef}
                color="inherit"
                aria-label="Login"
                onClick={() => setOpen(true)} >
                <AccountCircle />
            </IconButton>

            <Popper open={open} anchorEl={anchorRef.current} >
                <Paper>
                    <ClickAwayListener onClickAway={() => setOpen(false)}>
                        <MenuList>
                            <MenuItem key={0} onClick={() => setOpen(false)}>{props.userName}</MenuItem>
                            <MenuItem key={1} onClick={props.logOut}>Logout</MenuItem>                            
                        </MenuList>
                    </ClickAwayListener>
                </Paper>

            </Popper>
        </React.Fragment>
    )
}
export default (LoggedUserControl)