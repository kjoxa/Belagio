using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace devarts.Models
{
    public class SiteDbContext : DbContext
    {
        public SiteDbContext()
            : base("sitedb")
        {
            // wyłączanie echanizmu migracji
            Database.SetInitializer<SiteDbContext>(null);            
            Configuration.ValidateOnSaveEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // Administracyjne
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Tracing> Tracing { get; set; }

        // Community
        public DbSet<Newsletter> Newsletter { get; set; }
        public DbSet<Subscriber> Subscriber { get; set; }

        // Posty
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Comment> Comments { get; set; }        
        
        // Lokalizacje na mapie
        public DbSet<DogsLocation> DogsLocation { get; set; } // mapa psów

    }
}