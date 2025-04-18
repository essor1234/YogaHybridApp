using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaHybridApp.Objects
{
    public class User
    {
        private string UserId { get; set; }
        private string Name { get; set; }
        private string Email { get; set; }
		private string[] ClassesId { get; set; }

        public User(string userId, string name, string email, string[] classesId)
        {
            UserId = userId;
            Name = name;
            Email = email;
            ClassesId = classesId;
        }

        public Boolean AddClass(string classId) {
            return true;
        }

        public Boolean RemoveClass(string classId) { return true; }
    


	}
}
