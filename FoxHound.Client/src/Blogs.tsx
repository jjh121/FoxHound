import React, { useState, useEffect, ChangeEvent } from "react";
import Blog from "./Blog";

export interface BlogModel {
  blogId: number;
  owner: string;
  createdDate: Date;
}

const blogsExample: BlogModel[] = [
  { blogId: 1, owner: "Jon", createdDate: new Date() },
  { blogId: 2, owner: "Tim", createdDate: new Date() },
  { blogId: 3, owner: "Ryan", createdDate: new Date() },
  { blogId: 4, owner: "Kevin", createdDate: new Date() },
];

const Blogs: React.FC = () => {
  const [blogs, setBlogs] = useState<BlogModel[]>([]);
  const [owner, setOwner] = useState<string>("");

  useEffect(() => {
    setBlogs(blogsExample);
  }, []);

  const addBlog = () => {
    const newBlog: BlogModel = {
      blogId: Math.floor(Math.random() * 100),
      owner: owner,
      createdDate: new Date(),
    };

    setBlogs((prevBlogs) => [...prevBlogs, newBlog]);
    setOwner("");
  };

  return (
    <div>
      <h3>Blogs</h3>
      <ul>
        {blogs.map((blog) => (
          <li key={blog.blogId}>
            <Blog blog={blog} />
          </li>
        ))}
      </ul>

      <input
        type="text"
        value={owner}
        onChange={(event: ChangeEvent<HTMLInputElement>) =>
          setOwner(event.target.value)
        }
      />

      <button
        onClick={() => {
          addBlog();
        }}
      >
        Add Blog
      </button>

      <div>Owner: {owner}</div>
    </div>
  );
};

export default Blogs;
