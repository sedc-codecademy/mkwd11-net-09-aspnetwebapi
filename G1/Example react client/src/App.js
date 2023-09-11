import React, { useState } from "react";
import "./App.css";
import HomePage from "./Home";
import Login from "./Login";

function App() {
  const [selectedPage, setSelectedPage] = useState("login");
  const [loggedUser, setLoggeduser] = useState(null);
  return (
    <div className="App">
      {!loggedUser ? (
        <div>
          <span onClick={() => setSelectedPage("login")}>Login</span>
          <span onClick={() => setSelectedPage("register")}>Register</span>

          {selectedPage === "login" ? (
            <Login loginUser={(user) => setLoggeduser(user)} />
          ) : (
            <div></div>
          )}
        </div>
      ) : (
        <HomePage user={loggedUser} />
      )}
      <div></div>
    </div>
  );
}

export default App;
