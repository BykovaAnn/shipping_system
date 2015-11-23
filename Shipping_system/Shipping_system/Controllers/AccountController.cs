using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping_system.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace Shipping_system.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login logindata, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(logindata.Username, logindata.Password))
                {
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Sorry, smth wrong!");
            return View(logindata);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register registerdata, string role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(registerdata.Username, registerdata.Password);
                    Roles.AddUserToRole(registerdata.Username, role);
                    return RedirectToAction("Index", "Home");
                }
                catch (System.Web.Security.MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", "Sorry, smth exist!");
                    return View(registerdata);
                }
            }
            ModelState.AddModelError("", "Sorry, smth exist!");
            return View(registerdata);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}