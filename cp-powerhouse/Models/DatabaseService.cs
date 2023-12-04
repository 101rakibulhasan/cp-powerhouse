using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using cp_powerhouse.Models.ListObjects;
using MySqlX.XDevAPI.Common;
using cp_powerhouse.Models;
using System.Security.Policy;

namespace cp_powerhouse
{
    public class DatabaseService
    {
        MySqlConnection conn;
        string server_url = "127.0.0.1";
        string server_db = "db_cppowerhouse";
        string problemset_table = "problemset";
        string friends_table = "friends";
        string favcontest_table = "favourite_contest";
        public void StartConnection()
        {
            string connStr = $"server={server_url};user=root;database={server_db};password=\"\"";
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
            finally
            {
                conn.Close();
            }
        }
        public void AddDataProblemset(string platform, string name, string url)
        {
            conn.Open();
            try
            {
                string command = "INSERT INTO "+ problemset_table + " VALUES(@platform, @name, @url);";
                MySqlCommand sql_cmd = new MySqlCommand(command, conn);

                sql_cmd.Parameters.AddWithValue("@name", name);
                sql_cmd.Parameters.AddWithValue("@platform", platform);
                sql_cmd.Parameters.AddWithValue("@url", url);

                sql_cmd.ExecuteNonQuery();

                conn.Close();
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        public ObservableCollection<ToDoListItems> FetchDataProblemset()
        {
            conn.Open();
            ObservableCollection < ToDoListItems > result = new ObservableCollection<ToDoListItems>(); ;
            
            string query = "SELECT * FROM "+ problemset_table +" ;";
            using (MySqlCommand command = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ToDoListItems tdli = new ToDoListItems();
                        tdli.platform = reader.GetString(reader.GetOrdinal("platform"));
                        tdli.name = reader.GetString(reader.GetOrdinal("name"));
                        tdli.link = reader.GetString(reader.GetOrdinal("url"));

                        result.Add(tdli);
                    }
                }
            }
            conn.Close();

            return result;
        }

        public void RemoveDataProblemset(string platform, string name, string url)
        {
            conn.Open();

            string query = "DELETE FROM " + problemset_table + " WHERE name = @name AND url = @url";
            MySqlCommand command = new MySqlCommand(query, conn);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@platform", platform);
            command.Parameters.AddWithValue("@url", url);

            command.ExecuteNonQuery();

            conn.Close();
        }

        public void AddDataFriends(string platform, string name, string url)
        {
            conn.Open();
            try
            { 
                string command = "INSERT INTO "+friends_table+" VALUES(@platform,@name, @url);";
                MySqlCommand sql_cmd = new MySqlCommand(command, conn);
                sql_cmd.Parameters.AddWithValue("@name", name);
                sql_cmd.Parameters.AddWithValue("@platform", platform);
                sql_cmd.Parameters.AddWithValue("@url", url);
                sql_cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public ObservableCollection<FriendItem> FetchDataFriends()
        {
            conn.Open();
            ObservableCollection<FriendItem> result = new ObservableCollection<FriendItem>(); ;

            string query = $"SELECT * FROM {friends_table}";
            using (MySqlCommand command = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FriendItem frnd = new FriendItem();
                        
                        frnd.Platform = reader.GetString(reader.GetOrdinal("platform"));
                        frnd.Username = reader.GetString(reader.GetOrdinal("username"));
                        frnd.ProfileUrl = reader.GetString(reader.GetOrdinal("url"));                                                           // Add more columns as needed

                        result.Add(frnd);
                    }
                }
            }
            conn.Close();

            return result;
        }

        public void RemoveDataFriends(string platform, string name, string url)
        {
            conn.Open();

            string query = "delete from "+friends_table+" where username = @name and platform = @platform and url = @url;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@platform", platform);
            command.Parameters.AddWithValue("@url", url);
            command.ExecuteNonQuery();


            conn.Close();
        }

        public void AddDataFavContest(int id, string name, int startTimeSeconds, int durationSeconds, string phase, string platform,string startTimeSecondsView, string durationSecondsView, string link)
        {
            conn.Open();
            try
            {
                string command = "INSERT INTO "+favcontest_table+" VALUES(@id, @name, @startTimeSeconds, @durationSeconds, @phase, @platform, @startTimeSecondsView, @durationSecondsView, @link);";
                MySqlCommand sql_cmd = new MySqlCommand(command, conn);

                sql_cmd.Parameters.AddWithValue("@id", id);
                sql_cmd.Parameters.AddWithValue("@name", name);
                sql_cmd.Parameters.AddWithValue("@startTimeSeconds", startTimeSeconds);
                sql_cmd.Parameters.AddWithValue("@durationSeconds", durationSeconds);
                sql_cmd.Parameters.AddWithValue("@phase", phase);
                sql_cmd.Parameters.AddWithValue("@platform", platform);
                sql_cmd.Parameters.AddWithValue("@startTimeSecondsView", startTimeSecondsView);
                sql_cmd.Parameters.AddWithValue("@durationSecondsView", durationSecondsView);
                sql_cmd.Parameters.AddWithValue("@link", link);

                sql_cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public ObservableCollection<ContestItems> FetchDataFavContest()
        {
            conn.Open();
            ObservableCollection<ContestItems> result = new ObservableCollection<ContestItems>(); ;

            string query = $"SELECT * FROM {favcontest_table}";
            using (MySqlCommand command = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContestItems citem = new ContestItems();
                        citem.id = reader.GetInt32(reader.GetOrdinal("id"));
                        citem.name = reader.GetString(reader.GetOrdinal("name"));
                        citem.startTimeSeconds = reader.GetInt32(reader.GetOrdinal("startTimeSeconds"));
                        citem.durationSeconds = reader.GetInt32(reader.GetOrdinal("durationSeconds"));
                        citem.phase = reader.GetString(reader.GetOrdinal("phase"));
                        citem.platform = reader.GetString(reader.GetOrdinal("platform"));
                        citem.startTimeSecondsView = reader.GetString(reader.GetOrdinal("startTimeSecondsView"));
                        citem.durationSecondsView = reader.GetString(reader.GetOrdinal("durationSecondsView"));
                        citem.link = reader.GetString(reader.GetOrdinal("link"));

                        result.Add(citem);
                    }
                }
            }
            conn.Close();

            return result;
        }

        public void RemoveDataFavContest(string name, int startTimeSeconds)
        {
            conn.Open();

            string query = $"delete from " + favcontest_table +" where name = @name and startTimeSeconds = @startTimeSeconds;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@startTimeSeconds", startTimeSeconds);
            command.ExecuteNonQuery();

            conn.Close();
        }

    }

    
}
