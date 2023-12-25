

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class GearBoxManager : IGearBoxService
    {
        private readonly IGearBoxDal gearBoxDal;
        public GearBoxManager(IGearBoxDal gearBoxDal)
        {
            this.gearBoxDal = gearBoxDal;
        }


        public void Activity(int id)
        {
            gearBoxDal.Activity(id);
        }

        public void Add(GearBox gearBox)
        {
            gearBoxDal.Add(gearBox);
        }

        public void Delete(int id)
        {
            GearBox gearBox = gearBoxDal.Get(x => x.Id == id);
            gearBoxDal.Delete(gearBox);
        }

        public async Task<List<GearBox>> GetActiveGearBoxesAsync()
        {
            return await gearBoxDal.GetActiveGearBoxes();
        }

        public List<GearBox> GetAllGearBoxes()
        {
            return gearBoxDal.GetAll();
        }

        public GearBox GetGearBoxById(int? id)
        {
            return gearBoxDal.Get(x => x.Id == id);
        }

        public void Update(GearBox gearBox)
        {
            gearBoxDal.Update(gearBox);
        }
    }
}
