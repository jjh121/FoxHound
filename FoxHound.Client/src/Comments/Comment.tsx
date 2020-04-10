import React from "react";
import {CommentModel} from "./Comments";
import {makeStyles} from "@material-ui/core/styles";
import {Paper, Container} from "@material-ui/core/";

interface IProps {
    comment : CommentModel;
}

const Comment : React.FC < IProps > = (props) => {
    const useStyles = makeStyles((theme) => ({
        root: {
            padding: theme.spacing(2, 3),
            marginBottom: '10px'
        }
    }));

    const classes = useStyles({});
    const {comment} = props;

    return (
        <div>
            <Container maxWidth="sm">
                <Paper className={classes.root} elevation={5}>
                    <b>{comment.author}</b>
                    &nbsp;said:
                    <br/> {comment.content}
                    <br/>
                    <small>
                        <i>{comment
                                .createdDate
                                .toDateString()} {comment
                                .createdDate
                                .toTimeString()}</i>
                    </small>
                </Paper>
            </Container>
        </div>
    );
};

export default Comment;