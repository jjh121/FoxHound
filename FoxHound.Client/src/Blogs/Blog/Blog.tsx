import React from "react";
import { BlogModel } from "../Blogs";

interface IProps {
  blog: BlogModel;
}

const Blog: React.FC<IProps> = (props) => {
  const { blog } = props;

  return (
    <div>
      {blog.owner} | {blog.blogId}
    </div>
  );
};

export default Blog;
