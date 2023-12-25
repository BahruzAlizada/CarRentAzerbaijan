

using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IFuelService
    {
        List<Fuel> GetAllFuels();
        Task<List<Fuel>> GetActiveFuelsAsync();
        Fuel GetFuel(int? id);
        void Add(Fuel fuel);
        void Update(Fuel fuel);
        void Delete(int id);
        void Activity(int id);
    }
}
