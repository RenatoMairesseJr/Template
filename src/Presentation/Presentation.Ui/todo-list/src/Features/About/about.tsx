import { CssBaseline, Container, Box, FormGroup, Checkbox, FormControlLabel, Typography, Card, CardContent, CardHeader, Grid} from "@mui/material"
import { makeStyles } from "tss-react/mui";

const useStyles = makeStyles()({
    box: {
        bgcolor: '#cfe8fc', 
        height: '100vh'
    }
});

const AboutPage = () =>{
    const { classes }= useStyles();

    return(
        <>
            <CssBaseline />
            <br />
            <Container fixed maxWidth="xl">
                <Card sx={{ Width: '50%' }}>
                    <CardHeader title={"About"} />
                    <CardContent>
                        <Grid container spacing={2}>
                            <Grid item xs={12} alignItems="center">
                                <Typography variant="body1" gutterBottom>
                                    ToDo List app is a stude case of implementation of an web application following CLEAN Architecture,
                                    SOLID principles and, when possible, KISS principle.. <br/>
                                    It was developed using the follow technologies: <br />
                                </Typography>
                            </Grid>
                            <Grid item xs={6} alignItems="center">
                                <Typography variant="subtitle1" gutterBottom>Back-End</Typography>
                                <FormGroup>
                                    <FormControlLabel control={<Checkbox checked={true} />} label=".NET 6" />
                                    <FormControlLabel control={<Checkbox checked={true} />} label="Entity Framework" />
                                    <FormControlLabel control={<Checkbox checked={true} />} label="Swagger: Swashbuckle" />
                                </FormGroup>
                            </Grid>
                            <Grid item xs={6} alignItems="center">
                                <Typography variant="subtitle1" gutterBottom>Front-End</Typography>
                                <FormGroup>
                                    <FormControlLabel control={<Checkbox checked={true} />} label="React Hooks" />
                                    <FormControlLabel control={<Checkbox checked={true} />} label="TypeScript" />
                                    <FormControlLabel control={<Checkbox checked={true} />} label="Materia-ui" />
                                </FormGroup>
                            </Grid>
                        </Grid>
                    </CardContent>
                </Card>
            </Container>
        </>)
}

export default AboutPage
