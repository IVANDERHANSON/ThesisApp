using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IMentorPairRepository
    {
        ICollection<MentorPair> GetMentorPairs();
        MentorPair GetMentorPair(int id);
        bool MentorPairExists(int id);
        User GetStudent(int preThesisId);
        ICollection<User> GetMentorLecturers();
    }
}
