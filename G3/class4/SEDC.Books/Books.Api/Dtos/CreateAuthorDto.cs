namespace Books.Api.Dtos
{
    public class CreateAuthorDto
    {
        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}
