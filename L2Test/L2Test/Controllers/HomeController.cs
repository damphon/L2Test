using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using L2Test.Helpers;
using L2Test.Models;
using System.IO;
using System.Data;
using Ionic.Zip;
using System.Net.Http;

namespace L2Test.Controllers
{
    public class HomeController : Controller
    {
        //AllowAnonymous means that users do not need to authenticate to use/see this page
        //Authorize means that users have to be logged in to view the page.

        [HttpGet]
        [AllowAnonymous]//Cannot require login if no logins have been created yet
        public ActionResult Install()
        {
            UserMgmt check = new UserMgmt();
            if (check.isInstalled()) return RedirectToAction("Home"); //Verifies that there is no management accounts then returns the Install page. This is to make sure that no one can go to the install page to hack the system once the page is set up.
            else {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]//Cannot require login if no logins have been created yet
        public ActionResult ConfigDB(string dbPath, string dbName, string dbUser, string dbPassword)
        {
            Helpers.Install setup = new Helpers.Install();
            TempData["DBCheckResult"] = setup.RunInstall(dbPath, dbName, dbUser, dbPassword);
            return RedirectToAction("Install");
        }

        [HttpPost]
        [AllowAnonymous]//Cannot require login if no logins have been created yet
        public ActionResult CheckDB()
        {
            Helpers.Install setup = new Helpers.Install();
            var urlHelper = new UrlHelper(HttpContext.Request.RequestContext);
            string URL = urlHelper.Action("Install2", "Home");
            TempData["DBCheckResult"] = setup.TestDB(URL);
            return RedirectToAction("Install");
        }
        [HttpGet]

        [AllowAnonymous]//Cannot require login if no logins have been created yet
        public ActionResult Install2()
        {
            UserMgmt check = new UserMgmt();
            if (check.isInstalled()) return RedirectToAction("Home"); //Verifies that there is no management accounts then returns the Install page. This is to make sure that no one can go to the install page to hack the system once the page is set up.
            else return View();
        }

        [HttpGet]
        [AllowAnonymous] //Redirects to Home if there is a Manager/Lead login
        public ActionResult Index() 
        {
            UserMgmt check = new UserMgmt();
            if (check.isInstalled()) return RedirectToAction("Home"); //If no management accounts exist this takes you to the install page.
            else return RedirectToAction("Install");
        }

        [HttpGet]
        [AllowAnonymous]//Techs need to access this page to start taking the test
        public ActionResult Home()
        {
            ViewBag.L2Requirements = Config.GetString("L2Requirements");
            ViewBag.TestInstructions = Config.GetString("TestInstructions");

            if (TempData["error"] == null)
                ViewBag.Error = "";
            else
                ViewBag.Error = TempData["error"].ToString();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]//Used to authenticate the techs temp ID
        public ActionResult Home(string formTechID)
        {
            return Redirect("~/Home/Test/" + formTechID);
        }

        [HttpGet]
        [AllowAnonymous]//Techs need access to this page
        public ActionResult Test()
        {
            TechModels Check = new TechModels();
            TestQuestions List = new TestQuestions();

            ViewBag.TimeToTakeTest = Config.GetInt("TimeToTakeTest");
            string URL = Request.Url.ToString();
            string TechID = Path.GetFileName(URL);

            if (Check.isValid(TechID))
                return View();

            TempData["error"] = "ERROR: Invalid ID. Please verify you entered the ID correctly. ID is only valid for 90 minutes, ask lead to create a new ID for you if your ID is not working.";
            return RedirectToAction("Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string Error = "")
        {
            ViewBag.Err = Error;
            TestQuestions List = new TestQuestions();
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [Authorize]
        public ActionResult Edit(TestEditModel jsonData)
        {
            TestEditModel help = new TestEditModel();
            help.NewQuestion(jsonData);
            return Json(new { success = true });
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditQuestion()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [Authorize]
        public ActionResult EditQuestion(TestEditModel jsonData)
        {
            TestEditModel Help = new TestEditModel();
            Help.EditQuestion(jsonData);
            return Json(new { success = true});
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(string uid)
        {
            EditTest.Delete(uid);
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
        [AllowAnonymous] //Used to submit test
        public ActionResult TestResults(IEnumerable<TestResultModel> jsonData)
        {
            GradeTest help = new GradeTest();
            return Content(help.Submit(jsonData));
        }

        [HttpPost]
        [AllowAnonymous] //Used to save snapshot of HTML at time the submit button was pressed.
        public void TestArchive(string html, string tech)
        {
            GradeTest help = new GradeTest();
            help.Archive(html, tech);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ReportCards()
        {
            ReportCardHelper helper = new ReportCardHelper();
            ViewBag.GradedResults = helper.GetGradedReport(Server.MapPath(@"~\Views\Tests"));
            ViewBag.ArchiveResults = helper.GetGradedReport(Server.MapPath(@"~\Views\Tests\Ungraded"));
            return View();
        }

        [HttpGet]
        [Authorize]
        public FilePathResult Backup()
        {
            string fileName = "L2Test_Backup_" + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";

            CSVHelper help = new CSVHelper();
            string csv = help.ExportAsCSV();
            if (System.IO.File.Exists(Server.MapPath(@"~\Views\Tests\L2TestDB.csv"))) { System.IO.File.Delete(Server.MapPath(@"~\Views\Tests\L2TestDB.csv")); }
            System.IO.File.WriteAllText(Server.MapPath(@"~\Views\Tests\L2TestDB.csv"), csv);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(Server.MapPath(@"~\Views\Tests"));
                zip.Save(Server.MapPath(@"~\App_Data\Backup.zip"));
            }
            return new FilePathResult(Server.MapPath(@"~\App_Data\Backup.zip"), "application/zip");
        }

        [HttpGet]
        [Authorize]
        public FileContentResult DownloadCSV()
        {
            CSVHelper help = new CSVHelper();
            string csv = help.ExportAsCSV();
            string fileName = "L2TestDB_" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", fileName);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadCSV(HttpPostedFileBase FileUpload, int CSVAppend)
        {
            DataTable dt = new DataTable();
            string ErrMessage = "";

            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                string fileName = Path.GetFileName(FileUpload.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                try
                {
                    FileUpload.SaveAs(path);
                    //Proccess CSV file and save result to DataTable.
                    dt = CSVHelper.ProcessCSV(path);
                }
                catch (Exception ex) {ErrMessage = ex.Message;}
            }
            else {ErrMessage = "Invalid file, or no file selected";}

            //If "replace test" is selected the DB is purged before the new test is imported.
            if (CSVAppend == 2) ErrMessage += TestDBHelper.PurgeTest();
            ErrMessage += TestDBHelper.UploadCSV(dt);

            dt.Dispose();
            return RedirectToAction("Edit", new { Error = ErrMessage});
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Configuration()
        {
            ViewBag.L2Requirements = Config.GetString("L2Requirements");
            ViewBag.TestInstructions = Config.GetString("TestInstructions");
            ViewBag.NumberOfQuestions = Config.GetInt("NumberOfQuestions");
            ViewBag.PassingScore = Config.GetInt("PassingScore");
            ViewBag.TimeToTakeTest = Config.GetInt("TimeToTakeTest");
            ViewBag.TimeToStartTest = Config.GetInt("TimeToStartTest");

            return View();
        }

        [HttpPost, ValidateInput(false)]
        [Authorize] 
        public ActionResult Configuration(int formQuestions = -1, int formScore = -1, int formTimeToTake = -1, int formStart = -1, string formRequirements = "", string formInstructions = "")
        {
            //Get all the current values
            string L2Requirements = Config.GetString("L2Requirements");
            string TestInstructions = Config.GetString("TestInstructions");
            int NumberOfQuestions = Config.GetInt("NumberOfQuestions");
            int PassingScore = Config.GetInt("PassingScore");
            int TimeToTakeTest = Config.GetInt("TimeToTakeTest");
            int TimeToStartTest = Config.GetInt("TimeToStartTest");

            //Only update values if entry is valid and was changed/submitted
            if (formQuestions > 0) { NumberOfQuestions = formQuestions; }
            if (formScore > 0) { PassingScore = formScore; }
            if (formTimeToTake > 0) { TimeToTakeTest = formTimeToTake; }
            if (formStart > 0) { TimeToStartTest = formStart; }
            if (formRequirements != "") { L2Requirements = formRequirements; }
            if (formInstructions != "") { TestInstructions = formInstructions; }

            Config.Update(L2Requirements, TestInstructions, NumberOfQuestions, PassingScore, TimeToTakeTest, TimeToStartTest);

            return Redirect("~/Home/Configuration");
        }
    }
}