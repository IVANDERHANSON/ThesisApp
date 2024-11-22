using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IMentoringSessionRepository
    {
        ICollection<MentoringSession> GetMentoringSessions();
        MentoringSession GetMentoringSession(int id);
        bool MentoringSessionExists(int id);
    }
}
