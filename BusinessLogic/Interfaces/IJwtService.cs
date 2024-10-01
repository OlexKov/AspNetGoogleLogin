using BusinessLogic.Entities;
using System.Security.Claims;


namespace BusinessLogic.Interfaces
{
    public interface IJwtService
    { 
        string CreateToken(User user);
    }
}
