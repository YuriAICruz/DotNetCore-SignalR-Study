using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetSelf()
        {
            if (User != null && _signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                return Json(new UserViewModel(user.UserName, user.Email));
            }
            
            return Json(new ErrorMessage("Not logged in"));
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

                return Json(new ErrorMessage(result.Errors.Select(x => x.Description.ToString())));
            }

            HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;


            return Json(new ErrorMessage(ViewData.ModelState.Values.Select(x => x.Errors)
                .Select(x => x.Select(y => y.ErrorMessage).Aggregate((i, j) => i + "; " + j))));
        }


        [HttpPost]
        [Route("SignIn")]
        public async Task<JsonResult> SignIn([FromBody] LoginModelView model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsPersistent, false);
                if (result.Succeeded)
                {
                    return Json(model);
                }

                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

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

                return Json(new ErrorMessage("Invalid User or Password"));
            }

            HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

            return Json(new ErrorMessage(ViewData.ModelState.Values.Select(x => x.Errors)
                .Select(x => x.Select(y => y.ErrorMessage).Aggregate((i, j) => i + "; " + j))));
        }


        [HttpPost]
        [Route("SignOut")]
        public async Task<JsonResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
                return Json("User logout");
            }

            HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            return Json(new ErrorMessage("User not logged in"));
        }
    }
}