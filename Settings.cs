using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NeuroTraining
{
    public class Settings
    {
        public string dir
        {
            get
            {
                string dir = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location) + @"_NeuroTraining\";
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return dir;
            }
        }
        /// <summary>
        /// RememberNumber: "SettingsRememberNumber.ini"
        /// SetDigits: 4
        /// SetExposure:2
        /// SetNOfTries:3
        /// SetTotalTries:15
        /// 
        /// EvenNumbers: "SettingsEvenNumbers.ini"
        /// Total_Time: 100
        /// NOfButtons: 40
        /// 
        /// Concentration: "SettingsConcentration.ini"
        /// Target_Points: 5
        /// Other_Points: 9
        /// Overall_Time: 100
        /// Initiation_Time: 4
        /// </summary>
        public List<string> LoadSettings(string filename)
        {
            List<string> list = new List<string>();
            if (File.Exists(dir + filename))
            {
                string line = string.Empty;
                line = File.ReadAllText(dir + filename, Encoding.UTF8);
                string[] split = line.Split('\n');
                foreach (string s in split)
                {
                    if (!String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s))
                    {
                        list.Add(s);
                    }
                }
            }
            else
            {
                if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
                string s = string.Empty;
                switch (filename)
                {
                    case "SettingsRememberNumber.ini":
                        s = "4\n2\n3\n15\n";
                        list = new List<string>() { "4", "2", "3", "15" };
                        break;
                    case "SettingsEvenNumbers.ini":
                        s = "100\n40\n";
                        list = new List<string>() { "100", "40"};
                        break;
                    case "SettingsConcentration.ini":
                        s = "5\n9\n100\n4\n";
                        list = new List<string>() { "5", "9", "100", "4" };
                        break;
                }
                File.WriteAllText(dir + filename, s, Encoding.UTF8);
            }
            return list;
        }
    }
}
