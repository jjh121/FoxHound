import React, { useState, useEffect, ChangeEvent } from "react";
import axios from "axios";
import {
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  ListItemSecondaryAction,
  Avatar,
  IconButton,
  Container,
  Box,
  Paper,
  Typography,
  Divider,
  Tooltip,
} from "@material-ui/core";
import PersonIcon from "@material-ui/icons/Person";
import EditIcon from "@material-ui/icons/Edit";
import SearchIcon from "@material-ui/icons/Search";
import AddCircleOutlineIcon from "@material-ui/icons/AddCircleOutline";
import { Link } from "react-router-dom";

export interface BlogModel {
  blogId: number;
  owner: string;
  title: string;
  createdDate: Date;
}

const Blogs: React.FC = () => {
  const [blogs, setBlogs] = useState<BlogModel[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>("");

  useEffect(() => {
    getBlogs();
  }, []);

  const getBlogs = async () => {
    try {
      setIsLoading(true);

      const response = await axios.get<BlogModel[]>(
        `${process.env.API_URL}/Blog/GetAll`
      );

      setBlogs(response.data);
    } catch (ex) {
      setError(ex.message);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Container maxWidth="lg">
      <Paper elevation={5}>
        <Box p={3}>
          <Box display="flex">
            <Box flexGrow={1}>
              <Typography variant="h5">Blogs</Typography>
            </Box>
            <Box>
              <Tooltip title="Add blog">
                <IconButton component={Link} to="blog/addBlog">
                  <AddCircleOutlineIcon></AddCircleOutlineIcon>
                </IconButton>
              </Tooltip>
            </Box>
          </Box>

          {error && <div>{error}</div>}

          {isLoading ? (
            <div>Loading...</div>
          ) : (
            <List dense>
              {blogs.map((blog) => (
                <>
                  <ListItem key={blog.blogId}>
                    <ListItemAvatar>
                      <Avatar component={Link} to={`/blog/${blog.blogId}`}>
                        <PersonIcon />
                      </Avatar>
                    </ListItemAvatar>
                    <ListItemText primary={blog.title} secondary={blog.owner} />
                    <ListItemSecondaryAction>
                      <Tooltip title="View">
                        <IconButton
                          edge="end"
                          component={Link}
                          to={`/blog/${blog.blogId}`}
                        >
                          <SearchIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Edit">
                        <IconButton
                          edge="end"
                          component={Link}
                          to={`/blog/edit/${blog.blogId}`}
                        >
                          <EditIcon />
                        </IconButton>
                      </Tooltip>
                    </ListItemSecondaryAction>
                  </ListItem>
                  <Divider variant="inset" component="li" />
                </>
              ))}
            </List>
          )}
        </Box>
      </Paper>
    </Container>
  );
};

export default Blogs;
