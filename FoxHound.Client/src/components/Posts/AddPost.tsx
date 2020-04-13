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
import PostContentEditor from "./PostContentEditor";
import { useParams } from "react-router-dom";

const AddPost: React.FC = () => {
  const [title, setTitle] = useState<string>("");
  const [content, setContent] = useState<string>("");
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [error, setError] = useState<string>("");
  const { blogId } = useParams();

  const updateContent = (updatedContent: string) => {
    setContent(updatedContent);
  };

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);
      const response = await Axios.post<number>(
        `${process.env.API_URL}/Post/Create`,
        {
          blogId: +blogId,
          title: title,
          content: content,
        }
      );

      const postId = response.data;
      setTitle("");
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
              <Typography variant="h5">Add Post</Typography>
              <Grid container direction="column">
                <Grid item>
                  <TextField
                    label="Title"
                    fullWidth
                    value={title}
                    onChange={(event: ChangeEvent<HTMLInputElement>) =>
                      setTitle(event.target.value)
                    }
                  />
                </Grid>
                <Grid item>
                  <Box mt={2}>
                    <PostContentEditor
                      id="add-post"
                      content={content}
                      updateContent={updateContent}
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
                      Add Post
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

export default AddPost;
