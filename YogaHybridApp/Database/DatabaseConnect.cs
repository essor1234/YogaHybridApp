using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YogaHybridApp.Objects;

namespace YogaHybridApp.Database

{
    public class DatabaseConnect
    {
        FirebaseClient fireBaseClient = new FirebaseClient("https://yoloclassmanagement-default-rtdb.firebaseio.com/");


        public async Task<Teacher[]> LoadTeachersAsync()
        {
            try
            {
                var teachers = await fireBaseClient
                    .Child("teachers")
                    .OnceAsync<Teacher>();

                var teacherList = teachers.Select(t => t.Object).ToList();

                //listCourses.ItemsSource = teacherList;

                return teacherList.ToArray();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show a message to the user, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return Array.Empty<Teacher>();
            }
        }



    }


}
