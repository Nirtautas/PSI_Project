using TeamWebApplication.Models;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamWebApplication.Data
{
    public class UserContainer : IUserContainer
    {
        public string FetchingPath { get; }
        public int loggedInUserId { get; set; } = 0;
        public Role? loggedInUserRole { get; set; } = null;
        public int currentCourseId { get; set; } = 0;
        private int userIdCounter;
        public ICollection<User> userList { get; }

        public UserContainer()
        {
            userList = new List<User>();
        }
    }
}