namespace ThesisApp.Models
{
    public class ThesisDefence
    {
        public int id { get; set; }
        public int ThesisId { get; set; }
        public int MentorLecturerId { get; set; }
        public int ExaminerLecturerId { get; set; }
        public DateTime Schedule { get; set; }
        public string MeetingLink { get; set; }

        public Thesis Thesis { get; set; }
        public User MentorLecturer { get; set; }
        public User ExaminerLecturer { get; set; }
    }
}
