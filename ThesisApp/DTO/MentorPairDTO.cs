namespace ThesisApp.DTO
{
    public class MentorPairDTO
    {
        public int id { get; set; }
        public int PreThesisId { get; set; }
        public int MentorLecturerId { get; set; }

        public ICollection<MentoringSessionDTO> MentoringSessions { get; set; }
    }
}
