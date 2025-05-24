using Ardalis.Specification;
using DreamStore.Core.Entites;
using DreamStore.Core.Entites.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Specification
{
    public class AttributeSpecification
    {
        public class GetListByPagination : Specification<AppAttribute>
        {
            public GetListByPagination(int skip, int take)
            {
                Query.OrderBy(c => c.Id).Skip(skip).Take(take);
            }
        }
    }
}
