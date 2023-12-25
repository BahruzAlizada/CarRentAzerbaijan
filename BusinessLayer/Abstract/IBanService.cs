

using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBanService
    {
        List<Ban> GetAllBans();
        Task<List<Ban>> GetActiveBansAsync();
        Ban GetBanById(int? id);
        void Add(Ban ban);
        void Update(Ban ban);
        void Delete(int id);
        void Activity(int id);
    }
}
