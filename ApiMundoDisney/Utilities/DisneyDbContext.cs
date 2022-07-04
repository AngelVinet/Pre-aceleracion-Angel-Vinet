using ApiMundoDisney.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Utilities
{
    public class DisneyDbContext : DbContext
    {
        public DisneyDbContext(DbContextOptions<DisneyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personaje_PeSe>()
                .HasOne(p => p.Personajes)
                .WithMany(pe => pe.PersonajesPeliculasSeries)
                .HasForeignKey(pel => pel.PersonajeId);

            modelBuilder.Entity<Personaje_PeSe>()
                .HasOne(p => p.PeliculasSeries)
                .WithMany(pe => pe.PersonajesPeliculasSeries)
                .HasForeignKey(pel => pel.PeliculaSerieId);
        }

        //Se agrega como propiedad de DbSet las tablas que se requieran agregar por migración a la BD.
        //Para agregar una Tabla se puede seguir como ejemplo el primer atributo DbSet abajo
        //Donde se debe agregar un atributo DbSet reemplazando la palabra "Personaje"
        //por el nombre de la clase que se realizo, Con los atributos que tendrá la tabla en la BD,
        //asi mismo se debe reemplazar "Personajes", por alguna palabra alusiva a la tabla que se desea crear.
        public DbSet<Personaje> Personajes { get; set; }
        public DbSet<PeliculaSerie> PeliculasSeries { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Personaje_PeSe> Personajes_PeSe { get; set; }
    }
}
