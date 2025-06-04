using DreamStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DreamStore.Core.Entites.Product
{
    public class AppProduct : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string ImageName { get; set; } = "default.png";
        public AppCategory Category { get; set; } = null!;
        public ICollection<AppProductAttribute> Values { get; set; } = null!;

    }

}
