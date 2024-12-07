using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IThesisRepository
    {
        ICollection<Thesis> GetTheses();
        Thesis GetThesis(int id);
        bool ThesisExists(int id);
        bool StudentIdExists(int studentId);
        bool CreateThesis(Thesis thesis);
        bool Save();
    }
}
