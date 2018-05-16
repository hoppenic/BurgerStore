using System;
using BurgerStore.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace BurgerStore
{
    internal static class DbInitializer
    {
        internal static void Initialize(this BurgerStoreDbContext db)
        {
            db.Database.Migrate();

            if (db.Products.Count() == 0)
            {
                db.Products.Add(new Product
                {
                   
                    Name = "Ground Beef Burger",
                    Description = "Ground Beef",
                    Image = " ",
                    Price = 5.99m,
                    Organic = false,
                    Grassfed = false
                
                });
                db.Products.Add(new Product
                {
               
                    Name = "Organic Grassfed Ground Beef Burger",
                    Description="Ground Organic Grassfed Beef",
                    Image=" ",
                    Price=7.99m,
                    Organic=true,
                    Grassfed=true
                });
                db.Products.Add(new Product
                {
                 
                    Name = "Ground Turkey Burger",
                    Description = "Ground Turkey",
                    Image = " ",
                    Price = 3.99m,
                    Organic = false,
                    Grassfed = false
                });


                //This helps not make the request too "chatty" with SQL(locking tables)
                db.SaveChanges();

            }



        }
    }
}