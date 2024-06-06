using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserApi.Context;
using UserApi.Services.Users;

namespace UserApi.Services.User;

public class UserProfileService : IUserProfileService
{
    private readonly UserContext _userContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProfileService(UserContext userContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _userContext = userContext;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<Context.User> GetUserAsync()
    {
        if (_httpContextAccessor.HttpContext.User == null)
        {
            return null;
        }

        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        var userId = Guid.Parse(userIdClaim.Value);

        var user = await _userContext.User.FirstOrDefaultAsync(x => x.Id == userId);

        return user;
    }
}
