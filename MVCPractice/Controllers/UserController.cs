using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPractice.Models;
using MVCPractice.ViewModels;

namespace MVCPractice.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View(DataAccess.GetAllUsers());
        }

        public ActionResult Edit(int id)
        {
            User user = DataAccess.GetAllUsers().FirstOrDefault(x => x.Id == id);

            UserCompanyViewModel vm = new UserCompanyViewModel();
            vm.UserId = user.Id;
            vm.FirstName = user.FirstName;
            vm.LastName = user.LastName;
            vm.Username = user.Username;
            vm.CompanyId = user.Company.Id;

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(UserCompanyViewModel user)
        {
            DataAccess.UpdateUser(user);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new UserCompanyViewModel());
        }

        [HttpPost]
        public ActionResult Create(UserCompanyViewModel user)
        {
            DataAccess.CreateUser(user);
            return RedirectToAction("Index");
        }
    }
}