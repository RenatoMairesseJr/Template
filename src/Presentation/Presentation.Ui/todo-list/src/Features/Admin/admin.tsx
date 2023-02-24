import { Card, CardHeader, CardContent, Typography, Container } from "@mui/material"

const AdminPage = () =>{
    return(
        <>
            <Container fixed maxWidth="xl">
                <br />
                <Card sx={{ Width: '50%' }}>
                    <CardHeader title={"Admin Page"} />
                    <CardContent>
                        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                            This is just a placeholder to show how menu can adapt according with the user's access permission level.
                        </Typography>
                    </CardContent>
                </Card>
            </Container>
        </>)
}

export default AdminPage