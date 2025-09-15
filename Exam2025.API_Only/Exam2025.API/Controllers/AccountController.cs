using AutoMapper;
using AYMDating.Blazor.Data.DTO;
using Exam2025.API.DTO;
using Exam2025.API.Helpers;
using Exam2025.BLL.Interfaces;
using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Entities;
using Exam2025.DAL.Interfaces;
using Exam2025.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Exam2025.API.Controllers
{
    public class AccountController : BaseController
    {
        //private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        //private readonly IConfiguration configuration;

        public AccountController(/*IMapper mapper,*/ IUnitOfWork unitOfWork/*, IConfiguration configuration*/)
        {
            //this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            //this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("HelloWorld")]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ReturnValue_GenericDTO<UserDTO>>> Login([FromBody] LoginDTO login)
        {
            try
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(login.Email);
                if (user == null)
                    return Ok(new ReturnValue_GenericDTO<UserDTO>() {type= ReturnType.Failed.ToString(), message= "There is no user" });

                if (user.IsDeleted == true )
                    return Ok(new ReturnValue_GenericDTO<UserDTO>() {type = ReturnType.Failed.ToString(), message = "User is deleted" });

                var result = await unitOfWork.SignInManager.CheckPasswordSignInAsync(user, login.Password, false);
                if (!result.Succeeded)
                    return Ok(new ReturnValue_GenericDTO<UserDTO>() { type = ReturnType.Failed.ToString(), message = "Wrong Password" });


                if (result.Succeeded)
                {
                    await unitOfWork.SignInManager.SignInAsync(user, isPersistent: login.RememberMe);

                    var userDTO = new UserDTO
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        UserRole = await GetUserRole(user),
                        Token = await CreateTokenAndSaveIt(user),
                    };

                    return Ok(new ReturnValue_GenericDTO<UserDTO>() { type = ReturnType.Success.ToString(), message = "", data = userDTO });
                }

                return Ok(new ReturnValue_GenericDTO<UserDTO>() { type = ReturnType.Failed.ToString(), message = "General Error" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<ReturnValue_GenericDTO<RegisterDTO>>> Register(RegisterDTO model)
        {
            try
            {
                if (CheckEmailExistsAsync(model.Email).Result)
                {
                    return Ok(new ReturnValue_GenericDTO<RegisterDTO>() { type = ReturnType.Failed.ToString(), message = "This Email Address is already in use", data = new RegisterDTO() });
                }
                if (model.Password != model.ConfirmPassword)
                {
                    return Ok(new ReturnValue_GenericDTO<RegisterDTO>() { type = ReturnType.Failed.ToString(), message = "Password doesn't Match Confirm Password", data = new RegisterDTO() });
                }
                // Create new user
                var user = new AppUser
                {
                    UserName = model.FirstName + "." + model.LastName + "-" + unitOfWork.MySPECIALGUID.GetUniqueKey(6),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var AddNewUser = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                if (AddNewUser.Succeeded)
                {
                    var AddtoRole = await unitOfWork.UserManager.AddToRoleAsync(user, "User");
                    if (AddtoRole.Succeeded)
                    {
                        return Ok(new ReturnValue_GenericDTO<RegisterDTO>() { type = ReturnType.Success.ToString(), message = "", data = new RegisterDTO() });

                    }
                }
                return Ok(new ReturnValue_GenericDTO<RegisterDTO>() { type = ReturnType.Failed.ToString(), message = "General Error", data = new RegisterDTO() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Authorize]
        [HttpPost("GetCurrentUserData")]
        public async Task<ActionResult<ReturnValueDTO>> GetCurrentUserData()
        {
            try
            {
                var CurrentUser = await GetUserById(unitOfWork.AppSession.User1[0]);
                var userDTO = new UserDTO
                {
                    UserId = CurrentUser.Id,
                    Email = unitOfWork.AppSession.User1[2],
                    UserName = unitOfWork.AppSession.User1[1],
                    UserRole = unitOfWork.AppSession.User1[4],
                    Token = GetToken()
                };
                return Ok(new ReturnValueDTO(ReturnType.Success,"", userDTO));


            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }


        [HttpPost("Logout")]
        public async new Task<ActionResult<ReturnValueDTO>> SignOut()
        {
            try
            {
                await unitOfWork.SignInManager.SignOutAsync();
                return Ok(new ReturnValueDTO(ReturnType.Success, ""));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        #region Private Methods


        private async Task<bool> CheckEmailExistsAsync(string email)
           => await unitOfWork.UserManager.FindByEmailAsync(email) != null;

        private async Task<string> GetUserRole(AppUser user)
        {
            string UserRole = "";
            foreach (var item in unitOfWork.RoleManager.Roles.ToList())
            {
                if (await unitOfWork.UserManager.IsInRoleAsync(user, item.Name))
                {
                    UserRole = item.Name;
                    break;
                }
            }
            return UserRole;
        }

        
        private async Task<AppUser> GetUserById(string UserId)
            => await unitOfWork.UserManager.FindByIdAsync(UserId);

        private async Task<string> CreateTokenAndSaveIt(AppUser user)
        {
            var token = await unitOfWork.TokenService.CreateToken(user);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            return token;
        }

        private string GetToken()
        => !string.IsNullOrEmpty(unitOfWork.AppSession.AuthorizationToken) && unitOfWork.AppSession.AuthorizationToken != "Bearer" ? unitOfWork.AppSession.AuthorizationToken.Split(" ")[1] : string.Empty;



        #endregion
    }
}
