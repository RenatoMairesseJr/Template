import { CssBaseline, Container, Card, CardContent, CardHeader, Grid, TextField, Button, Divider, FormControlLabel, FormGroup, Snackbar} from "@mui/material"
import { useContext, useState } from "react";
import RegisterDialog from "./Register/register";
import AuthContext from '../../Clients/Authentication/authenticationProvider'
import { useLocation, useNavigate } from "react-router-dom";
import { Checkbox } from "@mui/material";

const LoginPage = () =>{ 
   const { login}  = useContext(AuthContext);

   const navigate = useNavigate();
   const location = useLocation();
   const from  = location.state?.from?.pathname || "/";

   const [username, setUser] = useState('');
   const [pwd, setPassword] = useState('');
   const [isRegisterDialogOpen, setIsRegisterDialogOpen] = useState(false)
   const [persist, setPersist] = useState(false)

   const openRegisterDialog = () => {
       setIsRegisterDialogOpen(true)
   }

   const handleLogin = async () => {
         await login(username, pwd);
         navigate(from , {replace: true});
   }

   const psersitToggle = () => {
      setPersist((prev:boolean) => !prev)
   }

   return(
      <>
         <CssBaseline />
         <br />
         <Container fixed maxWidth="xl">
               <Card sx={{ Width: '50%' }}>
                  <CardHeader title={"Login"} />
                  <CardContent>
                     <Grid container spacing={2}>
                           <Grid item xs={3} alignItems="center">
                              <TextField id="username" 
                                         label="username" 
                                         variant="standard" 
                                         value={username}
                                         onChange={(e) => setUser(e.target.value)}/>
                           </Grid>
                           <Grid item xs={3} alignItems="center">
                              <TextField id="password" 
                                         label="password" 
                                         variant="standard" 
                                         type={"password"} 
                                         value={pwd}
                                         onChange={(e) => setPassword(e.target.value)}/>                            
                           </Grid>
                           <Grid item xs={6} alignItems="center">
                              <Button variant="outlined" onClick={handleLogin} disabled={!username || !pwd}>Login</Button>
                              <FormGroup>
                                 <FormControlLabel 
                                    control={<Checkbox size="small" checked={persist} onChange={psersitToggle}/>} label="Trust this device" />
                              </FormGroup>
                           </Grid>
                     </Grid>
                     <br />
                     <Divider />
                     <Grid container spacing={2}>
                        <Grid item xs={12} alignItems="center">&nbsp;</Grid>
                        <Grid item xs={12} alignItems="center">
                           <Button variant="outlined" onClick={openRegisterDialog}>Create an Account</Button>
                        </Grid>
                     </Grid>
                  </CardContent>
               </Card>
         </Container>
         {isRegisterDialogOpen && <RegisterDialog isOpen={isRegisterDialogOpen} close={() => setIsRegisterDialogOpen(false)}/>}
      </>)
}

export default LoginPage
