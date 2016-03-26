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
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<PizzaFlavor> PizzaFlavor { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<PizzaFlavorIngredient> PizzaFlavorIngredient { get; set; }
        public DbSet<PizzaSize> PizzaSize { get; set; }
        public DbSet<DrinkType> DrinkType { get; set; }
        public DbSet<Drink> Drink { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLog> OrderLog { get; set; }
        public DbSet<OrderPizza> OrderPizza { get; set; }
        public DbSet<OrderDrink> OrderDrink { get; set; }
    }
}