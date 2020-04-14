import "react-app-polyfill/stable";

import React, { useEffect } from "react";
import ReactDOM from "react-dom";
import NavDrawer from "./components/Navigation/NavDrawer";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { AuthService } from "./services/Authentication/AuthService";
import SigninCallback from "./components/Authentication/SigninCallback";

const authService = new AuthService();

const App: React.FC = () => {
  useEffect(() => {
    console.log("Calling UseEffect");
    authService.getUser().then((user) => {
      if (user) {
        authService.loggedIn = true;
      } else {
        authService.loggedIn = false;
      }
    });
  });
  return (
    <Router>
      <Switch>
        <Route exact path="/signin-callback">
          <SigninCallback />
        </Route>
        <Route exact path="/silent-renew"></Route>
        <Route path="/">
          <NavDrawer authService={authService} />
        </Route>
      </Switch>
    </Router>
  );
};

ReactDOM.render(<App />, document.getElementById("root"));
