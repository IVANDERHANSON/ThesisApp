using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class MentoringSessionRepository : IMentoringSessionRepository
    {
        private readonly DataContext _dataContext;
        public MentoringSessionRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<MentoringSession> GetMentoringSessions()
        {
            return _dataContext.MentoringSessions.OrderBy(ms => ms.id).ToList();
        }

        public MentoringSession GetMentoringSession(int id)
        {
            return _dataContext.MentoringSessions.Where(ms => ms.id == id).FirstOrDefault();
        }

        public bool MentoringSessionExists(int id)
        {
            return _dataContext.MentoringSessions.Any(ms => ms.id == id);
        }

        public ICollection<MentoringSession> GetMentoringSessionsByMentorPairId(int mentorPairId)
        {
            return _dataContext.MentoringSessions.Where(ms => ms.MentorPairId == mentorPairId).OrderBy(ms => ms.id).ToList();
        }

        public int CountMentoringSessions(int mentorPairId)
        {
            return _dataContext.MentoringSessions.Count(ms => ms.MentorPairId == mentorPairId);
        }
    }
}
