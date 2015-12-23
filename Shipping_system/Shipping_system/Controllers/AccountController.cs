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
                    if (Roles.GetRolesForUser(logindata.Username)[0] == "user")
                    {
                        return RedirectToAction("Index", "CustomerCall");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Sorry, data are wrong!");
            return View(logindata);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register registerdata)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(registerdata.Username, registerdata.Password, new {
                        FirstName = registerdata.FirstName,
                        LastName = registerdata.LastName,
                        Email = registerdata.Email,
                        Phone = registerdata.Phone,
                        ContactPerson = registerdata.ContactPerson,
                    });
                    
                    Roles.AddUserToRole(registerdata.Username, "user");
                    return RedirectToAction("Index", "CustomerCall");
                }
                catch (System.Web.Security.MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", "Sorry, user already exist!");
                    return View(registerdata);
                }
            }
            ModelState.AddModelError("", "Sorry, already exist!");
            return View(registerdata);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login", "Account");
        }
    }
}