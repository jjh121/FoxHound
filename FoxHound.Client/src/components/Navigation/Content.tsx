import React from "react";
import { Switch, Route } from "react-router-dom";
import Blogs from "../Blogs/Blogs";
import AddBlog from "../Blogs/AddBlog";
import AddPost from "../Posts/AddPost";
import EditBlog from "../Blogs/EditBlog";
import Post from "../Posts/Post";

const Content: React.FC = () => {
  return (
    <Switch>
      <Route path="/blogs">
        <Blogs />
      </Route>

      <Route path="/blog/addBlog">
        <AddBlog />
      </Route>

      <Route path="/blog/:blogId/addPost">
        <AddPost />
      </Route>

      <Route path="/blog/edit/:blogId">
        <EditBlog />
      </Route>

      <Route path="/post/:postId">
        <Post />
      </Route>

      <Route path="/">
        <Blogs />
      </Route>
    </Switch>
  );
};

export default Content;
