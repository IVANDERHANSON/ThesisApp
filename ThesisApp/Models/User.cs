using System.Text.Json.Serialization;

namespace ThesisApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        public PreThesis PreThesis { get; set; }
        public MentorPair MentorPair { get; set; }
        [JsonIgnore]
        public Thesis Thesis { get; set; }
        public ThesisDefence ThesisDefenceForMentor { get; set; }
        public ThesisDefence ThesisDefenceForExaminer { get; set; }
    }
}
