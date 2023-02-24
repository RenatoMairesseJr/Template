import { Container, Card, CardHeader, CardContent, Typography, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

const Unauthorized = ()  => {
    const navigate = useNavigate();
    const goBack = () => navigate(-1);

    return  (            
        <Container fixed maxWidth="xl">
            <br />
            <Card sx={{ Width: '50%' }}>
                <CardHeader title={"Unauthorized"} />
                <CardContent>
                    <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                        You do not have access to the requested page.
                    </Typography>
                    <br />
                    <Button variant="outlined" onClick={goBack}>Go Bach</Button>
                </CardContent>
            </Card>
        </Container>
    )
}

export default Unauthorized;