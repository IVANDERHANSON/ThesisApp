namespace ThesisApp.DTO
{
    public class ThesisDefenceDTO
    {
        public int id { get; set; }
        public int ThesisId { get; set; }
        public int MentorLecturerId { get; set; }
        public int ExaminerLecturerId { get; set; }
        public DateTime Schedule { get; set; }
        public string MeetingLink { get; set; }
    }
}
