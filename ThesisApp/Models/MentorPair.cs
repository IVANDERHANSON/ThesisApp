namespace ThesisApp.Models
{
    public class MentorPair
    {
        public int id { get; set; }
        public int PreThesisId { get; set; }
        public int MentorLecturerId { get; set; }

        public PreThesis PreThesis { get; set; }
        public User MentorLecturer { get; set; }
        public ICollection<MentoringSession> MentoringSessions { get; set; }
    }
}
