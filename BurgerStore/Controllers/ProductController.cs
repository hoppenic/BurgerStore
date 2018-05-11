using BurgerStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


namespace BurgerStore.Controllers
{
    public class ProductController : Controller
    {
        private string _AdventureWorks2016ConnectionString = null;

        public ProductController(IConfiguration config)
        {
            // using microsoft.extensions.configuration
            _AdventureWorks2016ConnectionString = config.GetConnectionString("Adventureworks2016");
        }



        public IActionResult Index()
        {

            List<Product> products = new List<Product>();

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(_AdventureWorks2016ConnectionString))
            {
                connection.Open();

                using (System.Data.SqlClient.SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "sp_GetAllProducts";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        int idColumn = reader.GetOrdinal("ID");
                        int nameColumn = reader.GetOrdinal("Name");
                        int descriptionColumn = reader.GetOrdinal("Description");
                        int priceColumn = reader.GetOrdinal("Price");
                        int organicColumn = reader.GetOrdinal("Organic");
                        int grassfedColumn = reader.GetOrdinal("Grassfed");

                        while (reader.Read())
                        {
                            int productModelID = reader.GetInt32(idColumn);
                            string name = reader.GetString(nameColumn);
                            string description = reader.GetString(descriptionColumn);
                            int price = reader.GetInt32(priceColumn);
                            bool organic = reader.GetBoolean(organicColumn);
                            bool grassfed = reader.GetBoolean(grassfedColumn);
                            products.Add(new Product
                            {
                                ID = productModelID,
                                Name = name,
                                Description = description,
                                Price = price,
                                Organic = organic,
                                Grassfed = grassfed
                            });


                        }
                    }

                }

                foreach (var product in products)
                {
                    using (System.Data.SqlClient.SqlCommand imageAndPriceCommand = connection.CreateCommand())
                    {
                        imageAndPriceCommand.CommandText = "sp_GetProductImages";
                        imageAndPriceCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        imageAndPriceCommand.Parameters.AddWithValue("@productModelId", product.ID);
                        using (System.Data.SqlClient.SqlDataReader reader2 = imageAndPriceCommand.ExecuteReader())
                        {
                            while (reader2.Read())
                            {

                                product.Price = reader2.IsDBNull(0) ? (decimal?)null : reader2.GetSqlMoney(0).ToDecimal();
                                byte[] imageBytes = (byte[])reader2[1];
                                product.Image = "data:image/jpeg;base64, " + Convert.ToBase64String(imageBytes);
                                break;


                            }
                        }


                    }
                }




            }


            return View(products);

        }

            //This is the details method connecting to details view
            public IActionResult Details(int? ID)
            {
                if (ID.HasValue)
                {
                    Product p = null;

                 using(System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(_AdventureWorks2016ConnectionString))
                {
                    connection.Open();
                    System.Data.SqlClient.SqlCommand command = connection.CreateCommand();

                    command.CommandText = "sp_GetProduct";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@productModelID", ID.Value);

                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        ID=reader.GetInt32(0),
                        Name=reader.GetString(1),
                        Description=reader.GetString(2),
                        Price =reader.GetInt32(3),
                        Organic=reader.GetBoolean(4),
                        Grassfed=reader.GetBoolean(5)
                                    
                    };

                }
                
                
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

                    if (p != null)
                    {
                        System.Data.SqlClient.SqlCommand imageAndPriceCommand = connection.CreateCommand();
                        imageAndPriceCommand.CommandText = "sp_GetProductImages";
                        imageAndPriceCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        imageAndPriceCommand.Parameters.AddWithValue("@productModelID", p.ID);

                        System.Data.SqlClient.SqlDataReader reader2 = imageAndPriceCommand.ExecuteReader();
                        while (reader2.Read())
                        {
                            p.Price = reader2.IsDBNull(0) ? (decimal?)null : reader2.GetSqlMoney(0).ToDecimal();
                            byte[] imageBytes = (byte[])reader2[1];
                            p.Image = "data:image/jpeg;base64, " + Convert.ToBase64String(imageBytes);
                            break;
                        }
                        reader2.Close();

                    }

                    connection.Close();

                    if (p != null)
                    {
                        return View(p);
                    }
                }

                return NotFound();
            }




        }

    }