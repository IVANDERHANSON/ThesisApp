using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IThesisRepository
    {
        ICollection<Thesis> GetTheses();
        Thesis GetThesis(int id);
        bool ThesisExists(int id);
    }
}
