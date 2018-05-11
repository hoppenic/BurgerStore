using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerStore.Models;


namespace BurgerStore.Controllers
{
    public class ProductController : Controller
    {

    
      

        public ProductController()
        {


        }



        public IActionResult Index()
        {

            List<Product> products = new List<Product>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);
            connection.Open();

            System.Data.SqlClient.SqlCommand command = connection.CreateCommand();

            command.CommandText="SELECT Production.ProductModel.ProductModelID,[ID], ["name"],["description"],["price"],["organic"],["grassfed"] FROM Production.Productmodel INNER JOIN Production.ProductModelProductDescriptionCulture ON Production.ProductModel.ProductModelID = Production.ProductModelProductDescriptionCulture.ProductModelID INNER JOIN Production.ProductDescription ON Production.ProductModelProductDescriptionCulture.ProductDescriptionID = Production.ProductDescription.ProductDescriptionID WHERE CultureID = 'en'";

            System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id= reader.GetInt32(0);
                string name = reader.GetString(1);
                string description = reader.GetString(2);
                int price = reader.GetInt32(3);
                bool organic = reader.GetBoolean(4);
                bool grassfed = reader.GetBoolean(5);
                products.Add(new Product
                {
                    ID = id,
                    Description=description,
                    Name=name,
                    Price=price,
                    Organic=organic,
                    Grassfed=grassfed,                 
                });

            }
            reader.Close();

            foreach(var product in products)
            {
                System.Data.SqlClient.SqlCommand imageAndPriceCommand = connection.CreateCommand();
                imageAndPriceCommand.CommandText = @"SELECT ListPrice, Production.ProductPhoto.LargePhoto FROM Production.Product INNER JOIN Production.ProductProductPhoto ON Production.Product.ProductID = Production.ProductProductPhoto.ProductID
                +INNER JOIN Production.ProductPhoto ON Production.ProductPhoto.ProductPhotoID = Production.ProductProductPhoto.ProductPhotoID
                    +WHERE Production.ProductProductPhoto.[Primary] = 1 AND Production.Product.ProductModelID =" + product.ID;
               System.Data.SqlClient.SqlDataReader reader2 = imageAndPriceCommand.ExecuteReader();
              while (reader2.Read())
                {
                    product.Price = reader2.IsDBNull(0) ? (decimal?)null :reader2.GetSqlMoney(0).ToDecimal();
                    byte[] imageBytes = (byte[])reader2[1];
                    product.Image = "data:image/jpeg;base64, " + Convert.ToBase64String(imageBytes);
                     break;
                }

                reader2.Close();
            }

            connection.Close();
            return View(products);

        }

        

        //This is the details method connecting to details view
        public IActionResult Details(int? ID)
        {
            if (ID.HasValue)
            {
                Product p = null;

                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();

                System.Data.SqlClient.SqlCommand command = connection.CreateCommand();

                command.CommandText = "SELECT Production.ProductModel.ProductModelID, [Name], [Description] FROM Production.ProductModel INNER JOIN Production.ProductModelProductDescriptionCulture ON Production.ProductModel.ProductModelID = Production.ProductModelProductDescriptionCulture.ProductModelID INNER JOIN Production.ProductDescription ON Production.ProductModelProductDescriptionCulture.ProductDescriptionID = Production.ProductDescription.ProductDescriptionID WHERE CultureID = 'en' AND Production.ProductModel.ProductModelID = " + id.Value;

                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    p = new Product
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetInt32(3),
                        Organic = reader.GetBoolean(4),
                        Grassfed = reader.GetBoolean(5),
                    };

                }


                reader.Close();

                if(p != null)
                {
                    System.Data.SqlClient.SqlCommand imageAndPriceCommand = connection.CreateCommand();
                    imageAndPriceCommand.CommandText = @"SELECT ListPrice, Production.ProductPhoto.LargePhoto FROM Production.Product INNER JOIN Production.ProductProductPhoto ON Production.Product.ProductID = Production.ProductProductPhoto.ProductID
                    INNER JOIN Production.ProductPhoto ON Production.ProductPhoto.ProductPhotoID = Production.ProductProductPhoto.ProductPhotoID
                    WHERE Production.ProductProductPhoto.[Primary] = 1 AND Production.Product.ProductModelID =" + p.ID;
                    System.Data.SqlClient.SqlDataReader reader2 = imageAndPriceCommand.ExecuteReader();
                    while (reader2.Read())
                    {
                        p.Price = reader2.IsDBNull(0) ? (decimal?)null :reader2.GetSqlMoney(0).ToDecimal();
                        byte[] imageBytes = (byte[])reader2[1];
                        p.Image = "data:image/jpeg;base64, " + Convert.ToBase64String(imageBytes);
                       break;
                    }
                    reader2.Close();

                }

                connection.Close();

                if(p!= null)
                {
                    return View(p);
                }
            }

            return NotFound();
        }




    }
}