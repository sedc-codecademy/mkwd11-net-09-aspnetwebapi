import { useState } from "react";

const Login = (props) => {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");

  const onSubmit = async (e) => {
    e.preventDefault();
    try {
      const result = await fetch(
        "http://localhost:5052/api/user/authenticate",
        {
          headers: {
            "Content-Type": "application/json",
          },
          method: "POST",
          body: JSON.stringify({
            username: userName,
            password: password,
          }),
        }
      );
      const res = await result.json();
      props.loginUser(res);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <form onSubmit={onSubmit}>
      <label htmlFor="userName">User Name</label>
      <input
        type="text"
        name="userName"
        id="userName"
        value={userName}
        onChange={(e) => setUserName(e.target.value)}
      />
      <br />
      <label htmlFor="pass">Password</label>
      <input
        type="password"
        name="pass"
        id="pass"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <br />
      <button type="submit">Login</button>
    </form>
  );
};

export default Login;
