using Core.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
	public interface ISocialMediaDal : IRepositoryBase<SocialMedia>
	{
		List<SocialMedia> GetActiveSocialMedias();
		void Activity(int id);
	}
}
