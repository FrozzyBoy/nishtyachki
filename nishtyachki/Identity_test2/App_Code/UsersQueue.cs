using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminApp.Services;

namespace AdminApp.App_Code
{
    public static class UsersQueue
    {
        static List<User> _queue= new List<User>();

        //метод добавления в очередь
        public static void AddUser(string id)
        {
            User user = new User(id);
            _queue.Add(user);
        }
        //метод удаления из очереди
        public static void DeleteFromTheQueue(string id)
        {
            _queue.RemoveAll(m => m._ID == id);
        }
        //метод изменения роли пользователя
        public static void ChangeRole(string id, Role needed_role)
        {
            foreach (User user in _queue)
            {
                if (user._ID == id)
                {
                    user._role = needed_role;
                }
            }
        }

    }
}