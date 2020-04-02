import React, { useState } from "react";
import MarkdownEditor from "react-simplemde-editor";
import EasyMDE from "easymde";
import hljs from "highlight.js";

import "easymde/dist/easymde.min.css";
import "highlight.js/styles/solarized-light.css";

interface IProps {
  id: string;
  content: string;
  setContent: React.Dispatch<React.SetStateAction<string>>;
}

const PostContentEditor: React.FC<IProps> = (props) => {
  const { id, content, setContent } = props;

  return (
    <div>
      <MarkdownEditor
        id={id}
        options={{
          hideIcons: ["guide"],
          showIcons: ["code", "strikethrough", "table", "horizontal-rule"],
          renderingConfig: { codeSyntaxHighlighting: true, hljs: hljs },
          autosave: {
            enabled: true,
            uniqueId: id,
          },
          //previewClass: "background-color: red;",
        }}
        value={content}
        onChange={(value) => setContent(value)}
      />
    </div>
  );
};

export default PostContentEditor;
