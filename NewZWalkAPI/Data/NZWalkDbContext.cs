using Microsoft.EntityFrameworkCore;
using NewZWalkAPI.Models.Domain;

namespace NewZWalkAPI.Data
{
    public class NZWalkDbContext : DbContext
    {
        // Construisons un constructeur 

        public NZWalkDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
           
        }

        // Je vais créer DB Set qui est une proprieté de DBContext
        // Dont elle répresente les collections des entitées dans la BDD
        // Comme on a dans notre Domain Models 3 entités
        // Nous allons répresenter trois collections

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


    }
}
