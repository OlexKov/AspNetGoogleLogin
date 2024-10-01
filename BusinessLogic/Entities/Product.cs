using System;

namespace BusinessLogic.Entities
{
    public class Product
    {
     
        public long Id { get; set; }
        public string Mame { get; set; } = string.Empty;
        public string Sescription { get; set; } = string.Empty;
        public DateOnly CreationTime { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public Category Category { get; set; }
        public ICollection<CartProduct> InUsersCart { get; set; } = new HashSet<CartProduct>();
        public ICollection<ProductImage> Images =  new HashSet<ProductImage>();
        public ICollection<User> FavoriteInUsers = new HashSet<User>();
    }
}
