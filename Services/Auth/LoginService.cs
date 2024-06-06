using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Context;

namespace UserApi.Services.Auth;

public class LoginService : ILoginService
{
    private readonly UserContext _userContext;

    public LoginService(UserContext userContext)
    {
        _userContext = userContext;
    }
    public async Task<string> ValidateUserAsync(string username, string password)
    {
        var user = await _userContext.User.FirstOrDefaultAsync(x => 
                    x.UserName == username && 
                    x.Password == password);

        // no encontré el usuario
        if (user == null)
        {
            return null;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        // encontré el usuario por ende genero jwt token
        var tokenDescription = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("asdasdasdasdasdsadasdXCXCweqeqweqweewqe")
                ),
                SecurityAlgorithms.HmacSha256Signature)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescription);

        return token;
    }
}
