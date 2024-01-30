using CoreLayer.Entity;

namespace EntityLayer.Concrete
{
	public class SocialMedia : IEntity
	{
		public int Id { get; set; }
		public string Link { get; set; }
		public string Icon { get; set; }
		public bool IsDeactive { get; set; }
	}
}
