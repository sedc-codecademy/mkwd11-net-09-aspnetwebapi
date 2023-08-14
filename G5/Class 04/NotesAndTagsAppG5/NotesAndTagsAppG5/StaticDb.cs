using NotesAndTagsAppG5.Models;

namespace NotesAndTagsAppG5
{
    public static class StaticDb
    {

        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Tijana",
                LastName = "Stojanovska",
                Username = "t.stojanovska",
                Password = "123"
            },
            new User
            {
                Id = 2,
                FirstName = "Aleksandar",
                LastName = "Ivanovski",
                Username = "a.ivanovski",
                Password = "456"
            }

        };

        public static List<Tag> Tags = new List<Tag>
        {
            new Tag(){ Id = 1, Name = "Homework", Color= "red"},
            new Tag(){ Id = 2, Name = "SEDC", Color= "blue"},
            new Tag(){ Id = 3, Name = "Health", Color= "green"},
            new Tag(){ Id = 4, Name = "Exercise", Color= "white"},
            new Tag(){ Id = 5, Name = "Fit", Color= "yellow"},
        };

        public static List<Note> Notes = new List<Note>
        {
            new Note()
            {   Id = 1,
                Text ="Do the homework",
                Priority = Models.Enums.Priority.Medium,
                Tags = new List<Tag>()
                {
                   new Tag(){ Id = 1, Name = "Homework", Color= "red"},
                   new Tag(){ Id = 2, Name = "SEDC", Color= "blue"},
                },
                User = Users.First(),
                UserId = Users.First().Id,
            },
            new Note()
            {   Id =2,
                Text ="Drink more water",
                Priority = Models.Enums.Priority.High,
                Tags = new List<Tag>()
                {
                    new Tag(){ Id = 3, Name = "Health", Color= "green"},
                },
                User = Users.First(),
                UserId = Users.First().Id,
            },
             new Note()
            {   Id = 3,
                Text ="Go to the gym!",
                Priority = Models.Enums.Priority.Low,
                Tags = new List<Tag>()
                {
                   new Tag(){ Id = 3, Name = "Health", Color= "green"},
                   new Tag(){ Id = 4, Name = "Exercise", Color= "white"},
                   new Tag(){ Id = 5, Name = "Fit", Color= "yellow"},
                },
                User = Users.Last(),
                UserId = Users.Last().Id,
            },
        };
    }
}
