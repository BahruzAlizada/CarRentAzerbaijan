using CoreLayer.Entity;

namespace EntityLayer.Concrete
{
	public class About : IEntity
	{
		public int Id { get; set; }
		public string Description { get; set; }
	}
}
