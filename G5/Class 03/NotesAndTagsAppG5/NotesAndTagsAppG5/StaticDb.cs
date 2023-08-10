using NotesAndTagsAppG5.Models;

namespace NotesAndTagsAppG5
{
    public static class StaticDb
    {
        public static List<Note> Notes = new List<Note>
        {
            new Note()
            {
                Text ="Do the homework",
                Priority = Models.Enums.Priority.Medium,
                Tags = new List<Tag>()
                {
                    new Tag(){ Name= "Homework", Color="blue"},
                    new Tag(){ Name = "SEDC", Color = "purple"}
                }
            },
            new Note()
            {
                Text ="Drink more water",
                Priority = Models.Enums.Priority.High,
                Tags = new List<Tag>()
                {
                    new Tag(){ Name= "Health", Color="green"}
                }
            },
             new Note()
            {
                Text ="Go to the gym!",
                Priority = Models.Enums.Priority.Low,
                Tags = new List<Tag>()
                {
                    new Tag(){ Name= "Health", Color="green"},
                    new Tag(){ Name= "Exercise", Color="red"},
                }
            },
        };
    }
}
