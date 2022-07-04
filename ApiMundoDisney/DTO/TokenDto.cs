using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
