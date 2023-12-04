using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cp_powerhouse.Models.ListObjects;

namespace cp_powerhouse.Models
{
    public class Friends
    {

        ObservableCollection<FriendItem> friendList = new ObservableCollection<FriendItem>();

        public ObservableCollection<FriendItem> GetFriendList()
        {
            return friendList;
        }

        public void SetFriendList(ObservableCollection<FriendItem> c)
        {
            friendList = c;
        }
        public string GetProfileLink(string Platform,string Username)
        {
            switch (Platform)
            {
                case "Codeforces":
                    return $"https://codeforces.com/profile/{Username}";
                case "Hackerrank":
                    return $"https://www.hackerrank.com/{Username}";
                case "UVA":
                    return $"https://uhunt.onlinejudge.org/id/{Username}";
                case "CodeChef":
                    return $"https://www.codechef.com/users/{Username}";
                case "Light OJ":
                    return $"http://lightoj.com/user/{Username}";
                default:
                    return string.Empty;
            }
        }

        public void AddItemFriend(FriendItem f)
        {
            friendList.Add(f);
        }
    }

}
