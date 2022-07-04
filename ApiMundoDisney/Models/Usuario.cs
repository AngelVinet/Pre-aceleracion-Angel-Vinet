using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Models
{
    public class Usuario : IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
