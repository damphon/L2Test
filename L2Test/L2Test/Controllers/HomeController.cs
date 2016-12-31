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
        public ActionResult Edit(string formQuestion, string formAnswer1, string formAnswer2, string formAnswer3, string formAnswer4, string formAnswer5, string formAnswer6, string formAnswer7, string formAnswer8, string formC1, string formC2, string formC3, string formC4, string formC5, string formC6, string formC7, string formC8, string formCategory, string newCategory)
        {
            TestDBHelper testDBHelp = new TestDBHelper();
            string Category = "";
            bool C1 = false, C2 = false, C3 = false, C4 = false, C5 = false, C6 = false, C7 = false, C8 = false;

            if (formC1 == "1") C1 = true;
            if (formC2 == "1") C2 = true;
            if (formC3 == "1") C3 = true;
            if (formC4 == "1") C4 = true;
            if (formC5 == "1") C5 = true;
            if (formC6 == "1") C6 = true;
            if (formC7 == "1") C7 = true;
            if (formC8 == "1") C8 = true;

            //Handle ability to add Categorys to dropdown list.
            if (formCategory == "1")
                Category = newCategory;
            else
                Category = formCategory;

            //Need to put in checks to prevent incorrect data from breaking DB
            testDBHelp.NewQuestion(formQuestion, formAnswer1, formAnswer2, formAnswer3, formAnswer4, formAnswer5, formAnswer6, formAnswer7, formAnswer8, C1, C2, C3, C4, C5, C6, C7, C8, Category);
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
        public ActionResult EditQuestion(string formQuestion, string formAnswer1, string formAnswer2, string formAnswer3, string formAnswer4, string formAnswer5, string formAnswer6, string formAnswer7, string formAnswer8, string formC1, string formC2, string formC3, string formC4, string formC5, string formC6, string formC7, string formC8, string formCategory, string newCategory, string uid)
        {
            TestDBHelper testDBHelp = new TestDBHelper();
            TestModels.Delete(uid);
            string Category = "";
            bool C1 = false, C2 = false, C3 = false, C4 = false, C5 = false, C6 = false, C7 = false, C8 = false;

            if (formC1 == "1") C1 = true;
            if (formC2 == "1") C2 = true;
            if (formC3 == "1") C3 = true;
            if (formC4 == "1") C4 = true;
            if (formC5 == "1") C5 = true;
            if (formC6 == "1") C6 = true;
            if (formC7 == "1") C7 = true;
            if (formC8 == "1") C8 = true;

            //Handle ability to add Categorys to dropdown list.
            if (formCategory == "1")
                Category = newCategory;
            else
                Category = formCategory;

            testDBHelp.NewQuestion(formQuestion, formAnswer1, formAnswer2, formAnswer3, formAnswer4, formAnswer5, formAnswer6, formAnswer7, formAnswer8, C1, C2, C3, C4, C5, C6, C7, C8, Category);
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

        [HttpPost]
        [Authorize]
        public ActionResult ManageDelete(string uid)
        {
            UserMgmt.Delete(uid);
            return Redirect("~/Home/Manage");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ManageEdit(string uid, string newPassword)
        {
            UserMgmt.PaswordUpdate(uid, newPassword);
            return Redirect("~/Home/Manage");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult TestResults(IEnumerable<TestResultModel> jsonData)
        {
            return Content(TestResultModel.Submit(jsonData));
        }

        [HttpPost]
        [AllowAnonymous]
        public void TestArchive(string html, string tech)
        {
            TestResultModel.Archive(html, tech);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ReportCards()
        {
            ReportCardHelper helper = new ReportCardHelper();
            ViewBag.GradedResults = helper.GetGradedReport();
            ViewBag.ArchiveResults = helper.GetArchiveReport();
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ReportCards(string formTechName)
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public FileContentResult DownloadCSV()
        {
            TestModels help = new TestModels();
            string csv = help.ExportAsCSV();
            string fileName = "L2TestDB_" + DateTime.Now.ToString("dd-MM-yy") + ".csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", fileName);
        }

        //[HttpPost]
        //[Authorize]
        //public FileContentResult UploadCSV()
        //{
        //    //Upload script
        //}
    }
}