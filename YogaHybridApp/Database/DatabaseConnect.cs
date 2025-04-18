using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaHybridApp.Objects;

namespace YogaHybridApp.Database
{
    public class DatabaseConnect
    {
        private readonly FirebaseClient _firebaseClient;

        public DatabaseConnect()
        {
            _firebaseClient = new FirebaseClient("https://yoloclassmanagement-default-rtdb.firebaseio.com/");
        }

        // Fetch ClassInstance objects by CourseId
        public async Task<List<ClassInstance>> GetClassInstancesByCourseIdAsync(string courseId)
        {
            try
            {
                var classInstances = await _firebaseClient
                    .Child("class_instances")
                    .OnceAsync<ClassInstance>();

                return classInstances
                    .Select(item => item.Object)
                    .Where(ci => ci.CourseId == courseId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch class instances: {ex.Message}");
            }
        }

        // Fetch Teacher by TeacherId
        public async Task<Teacher> GetTeacherByIdAsync(string teacherId)
        {
            try
            {
                var teacher = await _firebaseClient
                    .Child("teachers")
                    .Child(teacherId)
                    .OnceSingleAsync<Teacher>();
                return teacher;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch teacher: {ex.Message}");
            }
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var users = await _firebaseClient
                    .Child("users")
                    .OnceAsync<User>();

                return users.FirstOrDefault(u => u.Object.Email.Equals(email, StringComparison.OrdinalIgnoreCase))?.Object;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by email: {ex.Message}");
                return null;
            }
        }



        public async Task<bool> CreateUserAsync(string name, string email)
        {
            try
            {
                // Check for duplicate email
                var users = await _firebaseClient
                    .Child("users")
                    .OnceAsync<User>();

                if (users.Any(u => u.Object.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Email already exists.");
                    return false;
                }

                // Create new user
                string userId = Guid.NewGuid().ToString();
                var user = new User(userId, name, email, new string[] { });

                // Save to Firebase
                await _firebaseClient
                    .Child("users")
                    .Child(userId)
                    .PutAsync(user);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                return false;
            }
        }

        
        public async Task<Course[]> GetAllCoursesAsync()
        {
            try
            {
                var courses = await _firebaseClient
                    .Child("courses")
                    .OnceAsync<Course>();

                var courseList = courses.Select(c => c.Object).ToList();
                return courseList.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching courses: {ex.Message}");
                return Array.Empty<Course>();
            }
        }

        
        public async Task<ClassInstance[]> GetClassInstancesByCourseAsync(string courseId)
        {
            try
            {
                var classInstances = await _firebaseClient
                    .Child("classInstances")
                    .OnceAsync<ClassInstance>();

                var filteredList = classInstances
                    .Select(ci => ci.Object)
                    .Where(ci => ci.CourseId == courseId)
                    .ToList();

                return filteredList.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching class instances: {ex.Message}");
                return Array.Empty<ClassInstance>();
            }
        }

       
        public async Task<Teacher[]> LoadTeachersAsync()
        {
            try
            {
                var teachers = await _firebaseClient
                    .Child("teachers")
                    .OnceAsync<Teacher>();

                var teacherList = teachers.Select(t => t.Object).ToList();
                return teacherList.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return Array.Empty<Teacher>();
            }
        }


    }
}