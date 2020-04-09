import React from "react";
import { BlogModel } from "./Blogs";
import { Link } from "react-router-dom";

interface IProps {
  blog: BlogModel;
}

const Blog: React.FC<IProps> = (props) => {
  const { blog } = props;

  return (
    <div>
      <Link to={`/blog/edit/${blog.blogId}`}>Edit</Link> | {blog.owner}
    </div>
  );
};

export default Blog;
