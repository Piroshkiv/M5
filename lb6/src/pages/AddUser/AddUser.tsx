import React, {useContext} from 'react'
import {Box, Button, CircularProgress, TextField, Typography} from '@mui/material'
import AddUserStore from "./AddUserStore";
import {AppStoreContext} from "../../App";
import {observer} from "mobx-react-lite";
import {IUser} from "../../interfaces/users"
import CreateStore from '../../stores/CreateStore';

const store = new AddUserStore(new CreateStore);
const AddUser = observer(() => {
    //const appStore = useContext(AppStoreContext);
    
    
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
                Add User
            </Typography>
            
            <form onSubmit={async (event) =>
                 {
                     event.preventDefault()
                     await store.add()
                 }}
                 >
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="name"
                    label="Name"
                    name="name"
                    autoComplete="name"
                    onChange={(event) => store.changeName(event.target.value)}
                    autoFocus
                />
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    name="job"
                    label="Job"
                    type="job"
                    id="job"
                    onChange={(event) => store.changeJob(event.target.value)}
                    autoComplete="current-password"
                />
                {!!store.error &&(
                    <p style={{ color: 'red', fontSize: 14 }}>{store.error}</p>
                )}
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
                {!!store.id && (
                    <p className="mt-3 mb-3" style={{ color: 'green', fontSize: 14, fontWeight: 700 }}>{`Success! Id is: ${store.id}`}</p>
                )}
            </form>
            
        </Box>
    )
})

export default AddUser
