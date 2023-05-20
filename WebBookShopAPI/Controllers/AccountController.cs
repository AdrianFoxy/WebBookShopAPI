using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Errors;
using WebBookShopAPI.Data.Helpers;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Models.Identity;
using WebBookShopAPI.Data.Repositories;
using WebBookShopAPI.Data.Specifications;
using WebBookShopAPI.Extensions;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IUserRepository userRepository, IMapper mapper) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userRepo = userRepository;
            _mapper = mapper;
        }


        [HttpGet("get-all-users")]
        public async Task<ActionResult<UserListDto>> GetAllUserForAdmin([FromQuery] PaginationParams pagParams)
        {
            var response = await _userRepo.GetAllUsersAsync();
            var totalItems = response.Count();

            var data = response
                .Skip((pagParams.PageIndex - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToList();

            var userList = _mapper.Map<IReadOnlyList<UserListDto>>(data);

            return Ok(new Pagination<UserListDto>(pagParams.PageIndex, pagParams.PageSize, totalItems, userList));
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Role = User.RetrieveRoleFromPrincipal(),
                PhoneNumber = user.PhoneNumber,
                UserGenderCode = user.UserGenderCode
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,
                false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var token = await _tokenService.CreateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleString = string.Join(",", roles);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Role = roleString,
                PhoneNumber = user.PhoneNumber,
                UserGenderCode = user.UserGenderCode

            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userEmail != null)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                { Errors = new[] { "Email address is in use" } });
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                DateOfBirth = registerDto.DateOfBirth,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserGenderCode = registerDto.UserGenderCode
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            var roles = await _userManager.GetRolesAsync(user);
            var roleString = string.Join(",", roles);


            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Role = roleString,
                PhoneNumber = user.PhoneNumber,
                UserGenderCode = user.UserGenderCode
            };
        }
    }
}
