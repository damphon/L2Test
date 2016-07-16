using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L2Test.Helpers;
using L2Test.Models;

namespace L2Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Results()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            TestModels List = new TestModels();
            ViewBag.TestQuestions = List.EditListString();
            return View();
        }

        [HttpPost]
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
        public ActionResult EditQuestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditQuestion(string formQuestion, string formAnswer1, string formAnswer2, string formAnswer3, string formAnswer4, string formCatagory, string uid)
        {
            TestDBHelper testDBHelp = new TestDBHelper();
            TestModels.Delete(uid);
            testDBHelp.NewQuestion(formQuestion, formAnswer1, formAnswer2, formAnswer3, formAnswer4, formCatagory);

            return Redirect("~/Home/Edit");
        }

        [HttpPost]
        public ActionResult Delete(string uid)
        {
            TestModels.Delete(uid);
            return Redirect("~/Home/Edit");
        }

        public ActionResult Manage()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}