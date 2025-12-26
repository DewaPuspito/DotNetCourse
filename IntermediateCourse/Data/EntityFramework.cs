using IntermediateProgram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace IntermediateProgram.Data
{
    public class EntityFrameworkExample : DbContext
    {
        private IConfiguration _config;
        public EntityFrameworkExample(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Computer>? Computer { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
                // .HasNoKey();
                .HasKey(c => c.ComputerId);
                // .ToTable("Computer", "TutorialAppSchema")
                // .ToTable("TableName", "SchemaName")
        }
    }
}