using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrototypeWebsite.Models;

namespace PrototypeWebsite.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Fruits()
        {
            var vm = new FruitsPageViewModel();
            vm.JSModuleName = "page-fruits";
            return View(vm);
        }

        public ActionResult Trees()
        {
            var vm = new TreesPageViewModel();
            vm.JSModuleName = "page-trees";
            return View(vm);
        }
    }
}
