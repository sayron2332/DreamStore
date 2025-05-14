using DreamStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Entites.Product
{
    public class AppAttribute : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public ICollection<AppProductAttribute> Values { get; set; } = null!;
    }
}
