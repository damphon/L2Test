using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L2Test.Helpers;
using L2Test.Models;
using System.IO;

namespace L2Test.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (TempData["error"] == null)
                ViewBag.Error = "";
            else
                ViewBag.Error = TempData["error"].ToString();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(string formTechID)
        {
            return Redirect("~/Home/Test/" + formTechID);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Test()
        {
            TechModels Check = new TechModels();
            string URL = Request.Url.ToString();
            string TechID = Path.GetFileName(URL);

            if (Check.isValid(TechID))
                return View();

            TempData["error"] = "ERROR: Invalid ID. Please verify you entered the ID correctly. ID is only valid for 90 minutes, ask lead to create a new ID for you if your ID is not working.";
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Results()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            TestModels List = new TestModels();
            ViewBag.TestQuestions = List.EditListString();
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(string formQuestion, string formAnswer1, string formAnswer2, string formAnswer3, string formAnswer4, string formCatagory, string newCatagory)
        {
            TestDBHelper testDBHelp = new TestDBHelper();
            string catagory = "";
            //Handle ability to add catagorys to dropdown list.
            if (formCatagory == "1")
                catagory = newCatagory;
            else
                catagory = formCatagory;

            //Need to put in checks to prevent incorrect data from breaking DB
            testDBHelp.NewQuestion(formQuestion, formAnswer1, formAnswer2, formAnswer3, formAnswer4, catagory);
            return Redirect("~/Home/Edit");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditQuestion()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditQuestion(string formQuestion, string formAnswer1, string formAnswer2, string formAnswer3, string formAnswer4, string formCatagory, string uid)
        {
            TestDBHelper testDBHelp = new TestDBHelper();
            TestModels.Delete(uid);
            testDBHelp.NewQuestion(formQuestion, formAnswer1, formAnswer2, formAnswer3, formAnswer4, formCatagory);

            return Redirect("~/Home/Edit");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(string uid)
        {
            TestModels.Delete(uid);
            return Redirect("~/Home/Edit");
        }

        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }
    }
}