import React, { useState } from 'react';
import { EditNoteModel } from '../../../../models/notes';

interface IEditNoteProps {
    note: EditNoteModel;
    onChange: (newNote: EditNoteModel) => void;
}
const EditNote = (props: IEditNoteProps) => {
    const [title, setTitle] = useState(props.note.title)
    const [description, setDescription] = useState(props.note.description);

    const onUpdate = () => {
        props.onChange({
            id: props.note.id,
            title,
            description
        })
    }
    return <div>
        <div>
            <label htmlFor="title">Title</label>
            <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
        </div>
        <div>
            <label htmlFor="description">Description</label>
            <input type="text" value={description} onChange={(e) => setDescription(e.target.value)} />
        </div>
        <button onClick={onUpdate}>
            Update
        </button>
    </div>
}

export default EditNote;