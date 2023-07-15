using Contoso.API.Persistence.Domain;
using Microsoft.EntityFrameworkCore;

namespace Contoso.API.Persistence.DbContexts
{
    public class ContosoDbContext : DbContext
    {
        public ContosoDbContext(DbContextOptions<ContosoDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>()
        //        .HasData(
        //        new Customer()
        //        {
        //            CustomerId = 1,
        //            Name = "Aljandra",
        //            LastName = "Adames",
        //            IdentificationCard = "450-7852014-5",
        //            PhoneNumber = "809-985-9878",
        //        },
        //        new Customer()
        //        {
        //            CustomerId = 2,
        //            Name = "Estrella",
        //            LastName = "Adames",
        //            IdentificationCard = "780-7452014-5",
        //            PhoneNumber = "829-975-9988",
        //        });
        //    modelBuilder.Entity<Address>()
        //       .HasData(
        //           new Address()
        //           {
        //               AddressId = 1,
        //               Municipality = "Santo Domingo Sur",
        //               Sector = "Algo",
        //               StreetName = "St/ Gregorio",
        //               ZipCode = "11807",
        //               AddressDescription = "por el colmado la Santiaguera",
        //               CustomerId = 2
        //           },
        //           new Address()
        //           {
        //               AddressId = 2,
        //               Municipality = "Santo Domingo Norte",
        //               Sector = "Villa Mella",
        //               StreetName = "St/ Una calle cualquiera",
        //               ZipCode = "11201",
        //               AddressDescription = "Casa Azul #25",
        //               CustomerId = 1,

        //           });
        //}
    }
}
