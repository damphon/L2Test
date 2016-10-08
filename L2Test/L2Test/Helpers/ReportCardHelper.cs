using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L2Test.Models;
using System.Text;

namespace L2Test.Helpers
{
    public class ReportCardHelper
    {
        public string GetGradedReport()
        {
            ReportCardDBHelper DBhelper = new ReportCardDBHelper();
            List<ReportCardModels> Temp = DBhelper.GetReportCards();
            var ReportCardList = Temp.OrderByDescending(x => x.time).ToList();
            string ReportCards = "";

            StringBuilder sb = new StringBuilder(ReportCards);

            foreach (var ReportCard in ReportCardList)
            {
                if(!ReportCard.testURL.Contains("ungraded")){
                    sb.Append("<li class='Record'><a href='");
                    sb.Append(ReportCard.testURL);
                    sb.Append("'>");
                    sb.Append(ReportCard.tech);
                    sb.Append(" - ");
                    sb.Append(ReportCard.time.ToString("yyyy/MM/dd HH:mm"));
                    sb.Append("</a>");
                }
                ReportCards = sb.ToString();
            }

            return ReportCards;
        }

        public string GetArchiveReport()
        {
            ReportCardDBHelper DBhelper = new ReportCardDBHelper();
            List<ReportCardModels> Temp = DBhelper.GetReportCards();
            var ReportCardList = Temp.OrderByDescending(x => x.time).ToList();
            string ReportCards = "";

            StringBuilder sb = new StringBuilder(ReportCards);

            foreach (var ReportCard in ReportCardList)
            {
                if (ReportCard.testURL.Contains("Ungraded"))//The file path for the archives is the same as the graded except for a directory named ungraded.
                {
                    sb.Append("<li class='Record'><a href='");
                    sb.Append(ReportCard.testURL);
                    sb.Append("'>");
                    sb.Append(ReportCard.tech);
                    sb.Append(" - ");
                    sb.Append(ReportCard.time.ToString("yyyy/MM/dd HH:mm"));
                    sb.Append("</a>");
                }
                ReportCards = sb.ToString();
            }

            return ReportCards;
        }
    }
}