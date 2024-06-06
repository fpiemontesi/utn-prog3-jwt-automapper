using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos;
using UserApi.Services.User;
using UserApi.Services.Users;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;

        public UserController(IUserProfileService userProfileService,
            IMapper mapper)
        {
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "regular,admin")]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            var user = await _userProfileService.GetUserAsync();
            var dto = _mapper.Map<UserDto>(user);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }
    }
}
