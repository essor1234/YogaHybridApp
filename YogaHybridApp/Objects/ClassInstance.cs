namespace YogaHybridApp.Objects;

public class ClassInstance
{
    public string ClassInstanceId { get; set; }
    public string CourseId { get; set; }
    public DateData Date { get; set; }
    public string TeacherId { get; set; }
    public string Comment { get; set; }

    // Computed property to convert DateData to DateTime using Unix timestamp
    public DateTime DateTime
    {
        get
        {
            if (Date == null || Date.Time == 0)
                return DateTime.MinValue;

            try
            {
                // Convert Unix timestamp (milliseconds) to DateTime
                DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime utcDateTime = unixEpoch.AddMilliseconds(Date.Time);

                // Adjust for timezoneOffset (in minutes, negative means ahead of UTC)
                return utcDateTime.AddMinutes(-Date.TimezoneOffset);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error converting DateTime: {ex.Message}");
                return DateTime.MinValue;
            }
        }
    }

    public ClassInstance() { }

    public ClassInstance(string classInstanceId, string courseId, DateData date, string teacherId, string comment)
    {
        ClassInstanceId = classInstanceId;
        CourseId = courseId;
        Date = date;
        TeacherId = teacherId;
        Comment = comment;
    }
}

public class DateData
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    public long Time { get; set; }
    public int TimezoneOffset { get; set; }
}