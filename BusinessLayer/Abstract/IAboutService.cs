

using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
	public interface IAboutService
	{
		About GetAbout();
		About GetAboutByID(int? id);
		void Update(About about);
	}
}
