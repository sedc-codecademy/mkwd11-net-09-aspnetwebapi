import axios from "axios";

// Create a new Axios instance with custom configuration
const getAxiosInstance = (user) => {
  const axiosInstance = axios.create({
    baseURL: "https://localhost:7183/api/", // Replace with your API's base URL
    headers: {
      "Access-Control-Allow-Origin": "*", // Allow CORS requests
      Authorization: `Bearer ${user?.token}`, // Set default authorization header
    },
    withCredentials: false, // Required to handle cookies
  });

  return axiosInstance;
};

// You can also add other default headers as needed

export default getAxiosInstance;
