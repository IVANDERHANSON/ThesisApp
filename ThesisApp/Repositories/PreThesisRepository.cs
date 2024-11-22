using ThesisApp.Data;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Repositories
{
    public class PreThesisRepository : IPreThesisRepository
    {
        private readonly DataContext _dataContext;
        public PreThesisRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<PreThesis> GetPreTheses()
        {
            return _dataContext.PreTheses.OrderBy(pt => pt.id).ToList();
        }

        public PreThesis GetPreThesis(int id)
        {
            return _dataContext.PreTheses.Where(pt => pt.id == id).FirstOrDefault();
        }

        public bool PreThesisExists(int id)
        {
            return _dataContext.PreTheses.Any(pt => pt.id == id);
        }
    }
}
