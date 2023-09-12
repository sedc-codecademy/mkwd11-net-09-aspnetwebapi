import { createContext, useState } from "react";
import useLocalStorage from "../lib/hooks/useLocalStorage";
import { useNavigate } from "react-router";

export const AuthContext = createContext(null);

const AuthProvider = ({ children }) => {
  const navigate = useNavigate();
  const [user, setUser] = useLocalStorage("auth", null);

  const logoutUser = () => {
    navigate("/");
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, setUser, logoutUser }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
