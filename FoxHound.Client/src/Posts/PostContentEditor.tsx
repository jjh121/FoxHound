import React, { useState } from "react";
import MarkdownEditor from "react-simplemde-editor";
import hljs from "highlight.js";

import "easymde/dist/easymde.min.css";
import "highlight.js/styles/solarized-light.css";

interface IProps {
  id: string;
  content: string;
  updateContent: (value: string) => void;
}

const PostContentEditor: React.FC<IProps> = (props) => {
  const { id, content, updateContent } = props;

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
        }}
        value={content}
        onChange={(value) => updateContent(value)}
      />
    </div>
  );
};

export default PostContentEditor;
