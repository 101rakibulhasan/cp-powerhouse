using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using cp_powerhouse;
using cp_powerhouse.Models;
using cp_powerhouse.Models.ListObjects;
using cp_powerhouse.Views;
using System.Net;

namespace cp_powerhouse.Models
{
    public class Core
    {
        GlobalFunctions gf = new GlobalFunctions();

        public ObservableCollection<ContestItems> contestlist = new ObservableCollection<ContestItems>();
        public ObservableCollection<ContestItems> cflist = new ObservableCollection<ContestItems>();
        public ObservableCollection<ContestItems> uvalist = new ObservableCollection<ContestItems>();
        public ObservableCollection<ContestItems> clist = new ObservableCollection<ContestItems>();
        StartScreen ss = new StartScreen();
        public Core()
        {
            StartScreen_Loaded();
        }

        private async void StartScreen_Loaded()
        {
            ss.Show();
            if (gf.IsInternetAvailable())
            {
                await GenerateContestList();
                StartApplication();
            }
            else
            {
                StatusWindow sw = new StatusWindow(0);
                sw.Show();
            }
        }

        public void StartApplication()
        {
            StartMainWindow();
            StartNotification();
            ss.Close();
        }

        public async Task GenerateContestList()
        {
            // SHOW CodeForces Loading
            int gen_cf_status = await GenerateCFList();
            if (gen_cf_status == 0)
            {
                // Show error
            }
            else if (gen_cf_status == 1)
            {
                // Show OK
                if (cflist.Count > 0)
                {
                    foreach (var item in cflist)
                    {
                        contestlist.Add(item);
                    }
                }
                else
                {
                    // No Contest Found!
                }
            }
            else
            {
                // Something Went Wrong!
            }

            // SHOW UVa Loading
            int gen_uva_status = await GenerateUVAList();
            if (gen_uva_status == 0)
            {
                // Show error
            }
            else if (gen_uva_status == 1)
            {
                // Show OK
                if (uvalist.Count > 0)
                {
                    foreach (var item in uvalist)
                    {
                        contestlist.Add(item);
                    }
                }
                else
                {
                    // No Contest Found!
                }
            }
            else
            {
                // Something Went Wrong!
            }

            // SHOW CList Loading
            
            int gen_cl_status = await GenerateCList();
            if (gen_cl_status == 0)
            { 
                // Show error
            }
            else if (gen_cl_status == 1)
            {
                // Show OK
                if (clist.Count > 0)
                {
                    foreach (var item in clist)
                    {
                        contestlist.Add(item);
                    }
                }
                else
                {
                    // No Contest Found!
                }
            }
            else
            {
                // Something Went Wrong!
            }

            if (gen_cf_status == 0 && gen_uva_status == 0 && gen_cl_status == 0)
            {
                StatusWindow sw = new StatusWindow(1);
                sw.Show();
            }
            else
            {
                ObservableCollection<ContestItems> sortedCollection = new ObservableCollection<ContestItems>(contestlist.OrderBy(item => item.startTimeSeconds));
                contestlist.Clear();
                foreach (var item in sortedCollection) contestlist.Add(item);
            }
        }

        private void StartMainWindow()
        {
            MainWindow mw = new MainWindow(contestlist);
            mw.Show();
        }

        private void StartNotification()
        {
            Notification nf = new Notification(contestlist);
        }

        public async Task<int> GenerateCFList()
        {
            var cfbaseUrl = new RestClient("https://codeforces.com/api/contest.list?gym=false");
            var cfrequest = new RestRequest();
            var cfresponse = cfbaseUrl.Execute(cfrequest);

            if (cfresponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RootObjects cfobj = JsonConvert.DeserializeObject<RootObjects>(cfresponse.Content);
                foreach (var item in cfobj.result)
                {
                    if (item.phase == "BEFORE")
                    {
                        cflist.Add(new ContestItems()
                            {
                                id = item.id,
                                name = item.name,
                                platform = "CodeForces",
                                phase = item.phase,
                                durationSeconds = item.durationSeconds,
                                startTimeSeconds = item.startTimeSeconds,
                                startTimeSecondsView = gf.convertTime(item.platform, item.startTimeSeconds),
                                durationSecondsView = gf.convertDuration(item.durationSeconds),
                                link = "https://codeforces.com/contests/" + item.id.ToString()
                    }
                        );
                    }
                }
                await Task.CompletedTask;
                return 1;
            }else
            {
                await Task.CompletedTask;
                return 0;
            }    
        }

        // 
        public async Task<int> GenerateCList()
        {
            string todayiso8601Format = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            var clbaseUrl = new RestClient($"https://clist.by/api/v1/contest/?username=M0R14R7Y&api_key=5549847bdf2663d74dd101ee50c23a427734f0a7&start__gte={todayiso8601Format}&order_by=start&format=json");
            var clrequest = new RestRequest();
            var clresponse = clbaseUrl.Execute(clrequest);

            if (clresponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Root clobj = JsonConvert.DeserializeObject<Root>(clresponse.Content);
                foreach (var item in clobj.objects)
                {
                    DateTime contesttime = DateTime.Parse(item.start);
                    TimeSpan timeDifference = contesttime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    int tempstarttimeseconds = (int)timeDifference.TotalSeconds;
                    string tempphase = "ELAPSED";
                    if(contesttime > DateTime.Now)
                    {
                        tempphase = "BEFORE";
                    }

                    try
                    {
                        clist.Add(new ContestItems()
                        {
                            id = item.id,
                            name = $"[{item.resource.name}] " + item.@event,
                            platform = item.resource.name,
                            phase = tempphase,
                            durationSeconds = item.duration,
                            startTimeSeconds = tempstarttimeseconds,
                            startTimeSecondsView = gf.convertTime("", tempstarttimeseconds),
                            durationSecondsView = gf.convertDuration(item.duration),
                            link = item.href
                        }
                            );
                    }catch (Exception ex) { MessageBox.Show(ex.Message); }
                    
                }
                await Task.CompletedTask;
                return 1;
            }
            else
            {
                await Task.CompletedTask;
                return 0;
            }
        }

        public async Task<int> GenerateUVAList()
        {
            var uvabaseUrl = new RestClient("https://uhunt.onlinejudge.org/api/contests");
            var uvarequest = new RestRequest();
            var uvaresponse = uvabaseUrl.Execute(uvarequest);

            if (uvaresponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<ContestItems> result = JsonConvert.DeserializeObject<List<ContestItems>>(uvaresponse.Content);
                foreach (var item in result)
                {
                    DateTime currentTime = DateTime.UtcNow;
                    DateTime referenceTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    TimeSpan timeDifference = currentTime - referenceTime;
                    double seconds = timeDifference.TotalSeconds;
                    if (item.startTimeSeconds >= seconds)
                    {
                        int st = item.starttime;
                        int dr = (item.endtime - item.starttime);
                        string pl = "UVa";

                        uvalist.Add(new ContestItems()
                        {
                            id = item.id,
                            name = item.name,
                            platform = pl,
                            phase = "BEFORE",
                            durationSeconds = dr,
                            startTimeSeconds = st,
                            startTimeSecondsView = gf.convertTime(pl, st),
                            durationSecondsView = gf.convertDuration(dr),
                            link = "https://onlinejudge.org/index.php?option=com_onlinejudge&Itemid=13&page=show_contest&contest=" + item.id
                        }
                        );
                    }
                }
                await Task.CompletedTask;
                return 1;
            }
            else
            {
                await Task.CompletedTask;
                return 0;
            }
        }
    }
}
