using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class MentorPairRepository : IMentorPairRepository
    {
        private readonly DataContext _dataContext;
        public MentorPairRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<MentorPair> GetMentorPairs()
        {
            return _dataContext.MentorPairs.OrderBy(mp => mp.id).ToList();
        }

        public MentorPair GetMentorPair(int id)
        {
            return _dataContext.MentorPairs.Where(mp => mp.id == id).FirstOrDefault();
        }

        public bool MentorPairExists(int id)
        {
            return _dataContext.MentorPairs.Any(mp => mp.id == id);
        }
    }
}
