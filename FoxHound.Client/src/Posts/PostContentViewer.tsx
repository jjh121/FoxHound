import React from "react";
import { makeStyles } from "@material-ui/core";
import MarkdownEditor from "react-simplemde-editor";
import EasyMDE from "easymde";
import hljs from "highlight.js";

import "easymde/dist/easymde.min.css";
import "highlight.js/styles/solarized-light.css";

interface IProps {
  content: string;
}

const PostContentViewer: React.FC<IProps> = (props) => {
  const { content } = props;

  return (
    <div>
      {
        <MarkdownEditor
          getMdeInstance={(instance) => {
            EasyMDE.togglePreview(instance);
          }}
          options={{
            toolbar: false,
            status: false,
            renderingConfig: { codeSyntaxHighlighting: true, hljs: hljs },
          }}
          value={content}
        />
      }
    </div>
  );
};

export default PostContentViewer;
