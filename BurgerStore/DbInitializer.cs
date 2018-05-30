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
            //Sometimes this has to be commented out if you run into migrations/db rebuild issues
            db.Database.Migrate();

            if (db.Products.Count() == 0)
            {
                db.Products.Add(new Product
                {

                    Name = "Ground Beef Burger",
                    Description = "Our special 80/20 blend of ground beef",
                    Image = "/images/burger1.jpeg  ",
                    Price = 5.99m,
                    Organic = false,
                    Grassfed = false

                });
                db.Products.Add(new Product
                {

                    Name = "Organic Grassfed Ground Beef Burger",
                    Description = "A delicious blend of humanely raised ground organic grassfed beef",
                    Image = "/images/burger2.jpeg ",
                    Price = 7.99m,
                    Organic = true,
                    Grassfed = true
                });
                db.Products.Add(new Product
                {

                    Name = "Ground Turkey Burger",
                    Description = "Thanksgiving on the grill!",
                    Image = "/images/burger3.jpeg ",
                    Price = 3.99m,
                    Organic = false,
                    Grassfed = false
                });
                db.Products.Add(new Product
                {
                    Name = "Veggie Burger",
                    Description = "A healthy and delicous meat alternative for your grilling pleasure!",
                    Image = "/images/veggieburger.jpeg",
                    Price = 5.99M,
                    Organic = true,
                    Grassfed = false
                });


                //This helps not make the request too "chatty" with SQL(locking tables)
                db.SaveChanges();

            }



        }
    }
}