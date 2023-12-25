using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class FuelManager : IFuelService
    {
        private readonly IFuelDal fuelDal;
        public FuelManager(IFuelDal fuelDal)
        {
            this.fuelDal=fuelDal;
        }

        public void Activity(int id)
        {
            fuelDal.Activity(id);
        }

        public void Add(Fuel fuel)
        {
            fuelDal.Add(fuel);
        }

        public void Delete(int id)
        {
            Fuel fuel = fuelDal.Get(x => x.Id == id);
            fuelDal.Delete(fuel);
        }

        public async Task<List<Fuel>> GetActiveFuelsAsync()
        {
            return await fuelDal.GetActiveFuels();
        }

        public List<Fuel> GetAllFuels()
        {
            return fuelDal.GetAll();
        }

        public Fuel GetFuel(int? id)
        {
            return fuelDal.Get(x => x.Id == id);
        }

        public void Update(Fuel fuel)
        {
            fuelDal.Update(fuel);
        }
    }
}
