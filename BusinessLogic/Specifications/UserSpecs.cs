using Ardalis.Specification;
using BusinessLogic.Entities;

namespace BusinessLogic.Specifications
{
    internal class UserSpecs
    {
        public class GetByEmail : Specification<User>
        {
            public GetByEmail(string email) => Query
                .Include(x=>x.Roles)
                .Where(x => x.Email == email)
                .AsNoTracking();
                
        }
    }
}
