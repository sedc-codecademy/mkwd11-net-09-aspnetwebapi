using NotesAndTagsApp.Models;
using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp
{
    public static class StaticDb
    {
        public static int NoteId = 3;

        public static List<Tag> Tags = new List<Tag>
        {
            new Tag(){ Id = 1, Name = "HomeWork", Color = "cyan"},
            new Tag(){ Id = 2, Name = "SEDC", Color = "blue"},
            new Tag(){ Id = 3, Name = "Healthy", Color = "orange"},
            new Tag(){  Id = 4,  Name = "Priority Medium", Color = "red"},
            new Tag(){  Id = 5,  Name = "exercise", Color = "blue"},
            new Tag(){  Id = 6,  Name = "Priority Low", Color = "yellow"}
        };

        public static List<Note> Notes = new List<Note>
        {
            new Note(){ Id = 1, Text = "Do Homework", Priority = Priority.High, Tags = new List<Tag>()
                {
                    new Tag(){ Id = 1, Name = "HomeWork", Color = "cyan"},
                    new Tag(){ Id = 2, Name = "SEDC", Color = "blue"}
                }
            },
            new Note(){ Id = 2, Text = "Drink more Water", Priority = Priority.Medium, Tags = new List<Tag>()
                {
                    new Tag(){ Id = 3, Name = "Healthy", Color = "orange"},
                    new Tag(){  Id = 4,  Name = "Priority Medium", Color = "red"}
                }
            },
            new Note(){ Id = 3, Text = "Go to the gym", Priority = Priority.Low, Tags = new List<Tag>()
                {
                    new Tag(){Id = 5,  Name = "exercise", Color = "blue"},
                    new Tag(){Id = 6,  Name = "Priority Low", Color = "yellow"}
                }
            }
        };
    }
}
