using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Dtos.Product
{
    public class CreateUpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Photo { get; set; } = null!;
        public int CategoryId { get; set; } 
        public double Price { get; set; }
    
    }
}
