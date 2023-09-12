namespace SEDC.NotesApp.Dtos.Notes
{
    public class NoteDto
    {
        public string Text { get; set; }
        public int Priority { get; set; }
        public int Tag { get; set; }
        public string UserFullName { get; set; } //this is why we do a join with the Users table
        //to be able to get FirstName and LastName for the related user
    }
}
