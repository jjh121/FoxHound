import React, { useState } from "react";
import MarkdownEditor from "react-simplemde-editor";
import EasyMDE from "easymde";
import hljs from "highlight.js";

import "easymde/dist/easymde.min.css";
import "highlight.js/styles/solarized-light.css";

const PostEditor: React.FC = () => {
  const [content, setContent] = useState<string>("## Testing");

  return (
    <div>
      <MarkdownEditor
        options={{
          hideIcons: ["guide"],
          showIcons: ["code", "strikethrough", "table", "horizontal-rule"],
          renderingConfig: { codeSyntaxHighlighting: true, hljs: hljs },
        }}
        value={content}
        onChange={(value) => setContent(value)}
      />

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
    </div>
  );
};

export default PostEditor;
