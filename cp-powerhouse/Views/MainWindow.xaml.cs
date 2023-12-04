using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using cp_powerhouse.Models;
using cp_powerhouse.Models.ListObjects;
using System.Security.Policy;

namespace cp_powerhouse
{
    public partial class MainWindow : Window
    {
        //// INITIALIZE VARIABLES
        int main_width;
        int main_height;

        Contests contests = new Contests();
        GlobalFunctions gf = new GlobalFunctions();
        Settings settings = new Settings();
        ToDoList tdl = new ToDoList();
        Friends frnd = new Friends();
        DatabaseService dbs = new DatabaseService();

        public ObservableCollection<ContestItems>? ContestList { get; set; }

        //// INITIALIZE VARIABLES ENDS

        //// CODE FOR BODY STARTS
        public MainWindow(ObservableCollection<ContestItems> c)
        {
            ContestList = c;
            InitializeComponent();
            main_width = (int)this.body.Width;
            main_height = (int)this.body.Height;

            InitializePages();
        }

        private void InitializePages()
        {
            dbs.StartConnection();
            if (ContestList is not null)
            {
                Contest_ListView.ItemsSource = ContestList;
                Contest_ListView.Loaded += ListView_Loaded;
            }

            contests.SetFavouriteContestList(dbs.FetchDataFavContest());
            FavContest_ListView.ItemsSource = contests.FavouriteContestList();

            tdl.SetToDoList(dbs.FetchDataProblemset());
            ToDoList_ListView.ItemsSource = tdl.GetToDoList();

            frnd.SetFriendList(dbs.FetchDataFriends());
            friends_ListView.ItemsSource = frnd.GetFriendList();

            Settings_ThemeSelection.SelectedIndex = Properties.Settings.Default.theme_ind;
            Settings_NotisoundSelection.SelectedIndex = Properties.Settings.Default.notisound_ind;
            Settings_NotitimeSelection.SelectedIndex = Properties.Settings.Default.notitime_ind;
            ApplySettings();
        }

