namespace ThesisApp.DTO
{
    public class ThesisDTO
    {
        public int id { get; set; }

        public int StudentId { get; set; }
        public string ThesisName { get; set; }
        public string ThesisLink { get; set; }

        public ThesisDefenceDTO ThesisDefence { get; set; }
    }
}
