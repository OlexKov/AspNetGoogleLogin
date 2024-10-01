using Ardalis.Specification;
using BusinessLogic.Entities;


namespace BusinessLogic.Specifications
{
    internal class UserRolesSpecs
    {
        public class GetByName : Specification<UserRole>
        {
            public GetByName(string name)
            {
                Query.Where(x => x.Name == name);
            }

        }
    }
}
