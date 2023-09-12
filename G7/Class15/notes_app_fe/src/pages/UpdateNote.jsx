import { useState, useContext, useEffect } from "react";
import { PriorityEnumMap, TagEnumMap } from "../lib/utils";
import { AuthContext } from "../contexts/AuthProvider";
import { useNavigate } from "react-router";
import getAxiosInstance from "../lib/axiosInstance";
import { useParams } from "react-router-dom";

const initialFormData = {
  text: "",
  tag: 0,
  priority: 0,
};

const UpdateNote = () => {
  const navigate = useNavigate();
  let { id } = useParams();
  const [formData, setFormData] = useState(initialFormData);
  const { user } = useContext(AuthContext);
  const axiosInstance = getAxiosInstance(user);

  // Function to handle input changes and update the formData object
  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const getNote = async () => {
    try {
      const res = await axiosInstance({
        method: "get",
        url: `/Note/${id}`,
      });
      console.log(res.data);
      setFormData({
        text: res.data.text,
        tag: res.data.tag,
        priority: res.data.priority,
      });
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    getNote();
  }, []);

  useEffect(() => {
    if (!user) {
      navigate("/login");
    }
  }, [user]);

  const handleSubmit = async () => {
    try {
      const body = {
        id: +id,
        text: formData.text,
        tag: +formData.tag,
        priority: +formData.priority,
        userId: user.id,
      };
      console.log(body);
      const res = await axiosInstance({
        method: "put",
        url: "/Note",
        data: body,
      });

      navigate("/notes");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="flex flex-col pd-4 md:pt-10 lg:pt-20 items-center justify-center">
      <h2 className="text-4xl font-bold">Update Note</h2>
      <div className="card flex-shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
        <div className="card-body">
          <div className="form-control">
            <label className="label">
              <span className="label-text">Text</span>
            </label>
            <input
              name="text"
              type="text"
              placeholder="Note Text"
              className="input input-bordered"
              value={formData.text}
              onChange={handleInputChange}
            />
          </div>
          <label className="label">
            <span className="label-text">Tags</span>
          </label>
          <select
            className="select select-bordered join-item"
            name="tag"
            value={formData.tag}
            onChange={handleInputChange}
          >
            {Object.keys(TagEnumMap).map((key) => {
              return (
                <option value={key} key={key}>
                  {TagEnumMap[key].name}
                </option>
              );
            })}
          </select>
          <label className="label">
            <span className="label-text">Priority</span>
          </label>
          <select
            className="select select-bordered join-item"
            name="priority"
            value={formData.priority}
            onChange={handleInputChange}
          >
            {Object.keys(PriorityEnumMap).map((key) => {
              return (
                <option value={key} key={key}>
                  {PriorityEnumMap[key].name}
                </option>
              );
            })}
          </select>

          <div className="form-control mt-6">
            <button className="btn btn-primary" onClick={handleSubmit}>
              Update
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UpdateNote;
