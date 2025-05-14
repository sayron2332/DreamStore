using Ardalis.Specification;
using DreamStore.Core.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Specification
{
    public class RoleSpecification
    {
        public class GetByName : Specification<AppRole>
        {

            public GetByName(string name)
            {
                Query.Where(b => b.Name == name);
            }
        }
    }
}
