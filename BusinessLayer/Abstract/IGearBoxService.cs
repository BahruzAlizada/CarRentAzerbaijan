

using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IGearBoxService
    {
        List<GearBox> GetAllGearBoxes();
        Task<List<GearBox>> GetActiveGearBoxesAsync();
        GearBox GetGearBoxById(int? id);
        void Add(GearBox gearBox);
        void Update(GearBox gearBox);
        void Delete(int id);
        void Activity(int id);
    }
}
