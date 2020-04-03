import React, { useState, useEffect, ChangeEvent } from "react";
import Blog from "./Blog";
import axios from "axios";
import AddBlog from "./AddBlog";
import { Box } from "@material-ui/core";
import AddPost from "./Posts/AddPost";

export interface BlogModel {
  blogId: number;
  owner: string;
  createdDate: Date;
}

const Blogs: React.FC = () => {
  const [blogs, setBlogs] = useState<BlogModel[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>("");

  useEffect(() => {
    getBlogs();
  }, []);

  const handleBlogAdded = (newBlogId: number) => {
    getBlogs();
  };

  const getBlogs = async () => {
    try {
      setIsLoading(true);

      const response = await axios.get<BlogModel[]>(
        "https://localhost:44360/Blog/GetAll"
      );

      setBlogs(response.data);
    } catch (ex) {
      setError(ex.message);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div>
      <h3>Blogs</h3>

      {error && <div>{error}</div>}

      {isLoading ? (
        <div>Loading...</div>
      ) : (
        <ul>
          {blogs.map((blog) => (
            <li key={blog.blogId}>
              <Blog blog={blog} />
            </li>
          ))}
        </ul>
      )}

      <AddBlog handleBlogAdded={handleBlogAdded} />

      <Box my={3}>
        <AddPost />
      </Box>
    </div>
  );
};

export default Blogs;
