
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Concrete
{
	public class SocialMediaManager : ISocialMediaService
	{
		private readonly ISocialMediaDal socialMediaDal;
		private readonly IMemoryCache memoryCache;
		public SocialMediaManager(ISocialMediaDal socialMediaDal, IMemoryCache memoryCache)
		{
			this.socialMediaDal = socialMediaDal;
			this.memoryCache = memoryCache;
		}


		public void Activity(int id)
		{
			socialMediaDal.Activity(id);
		}

		public void Add(SocialMedia socialMedia)
		{
			socialMediaDal.Add(socialMedia);
		}

		public void Delete(int id)
		{
			var socialMedia = socialMediaDal.Get(x => x.Id == id);
			socialMediaDal.Delete(socialMedia);
		}

		public List<SocialMedia> GetActiveSocialMedias()
		{
			const string scl = "scmedia";
			List<SocialMedia> socialMedias;

			if (!memoryCache.TryGetValue(scl, out socialMedias))
			{
				socialMedias = socialMediaDal.GetActiveSocialMedias();

				memoryCache.Set(scl, socialMedias, new MemoryCacheEntryOptions
				{
					SlidingExpiration = TimeSpan.FromMinutes(5),
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15),
					Priority = CacheItemPriority.Normal
				});
			}

			return socialMediaDal.GetActiveSocialMedias();
		}

		public SocialMedia GetSocialMediaByID(int? id)
		{
			return socialMediaDal.Get(x => x.Id == id);
		}

		public List<SocialMedia> GetSocialMedias()
		{
			return socialMediaDal.GetAll();
		}

		public void Update(SocialMedia socialMedia)
		{
			socialMediaDal.Update(socialMedia);
		}
	}
}
