using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Models
{
    public interface IHaveId
    {
        [Key]
        public int Id { get; set; }
    }
}
