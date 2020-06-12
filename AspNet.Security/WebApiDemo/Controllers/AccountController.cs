using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.ViewModels;

namespace WebApiDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                await signInManager.SignOutAsync();
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Students", "Students");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Students", "Students");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var identity = new IdentityUser(createUserViewModel.Email) { Email = createUserViewModel.Email };
                var result = await this.userManager.CreateAsync(identity, createUserViewModel.Password);
                if (result.Succeeded)
                {
                    var claimResult = await userManager.AddClaimAsync(identity, new Claim("Country", "Ukraine"));

                    return RedirectToAction("Users");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid create User.");
                    return View(createUserViewModel);
                }
            }

            return View(createUserViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> CreateUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> CreateRole()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await this.roleManager.CreateAsync(new IdentityRole(createRoleViewModel.Role));
                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid create role.");
                    return View(createRoleViewModel);
                }
            }

            return View(createRoleViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Users()
        {

            UsersViewModel model = new UsersViewModel() { UserNames = this.userManager.Users.Select(p => p.Email).ToList() };
            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> AssignUserRole(string userName)
        {
            AssignUserRoleViewModel model = new AssignUserRoleViewModel();
            var user = this.userManager.Users.SingleOrDefault(p => p.Email == userName);
            if (user == null)
            {
                return this.NotFound();
            }

            foreach (var role in this.roleManager.Roles.ToList())
            {
                model.UserRoles.Add(new UserRoleViewModel()
                {
                    RoleName = role.Name,
                    IsAssigned = await this.userManager.IsInRoleAsync(user, role.Name)
                });
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AssignUserRole(AssignUserRoleViewModel model)
        {
            var user = this.userManager.Users.SingleOrDefault(p => p.Email == model.UserName);
            foreach (var userRoleViewModel in model.UserRoles)
            {
                if (await this.userManager.IsInRoleAsync(user, userRoleViewModel.RoleName))
                {
                    await this.userManager.RemoveFromRoleAsync(user, userRoleViewModel.RoleName);
                }

                if (userRoleViewModel.IsAssigned)
                {
                    var result = await this.userManager.AddToRoleAsync(user, userRoleViewModel.RoleName);
                }
            }

            return RedirectToAction("Users");
        }

    }
}