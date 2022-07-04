using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.DTO
{
    public class ResponseDto
    {
        public bool IsSucess { get; set; } = true;
        public string DisplayMessage { get; set; }
        public object Result { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
