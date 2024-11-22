using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class ThesisRepository : IThesisRepository
    {
        private readonly DataContext _dataContext;
        public ThesisRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Thesis> GetTheses()
        {
            return _dataContext.Theses.OrderBy(t => t.id).ToList();
        }

        public Thesis GetThesis(int id)
        {
            return _dataContext.Theses.Where(t => t.id == id).FirstOrDefault();
        }

        public bool ThesisExists(int id)
        {
            return _dataContext.Theses.Any(t => t.id == id);
        }
    }
}
