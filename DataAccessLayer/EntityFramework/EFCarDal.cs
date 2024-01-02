using Core.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;


namespace DataAccessLayer.EntityFramework
{
    public class EFCarDal : EfRepositoryBase<Car,Context>,ICarDal
    {
    }
}
