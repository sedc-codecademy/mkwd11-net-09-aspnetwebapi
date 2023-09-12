import React, { useEffect, useContext, useState } from "react";
import { useNavigate } from "react-router";
import { AuthContext } from "../contexts/AuthProvider";
import getAxiosInstance from "../lib/axiosInstance";
import { PriorityEnumMap, TagEnumMap } from "../lib/utils";
import { NavLink } from "react-router-dom";

const Notes = () => {
  const navigate = useNavigate();
  const { user } = useContext(AuthContext);
  const axiosInstance = getAxiosInstance(user);

  const [notes, setNotes] = useState(null);
  const fetchNotes = async () => {
    try {
      const res = await axiosInstance({
        method: "get",
        url: "/Note",
      });
      console.log(res.data);
      setNotes(res.data.reverse());
    } catch (error) {
      console.log(error);
    }
  };

  const handleDelete = async (id) => {
    try {
      await axiosInstance({
        method: "delete",
        url: `/Note/${id}`,
      });
      fetchNotes();
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchNotes();
  }, []);

  useEffect(() => {
    if (!user) {
      navigate("/login");
    }
  }, [user]);

  return (
    <div className="w-full min-h-screen md:w-1/2 mx-auto p-4">
      <div className="flex flex-col gap-2 ">
        <div className="w-full flex justify-between items-center mb-4">
          <h1 className="text-4xl font-bold">Notes</h1>
          <NavLink to={"create"} className={"btn btn-primary"}>
            Create Note
          </NavLink>
        </div>
        {notes &&
          notes.map((note) => {
            let priorityName = PriorityEnumMap[note.priority].name;
            let priorityColor = PriorityEnumMap[note.priority].color;
            let tagName = TagEnumMap[note.tag].name;
            let tagColor = TagEnumMap[note.tag].color;

            return (
              <div
                key={note.id}
                className="border border-primary-focus p-2 flex items-center justify-between"
              >
                <div>
                  <h2>{note.userFullName}</h2>
                  <div className={`badge ${priorityColor}`}>{priorityName}</div>
                  <div className={`badge ${tagColor}`}>{tagName}</div>
                  <p className="text-lg">{note.text}</p>
                </div>
                <div className="flex flex-col items-end gap-1">
                  <NavLink to={`${note.id}`} className="btn btn-primary btn-sm">
                    Info
                  </NavLink>
                  {`${user.firstName} ${user.lastName}` ===
                  note.userFullName ? (
                    <>
                      <NavLink
                        to={`update/${note.id}`}
                        className="btn btn-warning btn-sm"
                      >
                        Edit
                      </NavLink>
                      <button
                        onClick={() => handleDelete(note.id)}
                        className="btn btn-error btn-sm"
                      >
                        Delete
                      </button>
                    </>
                  ) : null}
                </div>
              </div>
            );
          })}
      </div>
    </div>
  );
};

export default Notes;
