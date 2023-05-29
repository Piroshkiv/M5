import {Card, CardActionArea, CardContent, CardMedia, Typography} from "@mui/material"
import {FC, ReactElement} from "react";
import {IResources} from "../../../interfaces/resources";
import {useNavigate} from "react-router-dom";

const ResourceCard: FC<IResources> = (props): ReactElement => {

    const navigate = useNavigate()

     return (
        <Card sx={{ maxWidth: 250, backgroundColor: props.color }}>
            <CardActionArea onClick={() => navigate(`/Resource/${props.id}`)}>
                <CardContent>
                    <Typography noWrap gutterBottom variant="h6" component="div">
                        {props.name}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.year}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.pantone_value}
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    )
}

export default ResourceCard