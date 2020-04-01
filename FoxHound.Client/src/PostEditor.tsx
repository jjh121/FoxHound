import React, { useState, useEffect } from "react";
import MarkdownEditor from "react-simplemde-editor";
import EasyMDE from "easymde";
import "easymde/dist/easymde.min.css";

const PostEditor: React.FC = () => {
  const [content, setContent] = useState<string>("## Testing");
  const [easyMdeInstance, setEasyMdeInstance] = useState<EasyMDE>(null);

  //   useEffect(() => {
  //     if (easyMdeInstance === null) {
  //       return;
  //     }

  //     EasyMDE.togglePreview(easyMdeInstance);
  //   }, [content]);

  return (
    <div>
      <MarkdownEditor
        options={{}}
        value={content}
        onChange={(value) => setContent(value)}
      />

      <MarkdownEditor
        getMdeInstance={(instance) => {
          setEasyMdeInstance(instance);
          EasyMDE.togglePreview(instance);
        }}
        options={{ toolbar: false, status: false }}
        value={content}
        //onChange={(value) => setContent(value)}
      />

      <div>{content}</div>
    </div>
  );
};

export default PostEditor;
