import React from "react";
import { LuLogOut } from "react-icons/lu";

const LogoutButton = (props) => {
  return (
    <button onClick={props.logoutUser} className="btn btn-primary btn-md">
      <LuLogOut className="inline-block" /> Logout
    </button>
  );
};

export default LogoutButton;
