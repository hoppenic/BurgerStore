using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerStore.Models
{
    public class BurgerStoreDbContext : IdentityDbContext<BurgerStoreUser>
    {
        public BurgerStoreDbContext(): base()
        {

        }

        public BurgerStoreDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


    }

    public class BurgerStoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Cart
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }

        public int ID { get; set; }
        public Guid CookieIdentifier { get; set; }
        public DateTime LastModified { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

    }

    public class CartItem
    {
        public int ID { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }

    public class Order
    {
        public int ID { get; set; }
        public double ccNumber { get; set; }
        public int ccVerify { get; set; }
        public string nameOnCard { get; set; }
        public string billAddress { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public double phoneNumber { get; set; }

    }

    public class OrderItem
    {
        public int ID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }


}
