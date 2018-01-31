using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhatDo.Models;

namespace WhatDo.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var currentUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string enjoyerName = "Enjoyer";
            string adminName = "Admin";                     
            string enjoyerRoleId = (from role in db.Roles where enjoyerName == role.Name select role.Id).First();
            string adminId = (from role in db.Roles where adminName == role.Name select role.Id).First();
            if (currentUser.Roles.First().RoleId == enjoyerRoleId )
            {
               return RedirectToAction("Index", "Enjoyer");
            }
            if (currentUser.Roles.First().RoleId == adminId)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}