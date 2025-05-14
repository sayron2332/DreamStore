using DreamStore.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Entites
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string PasswordHash { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string ImageName { get; set; } = "default.png";
        public string? PhoneNumber { get; set; } = string.Empty;
        public AppRole Role { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
