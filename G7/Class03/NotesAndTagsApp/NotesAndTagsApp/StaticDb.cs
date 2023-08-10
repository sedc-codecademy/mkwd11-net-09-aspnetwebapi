using NotesAndTagsApp.Models;
using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp
{
    public static class StaticDb
    {
        public static List<Note> Notes = new List<Note>
        {
            new Note(){ Id = 1, Text = "Do Homework", Priority = Priority.High, Tags = new List<Tag>()
                {
                    new Tag(){ Name = "HomeWork", Color = "cyan"},
                    new Tag(){ Name = "SEDC", Color = "blue"}
                }
            },
            new Note(){ Id = 2, Text = "Drink more Water", Priority = Priority.Medium, Tags = new List<Tag>()
                {
                    new Tag(){ Name = "Healthy", Color = "orange"},
                    new Tag(){ Name = "Priority Medium", Color = "red"}
                }
            },
            new Note(){ Id = 3, Text = "Go to the gym", Priority = Priority.Low, Tags = new List<Tag>()
                {
                    new Tag(){ Name = "exercise", Color = "blue"},
                    new Tag(){ Name = "Priority Low", Color = "yellow"}
                }
            }
        };
    }
}
