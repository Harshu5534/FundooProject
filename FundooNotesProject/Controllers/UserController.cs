using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooNotesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBl iuserBl;
        public UserController(IUserBl iuserBl)
        {
            this.iuserBl = iuserBl;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration registration)
        {
            try
            {
                var result = iuserBl.Registration(registration);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Registration Successfull",
                        Response = result
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Registration UnSuccessful",
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var result = iuserBl.Login(userLogin);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Login Successfully",
                        Response = result
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Login UnSuccessfully",
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("Forget")]
        public IActionResult Forget(string email)
        {
            try
            {
                var token = iuserBl.ForgetPassword(email);
                if (token == null)
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "mail not send",
                    });

                }
                return this.Ok(new
                {
                    success = true,
                    message = "Reset mail send successfully",
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost("Reset")]
       
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value;
                var result = iuserBl.ResetPassword(email, password, confirmPassword);
                if (result!=null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Your password has been changed sucessfully",
                        Response = result
                    }) ; 
                }
                else
                {
                    return this.BadRequest(new { 
                        Success = false, 
                        message = "Unable to reset password.Please try again" 
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
