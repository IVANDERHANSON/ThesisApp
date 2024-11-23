namespace ThesisApp.DTO
{
    public class MentoringSessionDTO
    {
        public int id { get; set; }
        public int MentorPairId { get; set; }
        public DateTime Schedule { get; set; }
        public string MeetingLink { get; set; }
    }
}
