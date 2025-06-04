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
    public class ProductSpecification
    {
        public class GetListByPagination : Specification<AppProduct>
        {
            public GetListByPagination(int skip, int take)
            {
                Query.Include(p => p.Category).Include(p => p.Values).OrderBy(c => c.Id).Skip(skip).Take(take);
            }
        }
    }
}
