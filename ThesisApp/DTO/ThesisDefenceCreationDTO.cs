using System.ComponentModel.DataAnnotations;

namespace ThesisApp.DTO
{
    public class ThesisDefenceCreationDTO
    {
        public int ThesisId { get; set; }
        public int MentorLecturerId { get; set; }
        public int ExaminerLecturerId { get; set; }
        public DateTime Schedule { get; set; }

        [RegularExpression(@"^https://.*", ErrorMessage = "The Meeting Link must start with 'https://'.")]
        public string MeetingLink { get; set; }
    }
}
