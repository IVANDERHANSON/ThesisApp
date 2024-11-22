namespace ThesisApp.DTO
{
    public class ThesisDefenceCreationDTO
    {
        public int ThesisId { get; set; }
        public int MentorLecturerId { get; set; }
        public int ExaminerLecturerId { get; set; }
        public DateTime Schedule { get; set; }
        public string MeetingLink { get; set; }
    }
}
