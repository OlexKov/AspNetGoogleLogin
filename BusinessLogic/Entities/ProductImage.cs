
namespace BusinessLogic.Entities
{
    public class ProductImage
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDelete { get; set; }
        public Product Product { get; set; }
    }
}
