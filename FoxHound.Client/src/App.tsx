import "react-app-polyfill/stable";

import React from "react";
import ReactDOM from "react-dom";
import Blogs from "./Blogs";

const App: React.FC = () => {
  return (
    <>
      <div>Hello from react</div>
      <Blogs />
    </>
  );
};

ReactDOM.render(<App />, document.getElementById("root"));
