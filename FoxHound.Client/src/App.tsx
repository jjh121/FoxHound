import "react-app-polyfill/stable";

import React from "react";
import ReactDOM from "react-dom";
import NavDrawer from "./Navigation/NavDrawer";
import { BrowserRouter as Router } from "react-router-dom";

const App: React.FC = () => {
  return (
    <Router>
      <NavDrawer />
    </Router>
  );
};

ReactDOM.render(<App />, document.getElementById("root"));
