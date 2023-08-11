using SEDC.NotesAndTagsApp2.Models;
using SEDC.NotesAndTagsApp2.Models.Enums;

namespace SEDC.NotesAndTagsApp2
{
    public static class StaticDb
    {
        public static List<User> Users = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Bojan",
                LastName = "Damcevski",
                Username = "BokiBateto",
                Password = "Batebate"
            },
            new User()
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Bobsky",
                Username = "BobskyBrat",
                Password = "12345"
            }
        };

        public static List<Tag> Tags = new List<Tag>()
        {
            new Tag(){ Id=1, Name="Homework", Color = "cyan" },
            new Tag(){ Id=2, Name="SEDC", Color = "blue" },
            new Tag(){ Id=3, Name="Healthy", Color = "orange" },
            new Tag(){ Id=4, Name="water", Color = "blue" },
            new Tag(){ Id=5, Name="exercise", Color = "blue" },
            new Tag(){ Id=6, Name="fit", Color = "yellow" }
        };

        public static List<Note> Notes = new List<Note>()
        {
            new Note(){
                Id=1,
                Text="Do homework",
                Priority = PriorityEnum.Low,
                Tags = new List<Tag>() { Tags[0], Tags[1] },
                User = Users[0],
                UserId = Users[0].Id
            },
            new Note(){
                Id=2,
                Text="Drink more water",
                Priority = PriorityEnum.High,
                Tags = new List<Tag>() { Tags[2], Tags[3] },
                User = Users[1],
                UserId = Users[1].Id
            },
            new Note(){
                Id=3,
                Text="Go to the gym",
                Priority = PriorityEnum.Medium,
                Tags = new List<Tag>() { Tags[4], Tags[5] } ,
                User = Users[0],
                UserId = Users[0].Id
            }
        };
    }
}
