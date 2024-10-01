
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Entities
{
    public class UserRole
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User> Users = new HashSet<User> ();
    }
}
