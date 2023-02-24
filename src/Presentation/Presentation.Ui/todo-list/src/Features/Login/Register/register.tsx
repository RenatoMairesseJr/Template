import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { useState } from 'react';

interface Iprops{
    isOpen: boolean,
    close: () => any
}

const RegisterDialog = (props: Iprops) => {
   const [name, setName] = useState("")
   const [username, setUsername] = useState("")
   const [pwd, setPwd] = useState("")
   const [confirmPwd, setConfirmPwd] = useState("")
   const [hasError, sethasError] = useState(true)

   const validate = () => {
      if(!pwd || !confirmPwd || !username){
         alert("Please fill all required fields")
         sethasError(true)
      }

      if(pwd !== confirmPwd){
         alert("Password doen't match")
         sethasError(true)
      }
   }
   const register = async () => {
      validate()

      if(!hasError)
         alert("Register confirmed")

      
      props.close()
   }

   return (
      <Dialog open={props.isOpen} onClose={props.close}>
         <DialogTitle>Create an Account</DialogTitle>
         <DialogContent>
            <TextField margin="dense" id="name" label="Name" type="text" fullWidth variant="standard" onChange={(e) => setName(e.target.value)}/>
            <TextField  autoFocus 
                        margin="dense" 
                        id="userName" 
                        type="email" 
                        fullWidth 
                        label="Username"
                        variant="standard" 
                        onChange={(e) => setUsername(e.target.value)}/>
            <TextField margin="dense" id="password" label="Password" type="password" fullWidth variant="standard" onChange={(e) => setPwd(e.target.value)}/>
            <TextField margin="dense" id="confirmPassword" label="Confirm Password" type="password" fullWidth variant="standard" onChange={(e) => setConfirmPwd(e.target.value)}/>
         </DialogContent>
         <DialogActions>
            <Button onClick={props.close}>Cancel</Button>
            <Button onClick={register} >Register</Button>
         </DialogActions>
      </Dialog>
  );
}

export default(RegisterDialog);