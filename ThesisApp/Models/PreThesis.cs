namespace ThesisApp.Models
{
    public class PreThesis
    {
        public int id { get; set; }
        public int StudentId { get; set; }
        public string PreThesisName { get; set; }
        public string PreThesisLink { get; set; }

        public User Student { get; set; }
        public MentorPair MentorPair { get; set; }
    }
}
