using ThesisApp.Models;

namespace ThesisApp.Interfaces
{
    public interface IPreThesisRepository
    {
        ICollection<PreThesis> GetPreTheses();
        PreThesis GetPreThesis(int id);
        bool PreThesisExists(int id);
    }
}
