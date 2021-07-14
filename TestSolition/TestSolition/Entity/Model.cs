using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestSolition.Entity
{
    internal class Model : DbContext
    {
        public DbSet<sample01> sample01s { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(GetConnectionString());
        }

        private string GetConnectionString()
        {
            String DB_SERVER = "localhost";
            String DB_PORT = "5432";
            String DB_CATAROG = "postgres";
            String DB_USER = "postgres";
            String DB_PASSWORD = "ac-12457";
            var sb = new StringBuilder();
            sb.Append($"Server={DB_SERVER};")
            .Append($"Port={DB_PORT};")
            .Append($"Database={DB_CATAROG};")
            .Append($"User Id={DB_USER};")
            .Append($"Password={DB_PASSWORD};ApplicationName=TAS-CL;Enlist=True;");
            return sb.ToString();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<sample01>().HasKey(r => r.name);

            modelBuilder.Entity<sample01>().Property(e => e.body).HasComputedColumnSql(@"""name"" || ' ' || ""name""");

            modelBuilder.Entity<sample01>().Property(b => b.updated_time).HasDefaultValueSql("CURRENT_TIMESTAMP");

            base.OnModelCreating(modelBuilder);
        }
    }
}