        private LinearGradientBrush CFGrad()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 1);
            gradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0.0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Orange, 1.0));
            return gradientBrush;
        }

        private LinearGradientBrush CCGrad()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 1);
            gradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0.0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.OrangeRed, 1.0));
            return gradientBrush;
        }
        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Contest_ListView.Items.Count; i++)
            {
                ListViewItem listViewItem = (ListViewItem)Contest_ListView.ItemContainerGenerator.ContainerFromIndex(i);
                if (listViewItem != null)
                {
                    var item = ContestList[i].platform;
                    if (item == "CodeForces")
                    {
                        listViewItem.Background = CFGrad();
                    }else if(item == "codechef.com")
                    {
                        listViewItem.Background = CCGrad();
                    }
                }
            }
        }

        // DRAG BODY WITH MOUSE
        private void body_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // DOUBLE CLICK TO MAXIMIZE
        // private bool IsMaximized = false;
        /*private void body_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }*/

        // CLOSE BUTTON EVENT
        private void close_MouseDown(object sender, RoutedEventArgs e)
        {
            this.Close();
            // Application.Current.Shutdown();
        }

        // MAXIMIZE BUTTON EVENT
        /*private void maximize_Click(object sender, RoutedEventArgs e)
        {
            if (IsMaximized)
            {
                this.WindowState = WindowState.Normal;
                IsMaximized = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                IsMaximized = true;
            }
        }*/

        // MINIMIZE BUTTON EVENT
        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // CODE
        private void contestPagebtn_Click(object sender, RoutedEventArgs e)
        {
            ContestPage.Visibility = Visibility.Visible;
            ToDoListPage.Visibility = Visibility.Hidden;
            FriendsPage.Visibility = Visibility.Hidden;
            ProfilePage.Visibility = Visibility.Hidden;
            SettingsPage.Visibility = Visibility.Hidden;

            contest_select.Visibility = Visibility.Visible;
            todolist_select.Visibility = Visibility.Hidden;
            friends_select.Visibility = Visibility.Hidden;
            profile_select.Visibility = Visibility.Hidden;

            pageTitle.Content = "CONTESTS";
        }

        private void friendsPage_btn_Click(object sender, RoutedEventArgs e)
        {
            ContestPage.Visibility = Visibility.Hidden;
            ToDoListPage.Visibility = Visibility.Hidden;
            FriendsPage.Visibility = Visibility.Visible;
            ProfilePage.Visibility = Visibility.Hidden;
            SettingsPage.Visibility = Visibility.Hidden;

            contest_select.Visibility = Visibility.Hidden;
            todolist_select.Visibility = Visibility.Hidden;
            friends_select.Visibility = Visibility.Visible;
            profile_select.Visibility = Visibility.Hidden;

            pageTitle.Content = "FRIENDS";
        }

        private void todolistPage_btn_Click(object sender, RoutedEventArgs e)
        {
            ContestPage.Visibility = Visibility.Hidden;
            ToDoListPage.Visibility = Visibility.Visible;
            FriendsPage.Visibility = Visibility.Hidden;
            ProfilePage.Visibility = Visibility.Hidden;
            SettingsPage.Visibility = Visibility.Hidden;

            contest_select.Visibility = Visibility.Hidden;
            todolist_select.Visibility = Visibility.Visible;
            friends_select.Visibility = Visibility.Hidden;
            profile_select.Visibility = Visibility.Hidden;

            pageTitle.Content = "TODOLIST";
        }

        private void profilePage_btn_Click(object sender, RoutedEventArgs e)
        {
            ContestPage.Visibility = Visibility.Hidden;
            ToDoListPage.Visibility = Visibility.Hidden;
            FriendsPage.Visibility = Visibility.Hidden;
            ProfilePage.Visibility = Visibility.Visible;
            SettingsPage.Visibility = Visibility.Hidden;

            contest_select.Visibility = Visibility.Hidden;
            todolist_select.Visibility = Visibility.Hidden;
            friends_select.Visibility = Visibility.Hidden;
            profile_select.Visibility = Visibility.Visible;

            pageTitle.Content = "PROFILE";
        }

        private void settingsPage_btn_Click(object sender, RoutedEventArgs e)
        {
            ContestPage.Visibility = Visibility.Hidden;
            ToDoListPage.Visibility = Visibility.Hidden;
            FriendsPage.Visibility = Visibility.Hidden;
            ProfilePage.Visibility = Visibility.Hidden;
            SettingsPage.Visibility = Visibility.Visible;

            contest_select.Visibility = Visibility.Hidden;
            todolist_select.Visibility = Visibility.Hidden;
            friends_select.Visibility = Visibility.Hidden;
            profile_select.Visibility = Visibility.Hidden;

            pageTitle.Content = "SETTINGS";
        }
        //
        public void RefreshList(ItemsControl ic)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ic.ItemsSource);
            view.Refresh();
        }

        private void contestSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (Contest_ListView.SelectedItems.Count > 0)
            {
                ContestListItemAction(Contest_ListView.SelectedIndex);
                Contest_ListView.SelectedItem = null;
            }
        }

        private void favouriteContestSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (FavContest_ListView.SelectedItems.Count > 0)
            {
                FavouriteContestListItemAction(FavContest_ListView.SelectedIndex);
                FavContest_ListView.SelectedItem = null;
            }
        }

        private void ContestListItemAction(int itempos)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                string uri = ContestList[itempos].link ;
                gf.LoadURL(uri);
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                MessageBox.Show(ContestList[itempos].name + " has been added to Favourites! ");
                dbs.AddDataFavContest(ContestList[itempos].id, ContestList[itempos].name, ContestList[itempos].startTimeSeconds, ContestList[itempos].durationSeconds, ContestList[itempos].phase, ContestList[itempos].platform, ContestList[itempos].startTimeSecondsView, ContestList[itempos].durationSecondsView, ContestList[itempos].link) ;
                contests.AddFavouriteItem(ContestList[itempos]);
                RefreshList(FavContest_ListView);
            }
        }

        private void FavouriteContestListItemAction(int favitempos)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                string uri = contests.FavouriteContestList()[favitempos].link;

                gf.LoadURL(uri);
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                if (favitempos != -1)
                {
                    MessageBox.Show(contests.FavouriteContestList()[favitempos].name + " has been removed!");
                    dbs.RemoveDataFavContest(contests.FavouriteContestList()[favitempos].name,contests.FavouriteContestList()[favitempos].startTimeSeconds);
                    contests.RemoveFavouriteItem(favitempos);
                    RefreshList(FavContest_ListView);
                }
            }
        }

        private void Contest_RefreshCon_FavconList_btn_Click(object sender, RoutedEventArgs e)
        {
            RefreshList(FavContest_ListView);
            RefreshList(Contest_ListView);
        }

        private void ApplySettings()
        {
            settings.ChangeTheme(Settings_ThemeSelection.SelectedIndex);
            settings.ChangeNotiSound(Settings_NotisoundSelection.SelectedIndex);
            settings.ChangeNotiTime(Settings_NotitimeSelection.SelectedIndex);
        }

        private void Settings_SaveButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings has been applied!");
            ApplySettings();
            Properties.Settings.Default.Save();
        }

        private void Settings_onThemeSelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Settings_ResetClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            Settings_ThemeSelection.SelectedIndex = Properties.Settings.Default.theme_ind;
            Settings_NotisoundSelection.SelectedIndex = Properties.Settings.Default.notisound_ind;
            Settings_NotitimeSelection.SelectedIndex = Properties.Settings.Default.notitime_ind;

            ApplySettings();
            MessageBox.Show("Setting has been reseted!");

        }

        private void Contests_ChangeContestListViewTab(object sender, RoutedEventArgs e)
        {
            ListViewTabController.SelectedIndex = 0;
            pageTitle.Content = "CONTESTS";
        }

        private void Contests_ChangeFavouriteListViewTab(object sender, RoutedEventArgs e)
        {
            ListViewTabController.SelectedIndex = 1;
            pageTitle.Content = "FAVOURITES";
        }

        private void ToDoList_AddProblemClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ToDoList_ProblemTitle.Text) && string.IsNullOrWhiteSpace(ToDoList_ProblemURL.Text))
            {
                MessageBox.Show("Problem Title and URL is empty!");
            } else
            {
                if (Uri.IsWellFormedUriString(ToDoList_ProblemURL.Text, UriKind.Absolute))
                {
                    ToDoListItems tdl_item = new ToDoListItems();
                    tdl_item.platform = ToDoList_Platform.Text;
                    tdl_item.name = ToDoList_ProblemTitle.Text;
                    tdl_item.link = ToDoList_ProblemURL.Text;

                    tdl.AddProblemItem(tdl_item);

                    dbs.AddDataProblemset(tdl_item.platform, tdl_item.name, tdl_item.link);

                    ToDoList_ProblemURL.Clear();
                    ToDoList_ProblemTitle.Clear();
                }
                else
                {
                    MessageBox.Show("The problem URL is not valid!");
                }

            }
        }

        private void ToDoList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int pos = ToDoList_ListView.SelectedIndex;
            if (ToDoList_ListView.SelectedItems.Count > 0)
            {
                ToDoList_ListView_ListItemAction(pos);
                ToDoList_ListView.SelectedItem = null;
            }
        }

        private void ToDoList_ListView_ListItemAction(int pos)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                string uri = tdl.GetToDoList()[pos].link;
                gf.LoadURL(uri);
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                if (pos != -1)
                {
                    MessageBox.Show(tdl.GetToDoList()[pos].name + " has been removed!");
                    dbs.RemoveDataProblemset(tdl.GetToDoList()[pos].platform, tdl.GetToDoList()[pos].name, tdl.GetToDoList()[pos].link);
                    tdl.RemoveProblemItem(pos);
                    RefreshList(ToDoList_ListView);
                }
            }
        }

        private void ToDoList_onTextChange(object sender, TextChangedEventArgs e)
        {
            string searchText = ToDoList_Search.Text.ToLower();

            if(ToDoList_ListView is not null)
            {
                ToDoList_ListView.Items.Filter = item =>
                {
                    ToDoListItems currentItem = (ToDoListItems)item;
                    return currentItem.name.ToLower().Contains(searchText);
                };
            }           
        }

        private void friends_AddFriendsEvent(object sender, RoutedEventArgs e)
        {
            string username = friends_Username.Text;
            string? platform = ((ComboBoxItem)codingPlatformComboBox.SelectedItem)?.Content.ToString();

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(platform))
            {
                FriendItem newFriend = new FriendItem();
                newFriend.Username = username;
                newFriend.Platform = platform;
                newFriend.ProfileUrl = frnd.GetProfileLink(platform,username);

                dbs.AddDataFriends(platform,username, newFriend.ProfileUrl);
                frnd.AddItemFriend(newFriend);

                friends_Username.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a username and select a platform.");
            }
        }

        private void friends_selection_change(object sender, SelectionChangedEventArgs e)
        {
            int pos = friends_ListView.SelectedIndex;
            if (friends_ListView.SelectedItems.Count > 0)
            {
                Friends_selectionChangeAction(pos);
                friends_ListView.SelectedItem = null;
            }
        }

        private void Friends_selectionChangeAction(int pos)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                GlobalFunctions friendSelection = new GlobalFunctions();
                string selectedString = frnd.GetFriendList()[pos].ProfileUrl;
                friendSelection.LoadURL(selectedString);
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                if (pos != -1)
                {
                    MessageBox.Show(frnd.GetFriendList()[pos].Username+" has been removed!");
                    dbs.RemoveDataFriends(frnd.GetFriendList()[pos].Platform, frnd.GetFriendList()[pos].Username, frnd.GetFriendList()[pos].ProfileUrl);
                    frnd.GetFriendList().RemoveAt(pos);
                }


            }
        }
    }
}
