using MVCPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPractice.ViewModels
{
    public class UserCompanyViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int CompanyId { get; set; }

        public IEnumerable<SelectListItem> CompanyData { get; set; }

        public UserCompanyViewModel()
        {
            CompanyData = DataAccess.GetAllCompanyData();
        }
    }
}