namespace UserApi.Services.Users;

public interface IUserProfileService
{
    Task<Context.User> GetUserAsync();
}