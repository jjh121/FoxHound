import React, { useState, useEffect, ChangeEvent } from "react";
import Blog from "./Blog";
import axios from "axios";

export interface BlogModel {
  blogId: number;
  owner: string;
  createdDate: Date;
}

const Blogs: React.FC = () => {
  const [blogs, setBlogs] = useState<BlogModel[]>([]);
  const [owner, setOwner] = useState<string>("");
  const [title, setTitle] = useState<string>("");
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>("");

  useEffect(() => {
    getBlogs();
  }, []);

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

  const addBlog = async () => {
    try {
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

      getBlogs();
    } catch (ex) {
      setError(ex.message);
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

      <div>
        <span>Owner: </span>
        <input
          type="text"
          value={owner}
          onChange={(event: ChangeEvent<HTMLInputElement>) =>
            setOwner(event.target.value)
          }
        />
      </div>

      <div>
        <span>Title: </span>
        <input
          type="text"
          value={title}
          onChange={(event: ChangeEvent<HTMLInputElement>) =>
            setTitle(event.target.value)
          }
        />
      </div>

      <div>
        <button onClick={addBlog}>Add Blog</button>
      </div>
    </div>
  );
};

export default Blogs;
