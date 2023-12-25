using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IGearBoxDal : IRepositoryBase<GearBox>
    {
        void Activity(int id);
        Task<List<GearBox>> GetActiveGearBoxes();
    }
}
