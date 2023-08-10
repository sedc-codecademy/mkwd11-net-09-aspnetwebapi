using SEDC.NotesAndTagsApp.Models;
using SEDC.NotesAndTagsApp.Models.Enums;

namespace SEDC.NotesAndTagsApp
{
    public static class StaticDb
    {
        public static List<Note> Notes = new List<Note>()
        {
            new Note 
            {
                Text = "Do Homework",
                Priority = Priority.High,
                Tags = new List<Tag>() 
                {
                    new Tag 
                    {
                        Name = "Homework",
                        Color = "cyan"
                    },
                    new Tag 
                    {
                        Name = "SEDC",
                        Color = "blue"
                    }
                }
            },
            new Note
            {
                Text = "Drink more water",
                Priority = Priority.Medium,
                Tags = new List<Tag>()
                {
                    new Tag
                    {
                        Name = "Healthy",
                        Color = "red"
                    },
                    new Tag
                    {
                        Name = "High Priority",
                        Color = "yellow"
                    }
                }
            },
            new Note
            {
                Text = "Exercise",
                Priority = Priority.High,
                Tags = new List<Tag>()
                {
                    new Tag
                    {
                        Name = "Exercise",
                        Color = "orange"
                    },
                    new Tag
                    {
                        Name = "High Priority",
                        Color = "blue"
                    }
                }
            },
        };
    }
}
