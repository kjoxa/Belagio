using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace devarts.Models
{
    public class KennelDbContext : DbContext
    {
        public KennelDbContext()
            : base("kenneldb")
        {
            // wyłączanie echanizmu migracji
            Database.SetInitializer<SiteDbContext>(null);
            Configuration.ValidateOnSaveEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // PSY W HODOWLI
        public DbSet<DogBreed> DogBreeds { get; set; }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<DogImages> DogImages { get; set; }

        // MIOTY I PSY W MIOTACH
        public DbSet<Litter> Litters { get; set; }
        public DbSet<LitterImages> LitterImages { get; set; }

        public DbSet<Puppy> Puppies { get; set; }
        public DbSet<PuppyImages> PuppyImages { get; set; }

        // DRZEWA GENEALOGICZNE
        public DbSet<Tree> Trees { get; set; }

        // ASYSTENT HODOWLI
        public DbSet<Document> Documents { get; set; }
        public DbSet<Reproduction> Reproduction { get; set; }
        public DbSet<PuppieOfBitch> PuppieOfBitch { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<HealthAndVaccinations> HealthAndVaccinations { get; set; }

        // FINANSE
        public DbSet<Finance> Finances { get; set; }
        public DbSet<FinanceCathegoryName> FinanceCathegoryNames { get; set; }
        public DbSet<FinanceFavouritesName> FinanceFavouritesNames { get; set; }
    }
}