using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebFramework.Helpers;
using WebFramework.Models;

namespace WebFramework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(Employee employee)
        {
            if (ModelState.IsValid)
            {
                SqlHelper db = new SqlHelper();
                await db.InsertAsync(employee);
            }
            return View(employee);
        }

    }
}