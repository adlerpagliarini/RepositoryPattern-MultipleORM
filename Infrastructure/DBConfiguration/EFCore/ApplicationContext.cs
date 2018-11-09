using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Infrastructure.DBConfiguration.EFCore
{
    public class ApplicationContext : DbContext
    {
        public Guid OperationId { get; private set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            OperationId = Guid.NewGuid();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        /* Creating DatbaseContext without Dependency Injection it would be Transient */
        public ApplicationContext() : this(Guid.NewGuid()) { }
        public ApplicationContext(Guid id)
        {
            OperationId = id;
        }
        private IConfigurationRoot _config => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection")); //dbContextOptionsBuilder.UseInMemoryDatabase("ToDoListMemory");
            }
        }
       
    }
}
