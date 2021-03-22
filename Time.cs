using System;
using System.Linq;

namespace NeuroTraining
{
    public class Time
    {
        public string MakeTime(DateTime StartTime)
        {
            string TimeValue = string.Empty;
            if ((DateTime.Now - StartTime).Hours * 3600 + (DateTime.Now - StartTime).Minutes * 60 + (DateTime.Now - StartTime).Seconds < 60)
            {
                TimeValue = (DateTime.Now - StartTime).Seconds.ToString();
            }
            else if ((DateTime.Now - StartTime).Hours * 3600 + (DateTime.Now - StartTime).Minutes * 60 + (DateTime.Now - StartTime).Seconds >= 60
                && (DateTime.Now - StartTime).Hours * 3600 + (DateTime.Now - StartTime).Minutes * 60 + (DateTime.Now - StartTime).Seconds < 3600)
            {
                TimeValue = (DateTime.Now - StartTime).Minutes.ToString() + ":" + (DateTime.Now - StartTime).Seconds.ToString();
            }
            else if ((DateTime.Now - StartTime).Hours * 3600 + (DateTime.Now - StartTime).Minutes * 60 + (DateTime.Now - StartTime).Seconds >= 3600)
            {
                TimeValue = (DateTime.Now - StartTime).Hours.ToString() + ":" + (DateTime.Now - StartTime).Minutes.ToString() + ":" + (DateTime.Now - StartTime).Seconds.ToString();
            }
            return TimeValue;
        }
        public string TransformSecondsToTimeValue(double resultSeconds)
        {
            string timeValue = string.Empty;
            if (resultSeconds >= 3600)
            {
                timeValue = Math.Round(resultSeconds / 3600, 0).ToString() + ":"
                    + Math.Round(resultSeconds % 3600 / 60, 0).ToString() + ":"
                    + Math.Round(resultSeconds % 3600 % 60, 0).ToString();
            }
            else if (resultSeconds < 3600 && resultSeconds >= 60)
            {
                timeValue = Math.Round(resultSeconds / 60, 0).ToString() + ":"
                    + Math.Round(resultSeconds % 60, 0).ToString();
            }
            else if (resultSeconds < 60)
            {
                timeValue = resultSeconds.ToString();
            }
            return timeValue;
        }
        public int TransformTimeValueToSeconds(string result)
        {
            int a = 0;
            int blocks = result.Count(f => f == ':');
            switch (blocks)
            {
                case 0:
                    a = Int32.Parse(result);
                    break;
                case 1:
                    a = Int32.Parse(result.Split(':')[0]) * 60 + Int32.Parse(result.Split(':')[1]);
                    break;
                case 2:
                    a = Int32.Parse(result.Split(':')[0]) * 3600 + Int32.Parse(result.Split(':')[1]) * 60 + Int32.Parse(result.Split(':')[2]);
                    break;
            }
            return a;
        }
    }
}
