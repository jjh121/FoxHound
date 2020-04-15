import React, { useState, useEffect, ChangeEvent, FormEvent } from "react";
import axios from "axios";
import { makeStyles } from "@material-ui/core/styles";
import {
  TextField,
  Button,
  Grid,
  Paper,
  Container,
  Box,
  Typography,
} from "@material-ui/core/";
import { useHistory } from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  root: {
    padding: theme.spacing(2, 3),
  },
}));

const AddBlog: React.FC = () => {
  const [owner, setOwner] = useState<string>("");
  const [title, setTitle] = useState<string>("");
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [error, setError] = useState<string>("");
  const history = useHistory();

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);
      const response = await axios.post<number>(
        `${process.env.API_URL}/Blog/Create`,
        {
          title: title,
          owner: owner,
        }
      );

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
