using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaHybridApp.Objects
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
		public string[] ClassesId { get; set; }


        public User()
        {
            ClassesId = new string[] { }; // Ensure non-null array for Firebase
        }

        public User(string userId, string name, string email, string[] classesId)
        {
            UserId = userId;
            Name = name;
            Email = email;
            ClassesId = classesId ?? new string[] { };

        }

        public Boolean AddClass(string classId) {
            return true;
        }

        public Boolean RemoveClass(string classId) { return true; }
    


	}
}
