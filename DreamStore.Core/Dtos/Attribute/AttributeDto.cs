﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Dtos.Attribute
{
    public class AttributeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
