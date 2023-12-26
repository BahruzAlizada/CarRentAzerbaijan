

namespace EntityLayer.Dtos
{
    public class ModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsDeactive { get; set; }
    }
}
