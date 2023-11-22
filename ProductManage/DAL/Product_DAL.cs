using ProductManage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ProductManage.DAL
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoconnectionString"].ToString();

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts1";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();
                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = Convert.ToString(dr["ProductName"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qnty = Convert.ToInt32(dr["Qnty"]),
                        Remarks = Convert.ToString(dr["Remarks"])
                    });


                }
                return productList;

            }
        }

        //INSERT PRODUCT
        public bool InsertProduct(Product product)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProductName", product.ProductName);
                command.Parameters.AddWithValue("Price", product.Price);
                command.Parameters.AddWithValue("Qnty", product.Qnty);
                command.Parameters.AddWithValue("Remarks", product.Remarks);
                connection.Open();
                id = command.ExecuteNonQuery();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //get product by id
        public List<Product> GetProductByID(int ProductID)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetProductbyID";
                command.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();
                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = Convert.ToString(dr["ProductName"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qnty = Convert.ToInt32(dr["Qnty"]),
                        Remarks = Convert.ToString(dr["Remarks"])
                    });


                }
                return productList;

            }
        }

        //update Product

        public bool UpdateProduct(Product product)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProductID", product.ProductID);
                command.Parameters.AddWithValue("ProductName", product.ProductName);
                command.Parameters.AddWithValue("Price", product.Price);
                command.Parameters.AddWithValue("Qnty", product.Qnty);
                command.Parameters.AddWithValue("Remarks", product.Remarks);
                connection.Open();
                i = command.ExecuteNonQuery();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Delete Product

        public string DeleteProduct(int productid)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("productid", productid);
                command.Parameters.Add("@OutputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OutputMessage"].Value.ToString();
                connection.Close();

            }
            return result;
        }

        //search





    }
}