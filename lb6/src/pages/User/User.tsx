import React, {useContext, useEffect, useState, FC, ReactElement} from 'react'
import {Box, Button, CircularProgress, TextField, Typography} from '@mui/material'
import AddUserStore from "./UserStore";
import {AppStoreContext} from "../../App";
import {observer} from "mobx-react-lite";
import {IUser} from "../../interfaces/users"
import CreateStore from '../../stores/PutStore';
import {useParams} from 'react-router-dom';
import * as usersApi from "../../api/modules/users"


const store = new AddUserStore(new CreateStore);

const Resource: FC<any> = (): ReactElement =>{
    const [user, setUser] = useState<IUser|null>(null)
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const {id} = useParams();

    useEffect( () => {
        if(id) {
            const getResource = async () =>{
                try {
                    setIsLoading(true);
                    const res = await usersApi.getById(id);
                    setUser(res.data);
                    
                } catch (e ) {
                    if(e instanceof Error){
                        console.error(e.message)
                    }
                }
                setIsLoading(false);
            };
            getResource();
        }
    }, [id]);

    return (
        <Box
        sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
        }}
    >
        <Typography component="h1" variant="h5">
            Update resource
        </Typography>
        
        <Box component="form"
             onSubmit={async (event) =>
             {
                 event.preventDefault()
                 await store.add()
             }}
             noValidate
             
             >
            <TextField
                margin="normal"
                required
                fullWidth
                id="first_name"
                label="First name"
                name="first_name"
                type="text"
                value={user?.first_name}
                onChange={(event) => {
                store.changeFirstName(event.target.value)
                setUser((previousState) => {
                    
                    if (previousState) {
                      return { ...previousState, first_name: event.target.value };
                    } else {
                      return null;
                    }
                  });
                }}
                InputLabelProps={{ shrink: true }} 
                autoFocus
            />
           <TextField
                margin="normal"
                required
                fullWidth
                id="second_name"
                label="Second name"
                name="second_name"
                type="text"
                value={user?.last_name}
                onChange={(event) => {
                store.changeSecondName(event.target.value)
                setUser((previousState) => {
                    
                    if (previousState) {
                      return { ...previousState, last_name: event.target.value };
                    } else {
                      return null;
                    }
                  });
                }}
                InputLabelProps={{ shrink: true }} 
                autoFocus
            />
            <TextField
                margin="normal"
                required
                fullWidth
                id="image"
                label="Image"
                name="image"
                type="text"
                value={user?.avatar}
                onChange={(event) => {
                store.changeImage(event.target.value)
                setUser((previousState) => {
                    
                    if (previousState) {
                        return { ...previousState, avatar: event.target.value };
                    } else {
                        return null;
                    }
                    });
                }}
                InputLabelProps={{ shrink: true }} 
                
            />
            <TextField
                margin="normal"
                required
                fullWidth
                id="email"
                label="Email"
                name="email"
                type="text"
                value={user?.email}
                onChange={(event) => {
                    store.changeEmail(event.target.value)
                    setUser((previousState) => {
                        
                        if (previousState) {
                            return { ...previousState, email: event.target.value };
                        } else {
                            return null;
                        }
                        });
                    }}
                    InputLabelProps={{ shrink: true }} 
            />

            <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
            >
                {store.isLoading ? (
                    <CircularProgress />
                ) : (
                    'Submit'
                )}
            </Button>
            {!!store.updatedAt && (
                    <p className="mt-3 mb-3" style={{ color: 'green', fontSize: 14, fontWeight: 700 }}>{`Success! Updated at: ${store.updatedAt}`}</p>
            )}
        </Box>
        
        
    </Box>
    );
   
};



export default observer(Resource);