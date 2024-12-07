using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IMentorPairRepository
    {
        ICollection<MentorPair> GetMentorPairs();
        MentorPair GetMentorPair(int id);
        bool MentorPairExists(int id);
        bool PreThesisIdExists(int preThesisId);
        User GetStudent(int preThesisId);
        ICollection<User> GetMentorLecturers();
        bool MentorLecturerIdExists(int mentorLecturerId);
        bool CreateMentorPair(MentorPair mentorPair);
        bool Save();
        User GetStudentForEditMentorPair(int studentId);
        bool UpdateMentorPair(MentorPair oldMentorPair, MentorPair mentorPair);
        bool SameMentorLecturerId(MentorPair oldMentorPair, MentorPair mentorPair);
        MentorPair GetMentorPairByPreThesisId(int preThesisId);
    }
}
