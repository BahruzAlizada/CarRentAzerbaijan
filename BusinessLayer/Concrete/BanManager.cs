
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class BanManager : IBanService
    {
        private readonly IBanDal banDal;
        public BanManager(IBanDal banDal)
        {
            this.banDal = banDal;
        }


        public void Activity(int id)
        {
            banDal.Activity(id);
        }

        public void Add(Ban ban)
        {
            banDal.Add(ban);
        }

        public void Delete(int id)
        {
            Ban ban = banDal.Get(x => x.Id == id);
            banDal.Delete(ban);
        }

        public async Task<List<Ban>> GetActiveBansAsync()
        {
            return await banDal.GetActiveBans();
        }

        public List<Ban> GetAllBans()
        {
            return banDal.GetAll();
        }

        public Ban GetBanById(int? id)
        {
            return banDal.Get(x => x.Id == id);
        }

        public void Update(Ban ban)
        {
            banDal.Update(ban);
        }
    }
}
