import React from "react";
import ThemeToggle from "./ThemeToggle";
import { NavLink } from "react-router-dom";
import { navLinks } from "../lib/navLinks";
import { useContext } from "react";
import { AuthContext } from "../contexts/AuthProvider";
import LogoutButton from "./LogoutButton";

const imgSrc =
  "https://images.unsplash.com/photo-1534528741775-53994a69daeb?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1964&q=80";

const Header = () => {
  const { user, setUser, logoutUser } = useContext(AuthContext);

  return (
    <header className=" flex items-center justify-between sticky top-0 z-50 bg-opacity-40 bg-base-100 px-4">
      <NavLink className="btn btn-ghost text-2xl">NotesApp</NavLink>
      <div className="flex items-center">
        <nav className="flex items-center space-x-4  mx-4">
          {navLinks.map((link, index) => {
            return (
              <NavLink
                key={index}
                to={link.to}
                end
                className={({ isActive, isPending }) =>
                  `btn btn-ghost ${isActive ? "text-primary" : ""}`
                }
              >
                {link.title}
              </NavLink>
            );
          })}
        </nav>
        <ThemeToggle />
        {user ? (
          // <button>Logout</button>
          <>
            <div className="dropdown dropdown-hover dropdown-end p-2">
              <label tabIndex={0} className="btn btn-ghost m-1 ">
                <div className="avatar">
                  <div className="w-10 rounded-full">
                    <img src={imgSrc} />
                  </div>
                </div>
                <h4>{`${user.firstName} ${user.lastName}`}</h4>
              </label>
              <ul
                tabIndex={0}
                className="dropdown-content z-[1] menu p-2 shadow  rounded-box w-52 h-full "
              >
                <LogoutButton logoutUser={logoutUser} />
              </ul>
            </div>
          </>
        ) : (
          <NavLink to={"/login"} className="btn btn-ghost">
            Login
          </NavLink>
        )}
      </div>
    </header>
  );
};

export default Header;
