﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Errors;
using WebBookShopAPI.Data.Models.Identity;
using WebBookShopAPI.Data.Repositories;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Role = User.FindFirstValue(ClaimTypes.Role)
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
                Email = user.Email,
                Token = token,
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Role = roleString
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                DateOfBirth = registerDto.DateOfBirth,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            var roles = await _userManager.GetRolesAsync(user);
            var roleString = string.Join(",", roles);


            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Role = roleString
            };
        }
    }
}