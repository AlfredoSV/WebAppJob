﻿using Framework.Security2023;
using Framework.Security2023.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection;
using WebAppJob.Models;
using Framework.Security2023.Entities; 
using System.Resources;
using Humanizer.Localisation;
using static Framework.Security2023.Entities.Login;

namespace WebAppJob.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceLogin _serviceLogin;
        private readonly IServiceUser _serviceUser;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IServiceUser serviceUser, IServiceLogin serviceLogin)
        {
            _serviceUser = serviceUser;
            this._serviceLogin = serviceLogin;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into LoginController");
        }

        public IActionResult Index()
        {
            //string culture = CultureInfo.CurrentCulture.TextInfo.CultureName ;
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(string userName)
        {
            try
            {
                if (_serviceUser.UserExist(userName))
                    return RedirectToAction("LoginValidation", "Login", new { userName });
               
                ModelState.AddModelError("UserName", "The user was not found");

            }
            catch (Exception e)
            {
                ModelState.AddModelError("UserName", "An error occurred while searching for the user");

            }

            return View("Index");

        }

        [HttpGet]
        public IActionResult LoginValidation(string userName)
        {

            UserModel user = new UserModel();
            user.UserName = userName;
            return View(user);
        }

        [HttpPost]
        public IActionResult LoginValidation(UserModel user)
        {
            try
            {

                Login login = Login.Create(user.UserName, user.Password);
                Login userLogin = _serviceLogin.Login(login);
                if (userLogin.StatusLog == StatusLogin.Ok)
                {
                    HttpContext.Session.SetString("User", userLogin.User.Id.ToString());
                    return RedirectToAction("Index", "Home", new { user.UserName });
                }
                    

            }
            catch (Exception)
            {

                ModelState.AddModelError("Password", "An error occurred while searching for the information of user");
            }

            return View(user);
          
        }
    }
}
