using Ardalis.Specification;
using DreamStore.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Specification
{
    public class UserSpecification
    {
        public class FindByEmail : Specification<AppUser>
        {
            public FindByEmail(string email)
            {
                Query.Where(b => b.Email == email);
            }
        }
    }
}
