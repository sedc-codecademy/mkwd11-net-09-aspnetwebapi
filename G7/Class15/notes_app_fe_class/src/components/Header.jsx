import React from "react";
import { NavLink } from "react-router-dom";

const Header = () => {
  return (
    <header className="flex items-center justify-between p-4">
      <h1 className="text-4xl">NotesApp</h1>
      <nav>
        <NavLink to="/" className="btn btn-ghost">
          Home
        </NavLink>

        <NavLink to="/login" className="btn btn-ghost">
          Login
        </NavLink>
      </nav>
    </header>
  );
};

export default Header;
