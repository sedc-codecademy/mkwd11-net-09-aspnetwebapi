import React, { useState } from 'react'
import { CreateNoteModel } from '../../../../models/notes';

interface IAddNoteProps {
    onAddNote: (createModel: CreateNoteModel) => void;
}
const AddNote = (props: IAddNoteProps) => {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const addNote = () => {
        props.onAddNote({
            title,
            description
        })
        setTitle("")
        setDescription("");
    };

    return <div>
        <div>
            <label htmlFor="title">Title</label>
            <input value={title} onChange={(e) => setTitle(e.target.value)} id='title' />
        </div>
        <div>
            <label htmlFor="description">Description</label>
            <input value={description} onChange={(e) => setDescription(e.target.value)} id='description' />
        </div>
        <button onClick={addNote}>
            Create note
        </button>
    </div>
}

export default AddNote;