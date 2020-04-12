import React, { useState, FormEvent, ChangeEvent } from "react";
import {
  Container,
  Paper,
  Typography,
  Grid,
  TextField,
  Box,
  Button,
} from "@material-ui/core";
import Axios from "axios";
import { useParams } from "react-router-dom";

interface IProps {
  postId: number;
}

const AddComment: React.FC<IProps> = (props) => {
  const [author, setAuthor] = useState<string>("");
  const [content, setContent] = useState<string>("");
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [error, setError] = useState<string>("");

  const postId = props.postId;

  const updateContent = (updatedContent: string) => {
    setContent(updatedContent);
  };

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);
      const response = await Axios.post<number>(
        `${process.env.API_URL}/Comment/Create`,
        {
          postId: +postId,
          author: author,
          content: content,
        }
      );

      const commentId = response.data;
      setAuthor("");
      setContent("");
      setError("");
    } catch (ex) {
      setError(ex.message);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <Container maxWidth="lg">
          <Paper elevation={5}>
            <Box p={3}>
              {error && <div>{error}</div>}
              <Typography variant="h5">Add Comment</Typography>
              <Grid container direction="column">
                <Grid item>
                  <TextField
                    label="Author"
                    fullWidth
                    value={author}
                    onChange={(event: ChangeEvent<HTMLInputElement>) =>
                      setAuthor(event.target.value)
                    }
                  />
                </Grid>
                <Grid item>
                  <Box mt={2}>
                    <TextField
                      label="Content"
                      value={content}
                      fullWidth
                      multiline
                      rows="6"
                      rowsMax="10"
                      onChange={(event: ChangeEvent<HTMLInputElement>) =>
                        setContent(event.target.value)
                      }
                    />
                  </Box>
                </Grid>
                <Grid item>
                  <Box mt={2}>
                    <Button
                      type="submit"
                      color="primary"
                      variant="contained"
                      disabled={isSubmitting}
                    >
                      Add Comment
                    </Button>
                  </Box>
                </Grid>
              </Grid>
            </Box>
          </Paper>
        </Container>
      </form>
    </div>
  );
};

export default AddComment;
