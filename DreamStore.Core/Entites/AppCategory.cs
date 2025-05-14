using DreamStore.Core.Entites.Product;
using DreamStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Entites
{
    public class AppCategory : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<AppProduct> Products { get; set; } = null!;
    }
}
