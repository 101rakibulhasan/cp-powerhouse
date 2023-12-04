using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using cp_powerhouse.Models;
using System.Configuration;
using System.Windows.Controls.Primitives;

namespace cp_powerhouse
{
    internal class Settings
    {
        GlobalFunctions gf = new GlobalFunctions();

        public string rt_loc_def;
        public string rt_loc_bubble;

        public Settings()
        {
            rt_loc_def = gf.GetResource("Default.mp3");
            rt_loc_bubble = gf.GetResource("Bubble.mp3");
        }

        public void ChangeTheme(int i)
        {
            ResourceDictionary Theme = new ResourceDictionary();
            App.Current.Resources.Clear();
            switch (i)
            {
                case 0:
                    Theme.Source = new Uri("/Views/Styles/Light.xaml", UriKind.Relative);
                    Properties.Settings.Default.theme_ind = 0;
                    break;

                case 1:
                    Theme.Source = new Uri("/Views/Styles/Dark.xaml", UriKind.Relative);
                    Properties.Settings.Default.theme_ind = 1;
                    break;
            }
            App.Current.Resources.MergedDictionaries.Add(Theme);       
        }

        public void ChangeNotiSound(int i)
        {
            switch (i)
            {
                case 0:
                    Properties.Settings.Default.notisound_ind = 0;
                    break;

                case 1:
                    Properties.Settings.Default.notisound_ind = 1;
                    break;
            }
        }

        public void ChangeNotiTime(int i)
        {
            switch (i)
            {
                case 0:
                    Properties.Settings.Default.notitime_ind = 0;
                    break;

                case 1:
                    Properties.Settings.Default.notitime_ind = 1;
                    break;

                case 2:
                    Properties.Settings.Default.notitime_ind = 2;
                    break;
            }
        }
    }
}
