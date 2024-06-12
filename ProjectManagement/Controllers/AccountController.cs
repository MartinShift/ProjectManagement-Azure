using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Business.Models;
using ProjectManagement.Business.Validation;
using ProjectManagement.Data;
using ProjectManagement.Data.Repositories.Interfaces;
using System.Data;
using System.Security.Claims;

namespace ProjectManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RegisterDtoValidator _registerDtoValidator;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _registerDtoValidator = new RegisterDtoValidator(unitOfWork);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task SignInClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>();
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var userIdentity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            model.Login = model.Login?.Trim();

            var validationResult = _registerDtoValidator.Validate(model);
            if(!validationResult.IsValid)
            {
                return BadRequest(new { Error = validationResult.Errors.First() });
            }

            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Login,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Success!" });
            }


            return BadRequest(new { Error = string.Join(",", result.Errors.Select(e => e.Description).ToList()) });
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Projects");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (string.IsNullOrEmpty(model.LoginOrEmail))
            {
                return BadRequest(new { Message = "", Error = "Login is required" } );
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(new { Message = "", Error = "Password is required" });
            }

            var existingEmail = await _userManager.FindByEmailAsync(model.LoginOrEmail);
            var existingLogin = await _userManager.FindByNameAsync(model.LoginOrEmail);
            if (existingLogin == null && existingEmail == null)
            {
                return BadRequest(new { Message = "", Error = "No Such Login Exists" });
            }
            if (existingEmail != null) { model.LoginOrEmail = existingEmail.UserName; }
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.LoginOrEmail, model.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                    return Ok(new { Message = "Success!" });
                }
            var username = existingLogin ?? existingEmail;
            await SignInClaims(username);

            return Ok(new { Message = "Success!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
