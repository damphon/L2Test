using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L2Test.Models;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace L2Test.Helpers
{
    public class CSVHelper
    {
        public string ExportAsCSV()
        {
            List<TestModels> List = TestModels.QuestionList();
            string Result = "P_Id,Question,A1,A2,A3,A4,A5,A6,A7,A8,A1ID,A2ID,A3ID,A4ID,A5ID,A6ID,A7ID,A8ID,C1,C2,C3,C4,C5,C6,C7,C8,Category\n";//Header line of the CSV
            string TempString = "";
            foreach (var question in List)
            {
                StringBuilder sb = new StringBuilder(TempString);
                sb.Append(question.key);
                sb.Append(",");
                sb.Append(Sanitize(question.question));
                sb.Append(",");
                sb.Append(Sanitize(question.answer1));
                sb.Append(",");
                sb.Append(Sanitize(question.answer2));
                sb.Append(",");
                sb.Append(Sanitize(question.answer3));
                sb.Append(",");
                sb.Append(Sanitize(question.answer4));
                sb.Append(",");
                sb.Append(Sanitize(question.answer5));
                sb.Append(",");
                sb.Append(Sanitize(question.answer6));
                sb.Append(",");
                sb.Append(Sanitize(question.answer7));
                sb.Append(",");
                sb.Append(question.answer8);
                sb.Append(",");
                sb.Append(question.aid1);
                sb.Append(",");
                sb.Append(question.aid2);
                sb.Append(",");
                sb.Append(question.aid3);
                sb.Append(",");
                sb.Append(question.aid4);
                sb.Append(",");
                sb.Append(question.aid5);
                sb.Append(",");
                sb.Append(question.aid6);
                sb.Append(",");
                sb.Append(question.aid7);
                sb.Append(",");
                sb.Append(question.aid8);
                sb.Append(",");
                sb.Append(question.c1);
                sb.Append(",");
                sb.Append(question.c2);
                sb.Append(",");
                sb.Append(question.c3);
                sb.Append(",");
                sb.Append(question.c4);
                sb.Append(",");
                sb.Append(question.c5);
                sb.Append(",");
                sb.Append(question.c6);
                sb.Append(",");
                sb.Append(question.c7);
                sb.Append(",");
                sb.Append(question.c8);
                sb.Append(",");
                sb.Append(Sanitize(question.category));
                sb.Append("\n");
                TempString = sb.ToString();
            }

            return Result + TempString;
        }

        public static DataTable ProcessCSV(string filename)
        {
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;

            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            //Set the filename in to our stream
            StreamReader sr = new StreamReader(filename);

            //Read the first line and split the string at , with our regular expression in to an array
            line = sr.ReadLine();
            strArray = r.Split(line);

            //For each item in the new split array, dynamically builds our Data columns. Save us having to worry about it.
            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));

            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                row = dt.NewRow();

                //add our current value to our data row
                row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }

            //Tidy Streameader up
            sr.Dispose();

            //Edit the Datatable to add appropriate Types.
            DataTable dtCloned = dt.Clone();
            dtCloned.Columns[0].DataType = typeof(Int32);
            dtCloned.Columns[18].DataType = typeof(bool);
            dtCloned.Columns[19].DataType = typeof(bool);
            dtCloned.Columns[20].DataType = typeof(bool);
            dtCloned.Columns[21].DataType = typeof(bool);
            dtCloned.Columns[22].DataType = typeof(bool);
            dtCloned.Columns[23].DataType = typeof(bool);
            dtCloned.Columns[24].DataType = typeof(bool);
            dtCloned.Columns[25].DataType = typeof(bool);
            foreach (DataRow dtr in dt.Rows)
            {
                dtCloned.ImportRow(dtr);
            }

            //return a the new DataTable
            return dt;
        }

        private string Sanitize(string text)
        {
            if (text == null) return string.Empty;
            text = MicrosoftTextFix(text);

            bool containsQuote = false;
            bool containsComma = false;
            int len = text.Length;

            for (int i = 0; i < len && (containsComma == false || containsQuote == false); i++)
            {
                char ch = text[i];
                if (ch == '"')
                {
                    containsQuote = true;
                }
                else if (ch == ',' || char.IsControl(ch))
                {
                    containsComma = true;
                }
            }

            bool mustQuote = containsComma || containsQuote;

            if (containsQuote)
            {
                text = text.Replace("\"", "\"\"");
            }

            // Quote the cell and replace embedded quotes with double-quote or just return as is
            return mustQuote ? "\"" + text + "\"" : text;
        }

        private string MicrosoftTextFix(string text)
        {
            if (text.IndexOf('\u2013') > -1) text = text.Replace('\u2013', '-'); // en dash
            if (text.IndexOf('\u2014') > -1) text = text.Replace('\u2014', '-'); // em dash
            if (text.IndexOf('\u2015') > -1) text = text.Replace('\u2015', '-'); // horizontal bar
            if (text.IndexOf('\u2017') > -1) text = text.Replace('\u2017', '_'); // double low line
            if (text.IndexOf('\u2018') > -1) text = text.Replace('\u2018', '\''); // left single quotation mark
            if (text.IndexOf('\u2019') > -1) text = text.Replace('\u2019', '\''); // right single quotation mark
            if (text.IndexOf('\u201a') > -1) text = text.Replace('\u201a', '\''); // single low-9 quotation mark
            if (text.IndexOf('\u201b') > -1) text = text.Replace('\u201b', '\''); // single high-reversed-9 quotation mark
            if (text.IndexOf('\u201c') > -1) text = text.Replace('\u201c', '\"'); // left double quotation mark
            if (text.IndexOf('\u201d') > -1) text = text.Replace('\u201d', '\"'); // right double quotation mark
            if (text.IndexOf('\u201e') > -1) text = text.Replace('\u201e', '\"'); // double low-9 quotation mark
            if (text.IndexOf('\u2026') > -1) text = text.Replace("\u2026", "..."); // horizontal ellipsis
            if (text.IndexOf('\u2032') > -1) text = text.Replace('\u2032', '\''); // prime

            //Below added to remove new lines (The sql database can handle them but a csv file cannot.)
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            return text.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
        } 
    }
}