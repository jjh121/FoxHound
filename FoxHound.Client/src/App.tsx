import "react-app-polyfill/stable";

import React from "react";
import ReactDOM from "react-dom";
import NavDrawer from "./Navigation/NavDrawer";
import { BrowserRouter as Router } from "react-router-dom";
import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import {
  ReactPlugin,
  withAITracking,
} from "@microsoft/applicationinsights-react-js";
import { createBrowserHistory } from "history";

const browserHistory = createBrowserHistory({ basename: "" });
const reactPlugin = new ReactPlugin();
const appInsights = new ApplicationInsights({
  config: {
    instrumentationKey: process.env.APP_INSIGHTS_INSTRUMENTATION_KEY,
    extensions: [reactPlugin],
    extensionConfig: {
      [reactPlugin.identifier]: { history: browserHistory },
    },
  },
});
appInsights.loadAppInsights();

const App: React.ComponentClass = withAITracking(reactPlugin, () => {
  return (
    <Router>
      <NavDrawer />
    </Router>
  );
});

ReactDOM.render(<App />, document.getElementById("root"));
