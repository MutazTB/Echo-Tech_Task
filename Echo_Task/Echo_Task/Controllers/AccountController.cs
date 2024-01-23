using Domain.DTOs;
using Domain.Security;
using Echo_TaskAPI.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repositories.Data;
using Services.IServices;
using System.Security.Claims;

namespace Echo_Task.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IUserSvc _userSvc;
        public AccountController(IUserSvc userSvc, UserManager<ApplicationUser> userManager)
        {
            _userSvc = userSvc;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _userSvc.Register(viewModel);
                return View("Login");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _userSvc.Login(viewModel);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _userSvc.Logout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }
        
    }
}
