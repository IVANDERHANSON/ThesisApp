using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IThesisDefenceRepository
    {
        ICollection<ThesisDefence> GetThesisDefences();
        ThesisDefence GetThesisDefence(int id);
        bool ThesisDefenceExists(int id);
        bool ThesisIdExists(int ThesisId);
        bool MentorLecturerIdExists(int MentorLecturerId);
        bool ExaminerLecturerIdExists(int ExaminerLecturerId);
        bool CreateThesisDefence(ThesisDefence thesisDefence);
        bool Save();
        User GetStudent(int thesisId);
        ICollection<User> GetExaminerLecturers(int mentorLecturerId);
    }
}
