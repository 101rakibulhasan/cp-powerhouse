using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cp_powerhouse.Models
{
    internal class GlobalFunctions
    {
        public bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public void LoadURL(string uri)
        {
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            Process.Start(psi);
        }

        public string convertTime(string pl, int x)
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            if (pl == "UVa")
            {
                x -= 24 * 60 * 60;
            }
            DateTime result = epochStart.AddSeconds(x);
            DateTime currentLocalTime = TimeZoneInfo.ConvertTimeFromUtc(result, localTimeZone);

            string isoStartTimeView = currentLocalTime.ToString("dd MMMM yyyy, hh:mm tt");
            return isoStartTimeView;
        }

        public DateTime convertStringToTime(string time)
        {
            DateTime dateTime;
            string format = "dd MMMM yyyy, hh:mm tt";
            DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            return dateTime;
        }

        public string convertDuration(int x)
        {
            int h_dur = x / 3600;
            int m_dur = (x % 3600) / 60;
            if (h_dur > 12) h_dur -= 12;
            else if (h_dur == 0) h_dur = 12;

            string h_dur_str = h_dur.ToString(), m_dur_str = m_dur.ToString();
            if (h_dur_str.Length == 1) h_dur_str = '0' + h_dur_str.ToString();
            if (m_dur_str.Length == 1) m_dur_str = '0' + m_dur_str.ToString();
            string isoDurationTime = $"{h_dur_str}:{m_dur_str}";
            return isoDurationTime;
        }

        public string GetResource(string x)
        {
            return "pack://application:,,,/Views/Resources/" + x;
        }
    }
}
