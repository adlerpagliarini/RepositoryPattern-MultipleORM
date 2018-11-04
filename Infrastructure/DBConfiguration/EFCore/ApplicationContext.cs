using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBConfiguration.EFCore
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /* A way to create Datbasecontext inside the class
        private IConfigurationRoot _config => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
                //dbContextOptionsBuilder.UseInMemoryDatabase("ToDoListMemory");
            }
        }
        */
    }
}
