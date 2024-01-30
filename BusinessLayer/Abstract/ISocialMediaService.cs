

using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
	public interface ISocialMediaService
	{ 
		List<SocialMedia> GetSocialMedias();
		SocialMedia GetSocialMediaByID(int? id);
		List<SocialMedia> GetActiveSocialMedias();
		void Activity(int id);
		void Delete(int id);
		void Add(SocialMedia socialMedia);
		void Update(SocialMedia socialMedia);
	}
}
