using Notes.Services.Models;

namespace Notes.Services.Service.External
{
    public interface IPlaceholderService
    {
        Task<IEnumerable<UserModel>> GetUsers();

        Task<PostModel> CreatePost(CreatePostModel createPostModel);
    }
}