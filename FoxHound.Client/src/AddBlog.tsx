import React, { useState, useEffect, ChangeEvent, FormEvent } from "react";
import axios from "axios";
import { makeStyles } from "@material-ui/core/styles";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import Paper from "@material-ui/core/Paper";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";

interface IProps {
  refreshBlogList: () => void;
}

const useStyles = makeStyles((theme) => ({
  root: {
    padding: theme.spacing(2, 3),
  },
}));

const AddBlog: React.FC<IProps> = (props) => {
  const [owner, setOwner] = useState<string>("");
  const [title, setTitle] = useState<string>("");
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [error, setError] = useState<string>("");
  const { refreshBlogList } = props;

  useEffect(() => {}, []);

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);
      const response = await axios.post<number>(
        "https://localhost:44360/Blog/Create",
        {
          title: title,
          owner: owner,
        }
      );

      const blogId = response.data;
      setOwner("");
      setTitle("");
      setError("");
      refreshBlogList();
    } catch (ex) {
      setError(ex.message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const classes = useStyles({});

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <Container maxWidth="sm">
          <Paper className={classes.root} elevation={5}>
            {error && <div>{error}</div>}
            <Typography variant="h5">Add Blog</Typography>
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
                    Add Blog
                  </Button>
                </Box>
              </Grid>
            </Grid>
          </Paper>
        </Container>
      </form>
    </div>
  );
};

export default AddBlog;
