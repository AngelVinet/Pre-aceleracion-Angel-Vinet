using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Models
{
    public class Personaje_PeSe
    {
        [Key]
        public int Id { get; set; }

        public int PersonajeId { get; set; }
        public Personaje Personajes { get; set; }

        public int PeliculaSerieId { get; set; }
        public PeliculaSerie PeliculasSeries { get; set; }
    }
}
