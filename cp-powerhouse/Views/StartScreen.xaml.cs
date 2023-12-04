using WpfAnimatedGif;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

using cp_powerhouse;
using cp_powerhouse.Models;
using System.Collections.ObjectModel;
using System.Collections;

namespace cp_powerhouse.Views
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Window
    {
        //private DispatcherTimer timer;
        GlobalFunctions gf = new GlobalFunctions();
        public StartScreen()
        {
            InitializeComponent();
            ImageSource cfSource = new BitmapImage(new Uri(gf.GetResource("loading.gif")));
            ImageBehavior.SetAnimatedSource(cfStatus_img, cfSource);

            ImageSource uvaSource = new BitmapImage(new Uri(gf.GetResource("loading.gif")));
            ImageBehavior.SetAnimatedSource(uvaStatus_img, uvaSource);
        }
    }
}
