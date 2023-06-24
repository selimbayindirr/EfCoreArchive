using EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.DataAccess
{
    public class EfContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DevArcMyPc(optionsBuilder);
            //WorkPc(optionsBuilder);
        }
        //Fluent API yi burada tasarlıyoruz. Navi Propertyler yine aynı şekilde tanımladıkdan sonra burada kullanırız.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasKey(c => c.Id);
            modelBuilder.Entity<Person>().HasOne(c => c.Address).WithOne(c => c.Person).HasForeignKey<Address>(c => c.Id); //birebir 

            modelBuilder.Entity<Person>()
                .HasOne(c => c.Department)
                .WithMany(d => d.People);
            //.HasForeignKey(c => c.DId); //Department Id DEĞİLDE DId benim fk m olacak dersen budur
        }

        private static void DevArcMyPc(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=BYNDR28;Initial Catalog=EfCore;User ID=sa;Password=Perkon123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }




        private static void MyPc(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=BYNDR\\PIPLE;Initial Catalog=EfCore;User ID=dw;Password=Perkon123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


        private static void WorkPc(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SELIM-BAYINDIR\\ARC;Initial Catalog=EfCore;User ID=sa;Password=Perkon123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
