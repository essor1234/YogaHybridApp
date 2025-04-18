namespace YogaHybridApp.Objects
{
    public class Teacher
    {
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string Expertise { get; set; }
        public string Email { get; set; }

        // Required for Firebase deserialization
        public Teacher() { }

        public Teacher(string teacherId, string name, string expertise, string email)
        {
            TeacherId = teacherId;
            Name = name;
            Expertise = expertise;
            Email = email;
        }
    }
}
