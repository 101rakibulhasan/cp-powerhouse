using cp_powerhouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace cp_powerhouse.Views
{
    /// <summary>
    /// Interaction logic for StatusWindow.xaml
    /// </summary>
    public partial class StatusWindow : Window
    {
        string message = "";
        string hint = "";
        string img_src = "";

        GlobalFunctions gf = new GlobalFunctions();
        public StatusWindow(int x)
        {
            // 0 = Internet Unavailable
            // 1 = Server's are Busy
            // 2 = Something Went Wrong
            InitializeComponent();

            switch(x)
            {
                case 0:
                    message = "Can not Connect to The Internet";
                    hint = "It seems your internet is not working, try restarting your Internet device...";
                    img_src = gf.GetResource("internet.png");
                    break;
                case 1:
                    message = "Can not access the Servers";
                    hint = "The servers are maybe busy or went offline. Try again later...";
                    img_src = gf.GetResource("server.png");
                    break;
                default:
                    message = "Ya'r a Wizard, Harry !";
                    hint = "Ya managed to break something only this Computer and God knows. Try Restarting maybe?";
                    img_src = gf.GetResource("weird.png");
                    break;
            }

            ImageSource imageSource = new BitmapImage(new Uri(img_src));
            StatusWindow_image.Source = imageSource;
            StatusWindow_Message.Content = message;
            StatusWindow_hint.Content = hint;
        }

        private void StatusWindow_OK(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
