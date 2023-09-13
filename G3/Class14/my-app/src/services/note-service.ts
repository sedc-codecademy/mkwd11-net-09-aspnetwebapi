import { CreateNoteModel, EditNoteModel, NoteModel } from "../models/notes";
import { deleteAuth, getAuth, postAuth, putAuth } from "./base-config"

export async function GetNotes(token: string) {
    try {
        const notes = await getAuth<NoteModel[]>("/api/v1/note", token);
        return notes;
    }
    catch (err) {
        console.log(err)
        return [];
    }
}

export async function CreateNote(model: CreateNoteModel, token: string) {
    try {
        const response = await postAuth("/api/v1/note", model, token);
        return (await response.json()) as NoteModel;
    }
    catch (err) {
        console.log(err)
        return null;
    }
}

export async function EditNote(model: EditNoteModel, token: string) {
    try {
        const response = await putAuth("/api/v1/note/" + model.id, model, token);
        return (await response.json()) as NoteModel;
    }
    catch (err) {
        console.log(err)
        return null;
    }
}
export async function DeleteNote(id: number, token: string) {
    try {
        const response = await deleteAuth<NoteModel>("/api/v1/note/" + id, token);
        return response
    }
    catch (err) {
        console.log(err)
        return null;
    }
}