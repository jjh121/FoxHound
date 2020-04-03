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
import PostContentViewer from "./PostContentViewer";

const AddPost: React.FC = () => {
  const [title, setTitle] = useState<string>("");
  const [content, setContent] = useState<string>("");
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [error, setError] = useState<string>("");

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);
      const response = await Axios.post<number>(
        "https://localhost:44360/Post/Create",
        {
          blogId: 1,
          title: title,
          content: content,
        }
      );

      const blogId = response.data;
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
                      setContent={setContent}
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

      <PostContentViewer content={content} />
    </div>
  );
};

export default AddPost;