import React from 'react';
import { NoteModel } from "../../../../models/notes";

interface INoteProps {
    note: NoteModel;
    onEdit: (id: number) => void;
    onDelete: (id: number) => void;

}
function Note({ note, onEdit, onDelete }: INoteProps) {
    return <div className='note' >
        <div>Title {note.title}</div>
        <div>Description {note.description}</div>
        <div>Tags {note.tags.map(x => x.name).join(",")}</div>
        <button onClick={() => onEdit(note.id)}> Edit</button>
        <button onClick={() => onDelete(note.id)}> Delete</button>
    </div>;
}
export default Note;