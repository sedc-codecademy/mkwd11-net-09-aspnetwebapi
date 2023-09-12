import { useState, useContext, useEffect } from "react";
import { PriorityEnumMap, TagEnumMap } from "../lib/utils";
import { AuthContext } from "../contexts/AuthProvider";
import { useNavigate } from "react-router";
import getAxiosInstance from "../lib/axiosInstance";

const initialFormData = {
  text: "",
  tag: 1,
  priority: 1,
};

const CreateNote = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState(initialFormData);
  const { user } = useContext(AuthContext);
  const axiosInstance = getAxiosInstance(user);
  const [isDisabled, setIsDisabled] = useState(true);

  // Function to handle input changes and update the formData object
  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  useEffect(() => {
    if (
      formData.text.length == 0 ||
      formData.tag > 3 ||
      formData.tag <= 0 ||
      formData.priority > 3 ||
      formData.priority <= 0
    ) {
      setIsDisabled(formData);
    } else {
      setIsDisabled(false);
    }
  }, [formData]);

  useEffect(() => {
    if (!user) {
      navigate("/login");
    }
  }, [user]);

  const handleSubmit = async () => {
    try {
      const body = {
        text: formData.text,
        tag: +formData.tag,
        priority: +formData.priority,
        userId: user.id,
      };
      console.log(body);
      const res = await axiosInstance({
        method: "post",
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
      <h2 className="text-4xl font-bold">Create Note</h2>
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
            <button
              disabled={isDisabled}
              className="btn btn-primary"
              onClick={handleSubmit}
            >
              Create
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CreateNote;
