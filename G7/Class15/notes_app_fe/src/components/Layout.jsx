import React from "react";
import Header from "./Header";
import { Outlet } from "react-router";
import AuthProvider from "../contexts/AuthProvider";

const Layout = () => {
  return (
    <div className="relative min-h-screen ">
      <AuthProvider>
        <Header />
        <main>
          <Outlet />
        </main>
      </AuthProvider>
    </div>
  );
};

export default Layout;
