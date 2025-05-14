using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Entites.Product
{
    public class AppProductAttribute
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public AppProduct Product { get; set; } = null!;
        public int AttributeId { get; set; }
        public AppAttribute Attribute { get; set; } = null!;
        public string Value { get; set; } = string.Empty;
    }
}
