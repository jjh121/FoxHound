import React, { useState, useEffect, ChangeEvent } from "react";
import Blog from "./Blog";
import axios from "axios";

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

  const handleBlogAdded = (newBlogId: number) => {
    getBlogs();
  };

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
    </div>
  );
};

export default Blogs;
