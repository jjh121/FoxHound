import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Comments, { CommentModel } from "../Comments/Comments";
import Axios from "axios";
import { Container, Paper, Box, Typography } from "@material-ui/core";
import PostContentViewer from "./PostContentViewer";
import AddComment from "../Comments/AddComment";

export interface PostModel {
  postId: number;
  blogId: number;
  title: string;
  content: string;
  createdDate: Date;
  comments: CommentModel[];
}

const Post: React.FC = () => {
  const { postId } = useParams();
  const [post, setPost] = useState<PostModel>(null);
  const [error, setError] = useState<string>("");
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    getPost();
  }, []);

  const getPost = async () => {
    try {
      setIsLoading(true);

      const response = await Axios.get<PostModel>(
        `${process.env.API_URL}/Post/Get/${postId}`
      );

      setPost(response.data);
    } catch (ex) {
      setError(ex.message);
    } finally {
      setIsLoading(false);
    }
  };

  const commentAdded = (comment: CommentModel) => {
    setPost((prevState) => {
      const postCopy = { ...prevState };
      postCopy.comments.push(comment);
      return postCopy;
    });
  };

  return (
    <div>
      {error && <div>{error}</div>}

      {isLoading || !post ? (
        <div>Loading...</div>
      ) : (
        <>
          <Container maxWidth="lg">
            <Paper elevation={5}>
              <Box p={3}>
                {error && <div>{error}</div>}
                <Typography variant="subtitle1">{`Show blog information for blogId: ${post.blogId}`}</Typography>
                <Typography variant="h5">{post.title}</Typography>
                <Typography variant="caption">
                  {new Date(post.createdDate).toDateString()}
                </Typography>
                <Box mt={2}>
                  <PostContentViewer content={post.content} />
                </Box>
              </Box>
            </Paper>
          </Container>

          <AddComment postId={postId} commentAddedSuccessfully={commentAdded} />
          <Comments comments={post.comments} />
        </>
      )}
    </div>
  );
};

export default Post;
