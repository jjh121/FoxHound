import React, { useState, useEffect, ChangeEvent, FormEvent } from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
  TextField,
  Button,
  Grid,
  Paper,
  Container,
  Box,
  Typography,
  Link,
} from "@material-ui/core/";
import { useParams, useHistory } from "react-router-dom";
import axios from "axios";
import { BlogModel } from "./Blogs";

const useStyles = makeStyles((theme) => ({
  root: {
    padding: theme.spacing(2, 3),
  },
}));

const EditBlog: React.FC = () => {
  const { blogId } = useParams();
  const [owner, setOwner] = useState<string>("");
  const [title, setTitle] = useState<string>("");
  const [error, setError] = useState<string>("");
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const history = useHistory();

  useEffect(() => {
    getBlog();
  }, []);

  const getBlog = async () => {
    try {
      setIsLoading(true);

      const response = await axios.get<BlogModel>(
        `${process.env.API_URL}/Blog/Get/${blogId}`
      );

      setOwner(response.data.owner);
      setTitle(response.data.title);
    } catch (ex) {
      setError(ex.message);
    } finally {
      setIsLoading(false);
    }
  };

  const handleBlogEditSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);

      await axios.post(`${process.env.API_URL}/Blog/Update`, {
        blogId: +blogId,
        title: title,
        owner: owner,
      });

      setError("");
      history.push("/");
    } catch (ex) {
      setError(ex.message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const classes = useStyles({});

  return (
    <div>
      {isLoading ? (
        <div>Loading...</div>
      ) : (
        <form onSubmit={handleBlogEditSubmit}>
          <Container maxWidth="sm">
            <Paper className={classes.root} elevation={5}>
              {error && <div>{error}</div>}
              <Typography variant="h5">Edit Blog</Typography>
              <Grid container direction="column">
                <Grid item>
                  <TextField
                    label="Owner"
                    value={owner}
                    onChange={(event: ChangeEvent<HTMLInputElement>) =>
                      setOwner(event.target.value)
                    }
                  />
                </Grid>
                <Grid item>
                  <TextField
                    label="Title"
                    value={title}
                    onChange={(event: ChangeEvent<HTMLInputElement>) =>
                      setTitle(event.target.value)
                    }
                  />
                </Grid>
                <Grid item>
                  <Box mt={2}>
                    <Button
                      type="submit"
                      color="primary"
                      variant="contained"
                      disabled={isSubmitting}
                    >
                      Update Blog
                    </Button>
                  </Box>
                </Grid>
              </Grid>
            </Paper>
          </Container>
        </form>
      )}
    </div>
  );
};

export default EditBlog;
