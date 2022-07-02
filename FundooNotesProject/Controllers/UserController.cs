using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var result=iuserBl.Registration(registration);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message="Registration Successfull",
                        Response=result
                    });

                }
                else{
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Registration UnSuccessful",
                    });
                }
            }
            catch (System.Exception)
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
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
