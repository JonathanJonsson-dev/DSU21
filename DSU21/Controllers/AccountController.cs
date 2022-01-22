using DSU21.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, 
                                SignInManager<IdentityUser> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region Login / logout
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (returnUrl != "")
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Felaktig inloggning");
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Privacy", "Home");
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid) // Kollar så att email och lösenord är ok
            {
                var user = new IdentityUser // Skapar användare
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password); //Skickar användare till databas
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home"); // Om det gick så renderas index sidan. 
                }
                // error
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                ModelState.AddModelError(string.Empty, "Inloggning har misslyckats");
            }
            return View(model);
        }
        #endregion

        #region Roller
        public async Task<IActionResult> CreateRoles()
        {
            var role = new IdentityRole("Deckhand");
            var roleExist = await _roleManager.RoleExistsAsync(role.Name);

            if (!roleExist)
            {
                var result = await _roleManager.CreateAsync(role);
            }
            return View();
        }

        public async Task<IActionResult> Roles()
        {
            var user = await _userManager.GetUserAsync(User); // Hämtar en user från cookie via userManager
            var result = await _userManager.AddToRoleAsync(user, "Deckhand"); // Tilldelar roll till användare
            await _signInManager.SignInAsync(user, isPersistent: false); // Loggar in användere
            return RedirectToAction("Index", "Capt"); // Renderar sidan index som ligger i capt-mappen
        }
        #endregion
    }
}
