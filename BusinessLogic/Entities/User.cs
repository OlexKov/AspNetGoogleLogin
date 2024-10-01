
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Entities
{
    public class User 
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; } = new DateTime();
        public string Image { get; set; } = string.Empty;
        public bool IsAccountNonExpired { get; set; }
        public bool IsAccountNonLocked { get; set; }
        public bool IsCredentialsNonExpired { get; set; }
        public bool IsEnabled { get; set; }
        public ICollection<CartProduct> Cart { get; set; } = new HashSet<CartProduct> ();
        public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();
        public ICollection<Product> FavoriteProducts { get; set; } = new HashSet<Product>();
     
    }
}
