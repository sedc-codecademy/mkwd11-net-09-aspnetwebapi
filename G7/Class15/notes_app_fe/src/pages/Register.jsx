import { useState, useContext, useEffect } from "react";
import getAxiosInstance from "../lib/axiosInstance";
import { AuthContext } from "../contexts/AuthProvider";
import { NavLink, useNavigate } from "react-router-dom";

const inputList = [
  {
    label: "Firstname",
    name: "firstName",
    type: "text",
    placeholder: "firstname",
  },
  {
    label: "Lastname",
    name: "lastName",
    type: "text",
    placeholder: "lastname",
  },
  {
    label: "Username",
    name: "username",
    type: "text",
    placeholder: "username",
  },
  {
    label: "Age",
    name: "age",
    type: "number",
    placeholder: "0",
  },
  {
    label: "Password",
    name: "password",
    type: "password",
    placeholder: "password",
  },
  {
    label: "ConfirmationPassword",
    name: "confirmationPassword",
    type: "password",
    placeholder: "confirm password",
  },
];

const Register = () => {
  // Define an initial state object with keys for input values
  const initialFormData = {
    username: "",
    password: "",
    firstName: "",
    lastName: "",
    age: 0,
    confirmationPassword: "",
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
        url: "/User/register",
        data: formData,
      });

      console.log(res);
      navigate("/login");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="hero min-h-screen bg-base-200">
      <div className="hero-content flex-col lg:flex-row-reverse">
        <div className="text-center lg:text-left">
          <h1 className="text-5xl font-bold">Register now!</h1>
          <p className="py-6">
            Provident cupiditate voluptatem et in. Quaerat fugiat ut assumenda
            excepturi exercitationem quasi. In deleniti eaque aut repudiandae et
            a id nisi.
          </p>
          <p>
            Already have an account?{" "}
            <NavLink to="/login" className="text-primary font-bold">
              Login
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
                    value={formData[input.name]}
                    placeholder={input.placeholder}
                    className="input input-bordered"
                    onChange={handleInputChange}
                  />
                </div>
              );
            })}

            <div className="form-control mt-6">
              <button className="btn btn-primary" onClick={handleSubmit}>
                Register
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
