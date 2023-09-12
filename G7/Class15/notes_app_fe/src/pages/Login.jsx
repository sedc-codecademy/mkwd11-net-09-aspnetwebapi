import { useState, useContext, useEffect } from "react";
import getAxiosInstance from "../lib/axiosInstance";
import { AuthContext } from "../contexts/AuthProvider";
import { NavLink, useNavigate } from "react-router-dom";

const inputList = [
  {
    label: "Username",
    name: "username",
    type: "text",
    placeholder: "username",
  },
  {
    label: "Password",
    name: "password",
    type: "password",
    placeholder: "password",
  },
];

const Login = () => {
  //   const [username, setUsername] = useState("");
  //   const [password, setPassword] = useState("");

  // Define an initial state object with keys for input values
  const initialFormData = {
    username: "",
    password: "",
  };

  const navigate = useNavigate();

  const { user, setUser } = useContext(AuthContext);
  const axiosInstance = getAxiosInstance(user);

  useEffect(() => {
    if (user) {
      console.log(user);
      navigate("/notes");
    }
  }, [user]);

  // Create a state variable for the form data
  const [formData, setFormData] = useState(initialFormData);

  // Function to handle input changes and update the formData object
  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async () => {
    try {
      const res = await axiosInstance({
        method: "post",
        url: "/User/login",
        data: formData,
      });

      console.log(res);
      setUser(res.data);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="hero min-h-screen bg-base-200">
      <div className="hero-content flex-col lg:flex-row-reverse">
        <div className="text-center lg:text-left">
          <h1 className="text-5xl font-bold">Login now!</h1>
          <p className="py-6">
            Provident cupiditate voluptatem et in. Quaerat fugiat ut assumenda
            excepturi exercitationem quasi. In deleniti eaque aut repudiandae et
            a id nisi.
          </p>
          <p>
            Dont have an account?{" "}
            <NavLink to="/register" className="text-primary font-bold">
              Register
            </NavLink>
          </p>
        </div>
        <div className="card flex-shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
          <div className="card-body">
            {inputList.map((input, index) => {
              return (
                <div key={index} className="form-control">
                  <label className="label">
                    <span className="label-text">{input.label}</span>
                  </label>
                  <input
                    name={input.name}
                    type={input.type}
                    placeholder={input.placeholder}
                    className="input input-bordered"
                    onChange={handleInputChange}
                  />
                </div>
              );
            })}
            <label className="label">
              <a href="#" className="label-text-alt link link-hover">
                Forgot password?
              </a>
            </label>
            <div className="form-control mt-6">
              <button className="btn btn-primary" onClick={handleSubmit}>
                Login
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
