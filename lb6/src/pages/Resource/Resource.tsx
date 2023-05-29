import React, {useContext, useEffect, useState, FC, ReactElement} from 'react'
import {Box, Button, CircularProgress, TextField, Typography, Card, CardActionArea, CardContent, CardMedia,} from '@mui/material'
import {AppStoreContext} from "../../App";
import {observer} from "mobx-react-lite";
import {IResources} from "../../interfaces/resources"
import {useParams} from 'react-router-dom';
import PutStore from "../../stores/PutResourceStore";
import ResourceStore from './ResourceStore'


import * as resourceApi from "../../api/modules/resources"
import { wait } from '@testing-library/user-event/dist/utils';

const store = new ResourceStore(new PutStore);


const Resource: FC<any> = (): ReactElement =>{
    const [resource, setResource] = useState<IResources|null>(null)
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const {id} = useParams();

    useEffect( () => {
        if(id) {
            const getResource = async () =>{
                try {
                    setIsLoading(true);
                    const res = await resourceApi.getById(id);
                    setResource(res.data);
                    
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
                id="name"
                label="Name"
                name="name"
                type="text"
                value={resource?.name}
                onChange={(event) => {
                store.changeName(event.target.value)
                  setResource((previousState) => {
                    
                    if (previousState) {
                      return { ...previousState, name: event.target.value };
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
                id="color"
                label="Color"
                name="color"
                value={resource?.color}
                onChange={(event) => {
                store.changeColor(event.target.value)
                  setResource((previousState) => {
                    
                    if (previousState) {
                      return { ...previousState, color: event.target.value };
                    } else {
                      return null;
                    }
                  });
                }}
                InputLabelProps={{ shrink: true }} 
                type="color"
            />
            <TextField
                margin="normal"
                required
                fullWidth
                id="year"
                label="Year"
                name="year"
                type="text"
                value={resource?.year}
                onChange={(event) => {
                store.changeYear(event.target.value)
                    setResource((previousState) => {
                    
                    if (previousState) {
                        return { ...previousState, year: event.target.value };
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
                id="pantone_value"
                label="Pantone value"
                name="pantone_value"
                type="text"
                value={resource?.pantone_value}
                onChange={(event) => {
                    store.changePantoneValue(event.target.value)
                        setResource((previousState) => {
                        
                        if (previousState) {
                            return { ...previousState, pantone_value: event.target.value };
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