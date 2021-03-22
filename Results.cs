using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace NeuroTraining
{
    public class Results
    {
        Time time = new Time();
        public string dir
        {
            get
            {
                string dir = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\_NeuroTraining\";
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return dir;
            }
        }
        public List<int> LoadResults(string filename, string gameParameter)
        {
            List<int> result = new List<int>();
            if (File.Exists(dir + filename))
            {
                List<string> lines = new List<string>(File.ReadLines(dir + filename, Encoding.UTF8));
                foreach (string l in lines)
                {
                    if (!String.IsNullOrEmpty(l) && l.Contains(gameParameter + "_"))
                    {
                        string lSplit = l.Split('_')[1];
                        var lineSplit = new List<string>(SplitString1(lSplit, ','));
                        for (int i = 0; i < lineSplit.Count(); i++)
                        {
                            if (!String.IsNullOrEmpty(lineSplit[i]) && lineSplit[i] != "\n" && lineSplit[i] != "\r")
                            {
                                if (lineSplit[i].Contains(":"))
                                {
                                    result.Add(time.TransformTimeValueToSeconds(lineSplit[i]));
                                }
                                else
                                {
                                    result.Add(Int32.Parse(lineSplit[i]));

                                }
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                result.Add(-1);
            }
            if (result.Count == 0)
            {
                result.Add(-1);
            }
            return result;
        }
        public void SaveResults(List<int> Results, string filename, string gameParameter)
        {
            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
            string res = gameParameter + "_";
            if (Results.Count != 0)
            {
                foreach (int i in Results)
                {
                    if (filename.Contains("Schulte"))
                    {
                        res += time.TransformSecondsToTimeValue((double)i) + ",";
                    }
                    else
                    {
                        res += i.ToString() + ",";
                    }
                }
            }
            if (!File.Exists(dir + filename))
            {
                File.WriteAllText(dir + filename, res, Encoding.UTF8);
            }
            else
            {
                List<string> lines = new List<string>(File.ReadLines(dir + filename, Encoding.UTF8));
                for (int i = 0; i < lines.Count; i++)
                {
                    if (!String.IsNullOrEmpty(lines[i]))
                    {
                        if (lines[i].Contains(gameParameter + "_"))
                        {
                            lines.Remove(lines[i]);
                        }
                    }
                }
                lines.Add(res);
                File.WriteAllLines(dir + filename, lines, Encoding.UTF8);
            }
        }
        public string FindBest(string ResultsLine)
        {
            Time time = new Time();
            ResultsLine = ResultsLine.Substring(3, ResultsLine.Length - 4);
            string[] split = ResultsLine.Split(',');
            List<double> ResultsSecs = new List<double>();
            foreach (string s in split)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    ResultsSecs.Add(time.TransformTimeValueToSeconds(s));
                }
            }
            ResultsSecs.Sort();
            return time.TransformSecondsToTimeValue(ResultsSecs[0]);
        }
        public List<string> SplitString1(string text, char ch)
        {
            List<string> list = new List<string>();
            string[] split = text.Split(ch);
            foreach (string spl in split)
            {
                if (!String.IsNullOrEmpty(spl) && !String.IsNullOrWhiteSpace(spl))
                {
                    list.Add(spl);
                }
            }
            return list;
        }
        public List<int> SplitString2(string text, char ch)
        {
            List<int> list = new List<int>();
            string[] split = text.Split(ch);
            foreach (string str in split)
            {
                if (!String.IsNullOrEmpty(str) && !String.IsNullOrWhiteSpace(str) && str != "\n" && str != "\r")
                {
                    list.Add(Int32.Parse(str));
                }
            }
            return list;
        }
        public List<int> SplitString3(string text, char ch)
        {
            List<int> list = new List<int>();
            string[] split = text.Split(ch);
            foreach (string spl in split)
            {
                if (!String.IsNullOrEmpty(spl) && !String.IsNullOrWhiteSpace(spl) && spl != "\n" && spl != "\r")
                {
                    list.Add(Int32.Parse(spl));
                }
            }
            return list;
        }
    }
}
