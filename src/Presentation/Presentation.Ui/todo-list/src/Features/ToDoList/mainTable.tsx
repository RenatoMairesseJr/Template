import { useContext, useEffect, useState } from 'react';
import { IToDoListDto, ToDoListClient } from '../../Clients/TodoList/toDoListClient';
import { Box, Card, CardContent, CardHeader, Checkbox, Container, Grid, SpeedDial, SpeedDialAction, SpeedDialIcon, Typography } from '@mui/material';
import { Create } from '@mui/icons-material';
import ToDoListContext, { ToDoListProvider } from '../../Clients/TodoList/todoListProviders';

const actions = [
  { icon: <Create />, name: 'Add New Task' },
];

const MainTable = () => {

  const [isLoading, setIsLoading] = useState(false)
  const [data, setData] = useState<IToDoListDto[]>([])
  const client = new ToDoListClient()

  const context = useContext(ToDoListContext)

  const handleDateCompletes = async(id: number) => {
      setIsLoading(true);

      var task = data.find(a => a.id === id);
      task = await context.completeTask(id);

      var copyData = data.map(i => {
         if(i.id === id){
            i.dateCompleted =task?.dateCompleted;
            return i;
         }
         return i;
      })

      setData([...copyData])
      setIsLoading(false);
  }

  
  useEffect(() => {
    async function getAll(){
      setIsLoading(true);
      const response = await client.getAll()
      setData([...response]);
      setIsLoading(false)
    }  

    getAll()
  } ,[])

  const BasicSpeedDial = () => {
    return (
      <ToDoListProvider>
        <Box sx={{ height: 50, transform: 'translateZ(0px)', flexGrow: 1 }}>
          <SpeedDial
            ariaLabel="SpeedDial basic example"
            sx={{ position: 'absolute', bottom: 16, right: 16 }}
            icon={<SpeedDialIcon />}
          >
            {actions.map((action) => (
              <SpeedDialAction
                key={action.name}
                icon={action.icon}
                tooltipTitle={action.name}
                onClick={() => {
                  let newtask : IToDoListDto = {
                    id: 0,
                    userId: 1,
                    description: "",
                    dateCreated: new Date(),
                    dateCompleted: null
                  }
                
                  setData(data => [...data, newtask]);
                }}
              />
            ))}
          </SpeedDial>
        </Box>
      </ToDoListProvider>
    );
  }
 
  return (
      <Container fixed maxWidth="xl">
        <br />
        <Card sx={{ Width: '50%' }}>
            <CardHeader title={"Todo List"} />
            <CardContent>
              {data.length > 0 && <div style={{ maxWidth: '100%' }}>
                <Grid container spacing={2} justifyContent="center" alignItems="center">
                    <Grid item xs={1}>Completed?</Grid>
                    <Grid item xs={7}>Description</Grid>
                    <Grid item xs={2}>Created Date</Grid>
                    <Grid item xs={2}>Completed Date</Grid>
                    
                    { data.map(item => 
                        <>
                          <Grid item xs={1} ><Checkbox onClick={() => handleDateCompletes(item.id)} checked={item.dateCompleted === null ? false : true }/></Grid>
                          <Grid item xs={7}>{item.description}</Grid>
                          <Grid item xs={2}>{item.dateCreated?.toString()}</Grid>
                          <Grid item xs={2}>{item.dateCompleted?.toString()}</Grid> 
                        </>
                    )}
                </Grid>
              </div>}
          </CardContent>
        </Card>
      </Container>
        /* <br />
        
      <br /> */
  );
}


export default (MainTable)
