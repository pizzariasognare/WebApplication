using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication.Models
{
    public partial class Entities : DbContext
    {
        public Entities() : base(nameOrConnectionString: "Entities") { }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<ZipCode> ZipCode { get; set; }
    }
}