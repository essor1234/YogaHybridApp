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


        // Add to existing DatabaseConnect.cs
        public async Task<Course> GetCourseByIdAsync(string courseId)
        {
            try
            {
                var course = await _firebaseClient
                    .Child("courses")
                    .Child(courseId)
                    .OnceSingleAsync<Course>();
                System.Diagnostics.Debug.WriteLine($"Fetched course: {course?.Category} for CourseId: {courseId}");
                return course;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetCourseByIdAsync: {ex}");
                throw new Exception($"Failed to fetch course: {ex.Message}");
            }
        }

        public async Task<List<ClassInstance>> GetAllClassInstancesAsync()
        {
            try
            {
                var classInstances = await _firebaseClient
                    .Child("class_instances")
                    .OnceAsync<ClassInstance>();
                return classInstances.Select(item => item.Object).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch all class instances: {ex.Message}");
            }
        }


        // Fetch User by UserId (new)
        public async Task<User> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _firebaseClient
                    .Child("users")
                    .Child(userId)
                    .OnceSingleAsync<User>();
                System.Diagnostics.Debug.WriteLine($"Fetched user: {user?.Name} for UserId: {userId}");
                return user;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetUserByIdAsync: {ex}");
                throw new Exception($"Failed to fetch user: {ex.Message}");
            }
        }

        // Update user's ClassesId array (new)
        public async Task UpdateUserClassesAsync(string userId, string[] classesId)
        {
            try
            {
                await _firebaseClient
                    .Child("users")
                    .Child(userId)
                    .Child("ClassesId")
                    .PutAsync(classesId);
                System.Diagnostics.Debug.WriteLine($"Updated ClassesId for UserId: {userId}, Count: {classesId.Length}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateUserClassesAsync: {ex}");
                throw new Exception($"Failed to update user classes: {ex.Message}");
            }
        }

        // Fetch User by Email
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
                System.Diagnostics.Debug.WriteLine($"Error fetching user by email: {ex.Message}");
                return null;
            }
        }

        // Create User
        public async Task<bool> CreateUserAsync(string name, string email)
        {
            try
            {
                var users = await _firebaseClient
                    .Child("users")
                    .OnceAsync<User>();

                if (users.Any(u => u.Object.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                {
                    System.Diagnostics.Debug.WriteLine("Email already exists.");
                    return false;
                }

                string userId = Guid.NewGuid().ToString();
                var user = new User(userId, name, email, new string[] { });

                await _firebaseClient
                    .Child("users")
                    .Child(userId)
                    .PutAsync(user);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating user: {ex.Message}");
                return false;
            }
        }

        // Fetch ClassInstance objects by IDs
        public async Task<List<ClassInstance>> GetClassInstancesByIdsAsync(IEnumerable<string> classInstanceIds)
        {
            try
            {
                var classInstances = await _firebaseClient
                    .Child("class_instances")
                    .OnceAsync<ClassInstance>();

                var filtered = classInstances
                    .Select(item => item.Object)
                    .Where(ci => classInstanceIds.Contains(ci.ClassInstanceId))
                    .ToList();
                System.Diagnostics.Debug.WriteLine($"Fetched {filtered.Count} class instances for IDs: {string.Join(", ", classInstanceIds)}");
                return filtered;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetClassInstancesByIdsAsync: {ex}");
                throw new Exception($"Failed to fetch class instances: {ex.Message}");
            }
        }

        // Fetch ClassInstance objects by CourseId
        public async Task<List<ClassInstance>> GetClassInstancesByCourseIdAsync(string courseId)
        {
            try
            {
                var classInstances = await _firebaseClient
                    .Child("class_instances")
                    .OnceAsync<ClassInstance>();
                var filtered = classInstances
                    .Select(item => item.Object)
                    .Where(ci => ci.CourseId == courseId)
                    .ToList();
                System.Diagnostics.Debug.WriteLine($"Fetched {filtered.Count} class instances for CourseId: {courseId}");
                return filtered;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetClassInstancesByCourseIdAsync: {ex}");
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
                System.Diagnostics.Debug.WriteLine($"Error in GetTeacherByIdAsync: {ex}");
                throw new Exception($"Failed to fetch teacher: {ex.Message}");
            }
        }

        // Other methods
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
                System.Diagnostics.Debug.WriteLine($"Error fetching courses: {ex.Message}");
                return Array.Empty<Course>();
            }
        }

        public async Task<ClassInstance[]> GetClassInstancesByCourseAsync(string courseId)
        {
            try
            {
                var classInstances = await _firebaseClient
                    .Child("class_instances") // Standardized to class_instances
                    .OnceAsync<ClassInstance>();
                var filteredList = classInstances
                    .Select(ci => ci.Object)
                    .Where(ci => ci.CourseId == courseId)
                    .ToList();
                return filteredList.ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching class instances: {ex.Message}");
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
                System.Diagnostics.Debug.WriteLine($"Error fetching teachers: {ex.Message}");
                return Array.Empty<Teacher>();
            }
        }
    }
}