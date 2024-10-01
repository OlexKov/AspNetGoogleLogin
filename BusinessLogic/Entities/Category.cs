
namespace BusinessLogic.Entities
{
    public class Category
    {
        public long Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
