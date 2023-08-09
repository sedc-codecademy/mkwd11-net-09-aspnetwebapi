using SEDC.NotesAndTagsApp.Models;
using SEDC.NotesAndTagsApp.Models.Enums;

namespace SEDC.NotesAndTagsApp
{
    public static class StaticDb
    {
        public static List<Note> Notes = new List<Note>()
        {
            new Note()
            {
                Text = "Learn MVC",
                Priority = Priority.High,
                Tags = new List<Tag>()
                {
                    new Tag()
                    {
                        Color = "red",
                        Name = "Coding"
                    },
                    new Tag()
                    {
                        Color = "blue",
                        Name = "SEDC"
                    }
                }
            },
            new Note()
            {
                Text = "Learn API",
                Priority = Priority.Medium,
                Tags = new List<Tag>()
                {
                    new Tag()
                    {
                        Color = "red",
                        Name = "Coding"
                    },
                    new Tag()
                    {
                        Color = "blue",
                        Name = "SEDC"
                    }
                }
            },
            new Note()
            {
                Text = "Learn JS",
                Priority = Priority.Low,
                Tags = new List<Tag>()
                {
                    new Tag()
                    {
                        Color = "yellow",
                        Name = "Design"
                    },
                    new Tag()
                    {
                        Color = "blue",
                        Name = "SEDC"
                    }
                }
            }
        };
    }
}
