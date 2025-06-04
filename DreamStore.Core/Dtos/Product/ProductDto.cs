using DreamStore.Core.Dtos.Attribute;
using DreamStore.Core.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DreamStore.Core.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Slug => GenerateSlug(Name);
        public ICollection<AttributeDto> Attribute { get; set; } = null!;
        public CategoryDto Category { get; set; } = null!;
        private string GenerateSlug(string name)
        {
            name = name.ToLower().Replace(" ", "-");
            name = Regex.Replace(name, @"[^a-z0-9\-]", "");
            return name;
        }
    }
}
