namespace ThesisApp.Models
{
    public class MentoringSession
    {
        public int id { get; set; }
        public int MentorPairId { get; set; }
        public DateTime Schedule {  get; set; }
        public string MeetingLink { get; set; }

        public MentorPair MentorPair { get; set; }
    }
}
