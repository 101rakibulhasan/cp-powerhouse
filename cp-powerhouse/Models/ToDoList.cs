using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using cp_powerhouse.Models;
using cp_powerhouse.Models.ListObjects;

namespace cp_powerhouse
{
    internal class ToDoList
    {
        GlobalFunctions gf = new GlobalFunctions();

        ObservableCollection<ToDoListItems> todolist = new ObservableCollection<ToDoListItems>();

        public ObservableCollection<ToDoListItems> GetToDoList()
        {
            return todolist;
        }

        public void SetToDoList(ObservableCollection<ToDoListItems> c)
        {
            todolist = c;
        }

        public void AddProblemItem(ToDoListItems c)
        {
            GetToDoList().Add(c);
        }

        public void RemoveProblemItem(int itempos)
        {
            GetToDoList().RemoveAt(itempos);
        }
    }
}
