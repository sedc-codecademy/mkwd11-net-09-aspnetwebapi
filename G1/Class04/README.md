# Building a Note API 📒

## Models

### Note

```csharp
public class NoteDto
{
 [Key]
 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 public int Id { get; set; }
 public string Text { get; set; }
 public string Color { get; set; }
 public int Tag { get; set; }
 public int UserId { get; set; }
 public virtual UserDto User { get; set; }
}

public class NoteModel
{
 public int Id { get; set; }
 public string Text { get; set; }
 public string Color { get; set; }
 public TagType Tag { get; set; }
 public int UserId { get; set; }
}
public enum TagType
{
 Work = 1,
 Education = 2,
 Home = 3,
 Misc = 4,
 Other = 5
}
```

### User

```csharp
public class UserDto
{
 [Key]
 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 public int Id { get; set; }
 public string Username { get; set; }
 public string FirstName { get; set; }
 public string LastName { get; set; }
 public string Password { get; set; }
 public virtual ICollection<NoteDto> NoteList { get; set; }
}

public class UserModel
{
 public int Id { get; set; }
 public string Username { get; set; }
 public string FirstName { get; set; }
 public string LastName { get; set; }
 public string FullName => $"{FirstName} {LastName}";
 public List<NoteModel> NoteList { get; set; }

 public UserModel()
 {
  NoteList = new List<NoteModel>();
 }
}
```
