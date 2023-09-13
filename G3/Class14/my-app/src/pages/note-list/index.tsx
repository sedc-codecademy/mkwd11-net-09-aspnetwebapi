import React, { useEffect, useState } from 'react';
import { EditNoteModel, NoteModel } from '../../models/notes';
import EditNote from './components/edit-note';
import Note from './components/note';

interface INoteListProps {
    onInit: () => void;
    data: NoteModel[]
    onNoteChange: (newNote: EditNoteModel) => void;
    onNoteDelete: (id: number) => void;
}
const NoteList = (props: INoteListProps) => {
    const [editId, setEditId] = useState<number | undefined>(undefined);
    useEffect(() => {
        props.onInit()
    }, []);

    const onChange = (newNote: EditNoteModel) => {
        props.onNoteChange(newNote);
        setEditId(undefined);
    }

    return (
        <div>
            {props.data.map(note => Boolean(editId) && note.id === editId ?
                <EditNote key={note.id} note={note} onChange={onChange} />
                :
                <Note key={note.id} note={note} onEdit={(id) => setEditId(id)} onDelete={props.onNoteDelete} />)}
        </div>
    )
}

export default NoteList;

