using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebServerStudy.Controllers.ModelView;
using WebServerStudy.Models;

namespace WebServerStudy.Controllers
{
    [Route("Auth")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        [Route("SignUp")]
        public async Task<JsonResult> Register([FromBody] RegisterModelView model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: model.IsPersistent);
                    
                    return Json(user);
                }

                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                
                return Json(new ErrorMessage(result.Errors));
            }

            HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            
            return Json(new ErrorMessage("Invalid Form"));
        }
        

        [HttpPost]
        [Route("SignIn")]
        public async Task<JsonResult> SignIn([FromBody] RegisterModelView model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsPersistent, false);
                if (result.Succeeded)
                {
                    
                }
                
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                
                if (result.IsLockedOut)
                {
                    return Json(new ErrorMessage("IsLockedOut"));
                }
                
                if (result.IsNotAllowed)
                {
                    return Json(new ErrorMessage("IsNotAllowed"));
                }
                
                if (result.RequiresTwoFactor)
                {
                    return Json(new ErrorMessage("RequiresTwoFactor"));
                }
            }

            HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            
            return Json(new ErrorMessage("Invalid Form"));
        }
    }
}