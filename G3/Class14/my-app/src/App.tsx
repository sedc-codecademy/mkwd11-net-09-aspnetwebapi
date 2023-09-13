import React, { useEffect, useState } from 'react';
import './App.css';
import Login from './pages/login';
import NoteList from './pages/note-list';
import { CreateNote, DeleteNote, EditNote, GetNotes } from './services/note-service';
import { CreateNoteModel, EditNoteModel, NoteModel } from './models/notes';
import AddNote from './pages/note-list/components/add-note';

function App() {
  const [userToken, setUserToken] = useState<string | null>(null);
  const [notes, setNotes] = useState<NoteModel[]>([]);

  useEffect(() => {
    const token = sessionStorage.getItem("user");
    if (token) {
      setUserToken(token);
    }
  }, [userToken]);

  const onNotesInit = () => {
    GetNotes(userToken!).then(x => setNotes(x));
  }

  const onAddNote = (createModel: CreateNoteModel) => {
    CreateNote(createModel, userToken!).then(x => {
      if (x) {
        setNotes([...notes, x]);
      }
    })
  }
  const onNoteChange = (newNote: EditNoteModel) => {
    EditNote(newNote, userToken!).then(x => {
      if (x) {
        setNotes(notes.map(note => note.id == x.id ? x : note));
      }
    })
  }

  const onNoteDelete = (id: number) => {
    DeleteNote(id, userToken!).then(x => {
      if (x) {
        setNotes(notes.filter(note => note.id !== x.id));
      }
    })
  }

  if (userToken) {
    return <div>
      <AddNote onAddNote={onAddNote}></AddNote>
      <NoteList onNoteDelete={onNoteDelete} onNoteChange={onNoteChange} onInit={onNotesInit} data={notes}></NoteList>
    </div>
  }
  return (
    <div>
      <Login onTokenChange={(token) => setUserToken(token)}></Login>
    </div>
  );
}

export default App;
