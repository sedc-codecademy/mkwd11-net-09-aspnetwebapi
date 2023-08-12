using SEDC.NoteApp.Models;
using SEDC.NoteApp.Models.Enums;

namespace SEDC.NoteApp
{
    public static class StaticDb
    {
        public static int NoteId = 3;
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "tanja",
                LastName = "Stojanovska",
                Username = "tstojanovska",
                Password = "123"
            },
            new User
            {
                Id = 2,
                FirstName = "Danilo",
                LastName = "Borozan",
                Username = "dborozan",
                Password = "123"
            }
        };

        public static List<Tag> Tags = new List<Tag>
        {
             new Tag(){ Id = 1, Name = "HomeWork", Color = "cyan"},
             new Tag(){ Id = 2, Name = "SEDC", Color = "blue"},
             new Tag(){ Id = 3,Name = "Healthy", Color = "orange"},
             new Tag(){ Id = 4, Name = "water", Color = "blue"},
             new Tag(){ Id = 5, Name = "exercise", Color = "blue"},
             new Tag(){ Id = 6, Name = "Fit", Color = "yellow"}

        };

        public static List<Note> Notes = new List<Note>()
        {
            new Note(){ Id = 1, Text = "Do Homework", Priority = PriorityEnum.Low, Tags = new List<Tag>()
                {
                    new Tag(){ Id = 1, Name = "HomeWork", Color = "cyan"},
                    new Tag(){ Id = 2, Name = "SEDC", Color = "blue"}
                },
                User = Users.First(),
                UserId = Users.First().Id
            },
            new Note(){ Id = 2, Text = "Drink more Water", Priority = PriorityEnum.High, Tags = new List<Tag>()
                {
                    new Tag(){ Id = 3,Name = "Healthy", Color = "orange"},
                    new Tag(){ Id = 4, Name = "water", Color = "blue"}
                },
                 User = Users.First(),
                 UserId = Users.First().Id
            },
            new Note(){ Id = 3, Text = "Go to the gym", Priority = PriorityEnum.Medium, Tags = new List<Tag>()
                {
                    new Tag(){ Id = 5, Name = "exercise", Color = "blue"},
                    new Tag(){ Id = 6, Name = "Fit", Color = "yellow"}
                },
                 User = Users.Last(),
                 UserId = Users.Last().Id
            }
        };
    }
}
