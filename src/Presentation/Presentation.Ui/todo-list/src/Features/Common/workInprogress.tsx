import { Card, CardHeader, CardContent, Typography, Container } from "@mui/material"

const WorkInProgress = () =>{
    return  (            
        <Container fixed maxWidth="xl">
            <br />
            <Card sx={{ Width: '50%' }}>
                <CardHeader title={"Work in Progress"} />
                <CardContent>
                    <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                        Still building....
                    </Typography>
                </CardContent>
            </Card>
        </Container>)
}

export default WorkInProgress