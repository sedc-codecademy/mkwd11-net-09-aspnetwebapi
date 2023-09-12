import React, { useState, useEffect, useContext } from "react";

import { useParams } from "react-router-dom";
import getAxiosInstance from "../lib/axiosInstance";
import { PriorityEnumMap, TagEnumMap } from "../lib/utils";
import { NavLink } from "react-router-dom";
import { AuthContext } from "../contexts/AuthProvider";
import axios from "axios";

const Note = () => {
  const [note, setNote] = useState(null);
  const { noteId } = useParams();

  const { user } = useContext(AuthContext);
  const axiosInstance = getAxiosInstance(user);

  const getNote = async () => {
    try {
      const res = await axios({
        method: "get",
        url: `https://localhost:7183/api/Note/${noteId}`,
        headers: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      // const res = await axiosInstance({
      //   method: "get",
      //   url: `/Note/${noteId}`,
      // });
      console.log(res.data);
      setNote(res.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    getNote();
  }, []);

  if (!note) return null;

  let priorityName = PriorityEnumMap[note.priority].name;
  let priorityColor = PriorityEnumMap[note.priority].color;
  let tagName = TagEnumMap[note.tag].name;
  let tagColor = TagEnumMap[note.tag].color;

  return (
    <div className="w-full md:w-1/2 mx-auto p-4 just flex justify-center">
      <div className="card w-96 bg-base-100 shadow-xl border-2 p-4">
        <div className="card-actions justify-start">
          <NavLink to={"/notes"} className="btn btn-primary">
            Back
          </NavLink>
        </div>
        <div className="card-body">
          <div>
            <h2 className="card-title">{note.userFullName}</h2>
            <div className={`badge ${priorityColor}`}>{priorityName}</div>
            <div className={`badge ${tagColor}`}>{tagName}</div>
            <p className="text-lg">{note.text}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Note;
