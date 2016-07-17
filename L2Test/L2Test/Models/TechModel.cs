using System;
using L2Test.Helpers;

namespace L2Test.Models
{
    public class TechModels
    {
        public int TechKey { get; set; }
        public string TechName { get; set; }
        public string TechID { get; set; }
        public DateTime Time { get; set; }

        public bool isValid(string ID)
        {
            TechDBHelper dbhelp = new TechDBHelper();
            var tech = dbhelp.CheckID(ID);

            if (tech != null)
            {
                foreach (var entry in tech)
                {
                    TimeSpan span = DateTime.Now - entry.Time;
                    if (span.Minutes < 90)
                        return true;
                }
            }
            return false;
        }
    }
}