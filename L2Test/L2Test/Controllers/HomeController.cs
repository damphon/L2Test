using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using L2Test.Helpers;
using L2Test.Models;
using System.IO;
using System.Data;
using Ionic.Zip;

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
            ViewBag.HomePageText = Config.GetString("HomePage");

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
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            Image ImageHelp = new Image();
            return Json(ImageHelp.Upload(file));
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

            //REPLACE THIS!
            //CSVHelper help = new CSVHelper();
            //string csv = help.ExportAsCSV();
            //if (System.IO.File.Exists(Server.MapPath(@"~\Views\Tests\L2TestDB.csv"))) { System.IO.File.Delete(Server.MapPath(@"~\Views\Tests\L2TestDB.csv")); }
            //System.IO.File.WriteAllText(Server.MapPath(@"~\Views\Tests\L2TestDB.csv"), csv);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(Server.MapPath(@"~\Views\Tests"));
                zip.Save(Server.MapPath(@"~\App_Data\Backup.zip"));
            }
            return new FilePathResult(Server.MapPath(@"~\App_Data\Backup.zip"), "application/zip");
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.AboutPageText = Config.GetString("AboutPage");
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Configuration()
        {
            ViewBag.HomePage = Config.GetString("HomePage");
            ViewBag.AboutPage = Config.GetString("AboutPage");
            ViewBag.NumberOfQuestions = Config.GetInt("NumberOfQuestions");
            ViewBag.PassingScore = Config.GetInt("PassingScore");
            ViewBag.TimeToTakeTest = Config.GetInt("TimeToTakeTest");
            ViewBag.TimeToStartTest = Config.GetInt("TimeToStartTest");
            ViewBag.Name = Config.GetString("Name");

            return View();
        }

        [HttpPost, ValidateInput(false)]
        [Authorize] 
        public ActionResult Configuration(int formQuestions = -1, int formScore = -1, int formTimeToTake = -1, int formStart = -1, string formHomePage = "", string formAboutPage = "", string formName ="")
        {
            //Get all the current values
            string HomePage = Config.GetString("HomePage");
            string AboutPage = Config.GetString("AboutPage");
            string Name = Config.GetString("Name");
            int NumberOfQuestions = Config.GetInt("NumberOfQuestions");
            int PassingScore = Config.GetInt("PassingScore");
            int TimeToTakeTest = Config.GetInt("TimeToTakeTest");
            int TimeToStartTest = Config.GetInt("TimeToStartTest");

            //Only update values if entry is valid and was changed/submitted
            if (formQuestions > 0) { NumberOfQuestions = formQuestions; }
            if (formScore > 0) { PassingScore = formScore; }
            if (formTimeToTake > 0) { TimeToTakeTest = formTimeToTake; }
            if (formStart > 0) { TimeToStartTest = formStart; }
            if (formHomePage != "") { HomePage = formHomePage; }
            if (formAboutPage != "") { AboutPage = formAboutPage; }
            if (formName != "") { Name = formName; }

            Config.Update(HomePage, AboutPage, NumberOfQuestions, PassingScore, TimeToTakeTest, TimeToStartTest, Name);

            return Redirect("~/Home/Configuration");
        }

        [ChildActionOnly]
        public ActionResult SiteName()
        {
            return new ContentResult { Content = Config.GetString("Name") };
        }
    }
}