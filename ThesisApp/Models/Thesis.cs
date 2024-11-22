namespace ThesisApp.Models
{
    public class Thesis
    {
        public int id { get; set; }

        public int StudentId { get; set; }
        public string ThesisName { get; set; }
        public string ThesisLink { get; set; }

        public User Student { get; set; }
        public ThesisDefence ThesisDefence { get; set; }
    }
}
