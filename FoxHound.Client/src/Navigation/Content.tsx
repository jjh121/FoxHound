import React from "react";
import {Switch, Route} from "react-router-dom";
import Blogs from "../Blogs/Blogs";
import AddBlog from "../Blogs/AddBlog";
import AddPost from "../Posts/AddPost";
import AddComment from '../Comments/AddComment';
import Comments, {CommentModel} from "../Comments/Comments";

const fakeComments : CommentModel[] = [
  {
    postId: 1,
    commentId: 1,
    author: 'Bill',
    content: 'I like turtles',
    createdDate: new Date()
  }, {
    postId: 1,
    commentId: 2,
    author: 'James',
    content: 'Me too',
    createdDate: new Date()
  }, {
    postId: 1,
    commentId: 3,
    author: 'Smith',
    content: 'I dont',
    createdDate: new Date()
  }
]

const Content : React.FC = () => {
  return (
    <div>
      <Comments comments={fakeComments}/>
      <Switch>
        <Route path="/blogs">
          <Blogs/>
        </Route>
        <Route path="/addBlog">
          <AddBlog handleBlogAdded={(id : number) => {}}/>
        </Route>
        <Route path="/blog /: blogId / addPost ">
          < AddPost/>
        </Route>
        <Route path="/">
          <Blogs/>
        </Route>
      </Switch >
    </div>
  )
}

export default Content;