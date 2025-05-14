using DreamStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Entites.Product
{
    public class AppProduct : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public AppCategory Category { get; set; } = null!;
        public ICollection<AppProductAttribute> Values { get; set; } = null!;
    }
}
