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

namespace cp_powerhouse
{
    internal class Contests
    {
        GlobalFunctions gf = new GlobalFunctions();

        ObservableCollection<ContestItems> favitem = new ObservableCollection<ContestItems>();

        public ObservableCollection<ContestItems> FavouriteContestList()
        {
            return favitem;
        }

        public void SetFavouriteContestList(ObservableCollection<ContestItems> c)
        {
            favitem = c;
        }
        public void AddFavouriteItem(ContestItems c)
        {
            FavouriteContestList().Add(c);
            /*FavouriteContestList().Add(new ContestItems()
            {
                id = ContestList()[itempos].id,
                name = ContestList()[itempos].name,
                platform = ContestList()[itempos].platform,
                phase = ContestList()[itempos].phase,
                durationSeconds = ContestList()[itempos].durationSeconds,
                startTimeSeconds = ContestList()[itempos].startTimeSeconds,
                startTimeSecondsView = ContestList()[itempos].startTimeSecondsView,
                durationSecondsView = ContestList()[itempos].durationSecondsView,
            }
            );*/
        }

        public void RemoveFavouriteItem(int itempos)
        {
            FavouriteContestList().RemoveAt(itempos);
        }
    }
}
