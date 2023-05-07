﻿using Courses.DAL.Repositories;
using Courses.Domain.Entity;
using Courses.Domain.ViewModules.Account;
using Courses.Service.Implementations;
using Courses.Service.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Courses.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ICompletedPartService _completedPartService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
                                ICompletedPartService completedPartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _completedPartService = completedPartService;
        }


        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                User user = new User { Email = model.Email, UserName = model.Email, Name = model.Name};
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                //ModelState.AddModelError("",response.Description);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    //SortedSet<int> idPCompletedParts = new SortedSet<int>();
                    //var listofCompletedParts = await _completedPartService.GetCompletedPartBuIdUser(_userManager.GetUserId(HttpContext.User));
                    //if (listofCompletedParts.StatusCode == Domain.Enum.StatusCode.OK)
                    //{
                    //    foreach (var el in listofCompletedParts.Data)
                    //    {
                    //        idPCompletedParts.Add(el.PracticalPartId);
                    //    }
                    //    HttpContext.Session.Set("completedParts", JsonSerializer.SerializeToUtf8Bytes(idPCompletedParts));
                    //}
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
