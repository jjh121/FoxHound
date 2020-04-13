import React from "react";
import { ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import BathtubIcon from "@material-ui/icons/Bathtub";
import { AuthService } from "../../services/Authentication/AuthService";

interface IProps {
  button: boolean;
  loggedIn: boolean;
  authService: AuthService;
}

const LoginButton: React.FC<IProps> = ({ button, loggedIn, authService }) => {
  let handler: () => void;
  let buttonText: string;

  const logout = () => {
    authService.logout();
  };

  const login = () => {
    console.log("Called login");
    authService.login();
  };

  if (loggedIn) {
    buttonText = "Log Out";
    handler = logout;
  } else {
    buttonText = "Log In";
    handler = login;
  }

  return (
    <ListItem button onClick={handler}>
      <ListItemIcon>
        <BathtubIcon />
      </ListItemIcon>
      <ListItemText>{buttonText}</ListItemText>
    </ListItem>
  );
};

export default LoginButton;
