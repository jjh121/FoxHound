import React, { useEffect } from "react";
import { UserManager } from "oidc-client";
import { useHistory } from "react-router-dom";

const SigninCallback = () => {
  const history = useHistory();

  useEffect(() => {
    new UserManager({ response_mode: "query" })
      .signinRedirectCallback()
      .then((user) => {
        console.info("Processing user...");
        console.log(user);
        history.push("/");
      })
      .catch((e) => {
        console.error(e);
      });
  });

  return (
    <div className="loading">
      <h4>Loading...</h4>
    </div>
  );
};

export default SigninCallback;
