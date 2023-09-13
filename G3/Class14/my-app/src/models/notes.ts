export interface NoteModel {
    id: number;
    title: string;
    description?: string;
    tags: TagModel[];
}
export interface EditNoteModel {
    id: number;
    title: string;
    description?: string;
}
export interface CreateNoteModel {
    title: string;
    description?: string;
}
export interface TagModel {
    id: number;
    name: string;
}