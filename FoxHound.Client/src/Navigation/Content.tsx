import React from "react";
import { Switch, Route } from "react-router-dom";
import Blogs from "../Blogs";
import AddBlog from "../AddBlog";
import AddPost from "../Posts/AddPost";

const Content: React.FC = () => {
  return (
    <Switch>
      <Route path="/blogs">
        <Blogs />
      </Route>

      <Route path="/addBlog">
        <AddBlog handleBlogAdded={(id: number) => {}} />
      </Route>

      <Route path="/blog/:blogId/addPost">
        <AddPost />
      </Route>

      <Route path="/">
        <AddPost />
      </Route>
    </Switch>
  );
};

export default Content;
