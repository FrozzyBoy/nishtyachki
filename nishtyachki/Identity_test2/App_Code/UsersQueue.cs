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

       
        public static void AddUser(string id)
        {
            User user = new User(id);
            _queue.Add(user);
        }
       
        public static void DeleteFromTheQueue(string id)
        {
            _queue.RemoveAll(m => m.ID == id);
        }
       
        public static void ChangeRole(string id, Role needed_role)
        {
            foreach (User user in _queue)
            {
                if (user.ID == id)
                {
                    user.Role = needed_role;
                }
            }
        }

    }
}