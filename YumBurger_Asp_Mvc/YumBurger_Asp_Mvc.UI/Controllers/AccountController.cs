﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YumBurger_Asp_Mvc.UI.Models.ViewModels.UsersVM;

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
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Menu");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    //[HttpPost]
    //public async Task<IActionResult> Login(LoginVM model, string returnUrl)
    //{
    //    returnUrl ??= Url.Content("~/");

    //    if (ModelState.IsValid)
    //    {
    //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

    //        if (result.Succeeded)
    //        {
    //            return LocalRedirect(returnUrl);
    //        }

    //        ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
    //    }

    //    return View(model);
    //}

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Doğrudan istediğiniz sayfaya yönlendirin
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
