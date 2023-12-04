using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using cp_powerhouse.Models;
using System.Timers;
using System.Collections.ObjectModel;
using System.Media;
using System.IO.Packaging;
using System.Threading;
using System.Windows.Threading;

namespace cp_powerhouse
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    public partial class Notification : Window
    {
        int dis_width = (int)SystemParameters.PrimaryScreenWidth;
        int dis_height = (int)SystemParameters.PrimaryScreenHeight;

        GlobalFunctions gf = new GlobalFunctions();

        public ObservableCollection<ContestItems>? ContestList { get; set; }

        private System.Timers.Timer timer2;

        public Notification(ObservableCollection<ContestItems> c)
        {
            ContestList = c;
            InitializeComponent();
            FixPosition();
            Notification_ContestTitle.Content = ContestList[0].name;
            Notification_ContestTime.Content = ContestList[0].startTimeSecondsView;

            timer2 = new System.Timers.Timer();
            timer2.Elapsed += TimerElapsed;
            // Set the time when you want the window to open (2:00 PM on October 13, 2023)
            DateTime scheduledTime = gf.convertStringToTime(ContestList[0].startTimeSecondsView);

            // Calculate the time interval until the scheduled time
            TimeSpan timeUntilScheduled = scheduledTime - DateTime.Now;

            // Set the interval of the timer to the time remaining until the scheduled time
            timer2.Interval = timeUntilScheduled.TotalMilliseconds;

            int ThresholdTime = 0;
            switch (Properties.Settings.Default.notitime_ind)
            {
                case 0:
                    ThresholdTime = 12; break;
                case 1:
                    ThresholdTime = 6; break;
                case 2:
                    ThresholdTime = 24; break;
            }

            if (timeUntilScheduled.TotalHours < ThresholdTime)
            {
                ShowNotification();
            }

            // Start the timer
            timer2.Start();
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Stop the timer
            timer2.Stop();
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Show();
            });
        }

        private void ShowNotification()
        {
            string audioFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views/Resources/default.wav");
            if(Properties.Settings.Default.notisound_ind == 1)
            {
                audioFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views/Resources/bubble.wav");
            }
            SoundPlayer soundPlayer = new SoundPlayer(audioFilePath);
            soundPlayer.Play();
            this.Show();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += HideNotification;
            timer.Interval = TimeSpan.FromSeconds(10);

            // Start the timer
            timer.Start();
        }
        private void HideNotification(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FixPosition()
        {
            this.Left = dis_width - this.Width - 20;
            this.Top = dis_height - this.Height - 60;
            this.Topmost = true;

            string platformIconURL = gf.GetResource("contest_icn.png");
            if (ContestList[0].platform == "CodeForces")
            {
                platformIconURL = gf.GetResource("codeforces.png");
            }
            else if (ContestList[0].platform == "UVa")
            {
                platformIconURL = gf.GetResource("uva.png");
            }

            ImageSource imageSource = new BitmapImage(new Uri(platformIconURL));
            Notification_platformIcon.Source = imageSource;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Notification_mouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            string id = ContestList[0].id.ToString();
            string uri = "https://codeforces.com/contests/";
            if (ContestList[0].platform == "CodeForces")
            {
                uri = "https://codeforces.com/contests/" + id;
            }
            else if (ContestList[0].platform == "UVa")
            {
                uri = "https://onlinejudge.org/index.php?option=com_onlinejudge&Itemid=13&page=show_contest&contest=" + id;
            }

            gf.LoadURL(uri);
        }
    }
}
