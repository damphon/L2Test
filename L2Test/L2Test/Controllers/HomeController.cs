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
            TestModels List = new TestModels();

            //ViewBag.TestQuestions = List.EditListString();

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
            //ViewBag.TestQuestions = List.EditListString();
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(string formQuestion, string formAnswer1, string formAnswer2, string formAnswer3, string formAnswer4, string formC1, string formC2, string formC3, string formC4, string formCatagory, string newCatagory)
        {
            TestDBHelper testDBHelp = new TestDBHelper();
            string catagory = "";
            bool C1 = false, C2 = false, C3 = false, C4 = false;

            if (formC1 == "1") C1 = true;
            if (formC2 == "1") C2 = true;
            if (formC3 == "1") C3 = true;
            if (formC4 == "1") C4 = true;

            //Handle ability to add catagorys to dropdown list.
            if (formCatagory == "1")
                catagory = newCatagory;
            else
                catagory = formCatagory;

            //Need to put in checks to prevent incorrect data from breaking DB
            testDBHelp.NewQuestion(formQuestion, formAnswer1, formAnswer2, formAnswer3, formAnswer4, C1, C2, C3, C4, catagory);
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
        public ActionResult EditQuestion(string formQuestion, string formAnswer1, string formAnswer2, string formAnswer3, string formAnswer4, string formC1, string formC2, string formC3, string formC4, string formCatagory, string newCatagory, string uid)
        {
            TestDBHelper testDBHelp = new TestDBHelper();
            TestModels.Delete(uid);
            string catagory = "";
            bool C1 = false, C2 = false, C3 = false, C4 = false;

            if (formC1 == "1") C1 = true;
            if (formC2 == "1") C2 = true;
            if (formC3 == "1") C3 = true;
            if (formC4 == "1") C4 = true;

            //Handle ability to add catagorys to dropdown list.
            if (formCatagory == "1")
                catagory = newCatagory;
            else
                catagory = formCatagory;

            testDBHelp.NewQuestion(formQuestion, formAnswer1, formAnswer2, formAnswer3, formAnswer4, C1, C2, C3, C4, catagory);
            return Redirect("~/Home/Edit");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(string uid)
        {
            TestModels.Delete(uid);
            return Redirect("~/Home/Edit");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Manage(string formTechName)
        {
            TechModels tech = new TechModels();
            tech.CreateNew(formTechName);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult TestResults()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult TestResults(IEnumerable<TestResultModel> jsonData)
        {
            TestResultModel.Submit(jsonData);
            return Json(new { success = true });
        }
    }
}