import React from "react";
import Comment from './Comment';

export interface CommentModel {
    postId : number;
    commentId : number;
    author : string;
    content : string;
    createdDate : Date;
}

interface IProps {
    comments : CommentModel[];
}

const Comments : React.FC < IProps > = (props) => {
    const {comments} = props;

    return (
        <div>
            {comments.map((comment) => (<Comment key={comment.commentId} comment={comment}/>))}
        </div>
    );
};

export default Comments;