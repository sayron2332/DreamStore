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
    public class CategorySpecification
    {
        public class GetListByPagination : Specification<AppCategory>
        {
            public GetListByPagination(int skip, int take)
            {
                Query.OrderBy(c => c.Id).Skip(skip).Take(take);
            }
        }
        public class GetByName : Specification<AppCategory>
        {
            public GetByName(string name)
            {
                Query.Where(c => c.Name == name);
            }
        }
    }
}
