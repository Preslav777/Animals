using AnimalsProject.Models;
using System.Data.Entity;

namespace AnimalsProject
{
    public class AnimalsContext : DbContext
    {
        public AnimalsContext() : base("name=Animals")
        {
        }
        public DbSet<Animals> Animals { get; set; }
        public DbSet<Breeds> Breeds { get; set; }
    }
}