using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;


namespace ToDoList.Data
{
    internal class AppDbContext : DbContext
    {

        public DbSet<Person> Persons { get; set; }
        public DbSet<TODOTask> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // = jak se EF pripoji k databazi
        {
            if (!optionsBuilder.IsConfigured) // kontroluje jestli nekdo uz dbcontext nepripojil
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\mssqllocaldb;Database=ToDoListDB;Trusted_Connection=True;" //vytvari databazi
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // = jak bude vypadat databaze
        {
            //vytvrani "doplnjici" pravidla, ktera se daji jinak zapsat uz pri vytvareni tabulky
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // povinne, max 100 znaku

                entity.HasData(
                    new Person { Id = 1, Name = "Jan Novak" }
                );
            });

            modelBuilder.Entity<TODOTask>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200); // povinne, max delka 200 znaku
                entity.Property(e => e.Description).HasMaxLength(1000); // neni povinny, max delka 1000 znaku

                entity.HasData(
                    new TODOTask { Id = 1, Title = "Clena room", Description = "Clean my desk and throw away trash", IsCompleted = false, PersonId = 1 }
                );
            });

            /*
             entity.property(e => e.) = vytvrani "doplnjici" pravidla, ktera se daji jinak zapsat uz pri vytvareni tabulky
             entity.hasdata() = vytvrani defaultni data, jsou v tabulce od vytvoreni
             */
        }

    }
}
