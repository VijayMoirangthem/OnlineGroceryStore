using GrocerySysWeb_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GrocerySysWeb_DAL.Repository
{
    public class InventoryRepository
    {
        public List<String> getProductType()
        {
            List<String> TypeList = new List<String>();
            using(SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_getProductCategory", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using(SqlDataReader SDR = cmd.ExecuteReader()) 
                {
                    while(SDR.Read())
                    {
                        TypeList.Add(SDR["ProductType"].ToString());
                    }
                }
            }
            return TypeList;
        }


        public List<InventoryModel> getByProductType(string ProductType)
        {
            List <InventoryModel> TypeList = new List<InventoryModel>();
            using (SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_getByTypeProducts", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type", ProductType);
                using(SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while(SDR.Read())
                    {
                        InventoryModel PL = new InventoryModel();
                        PL.ProductID = SDR["ProductID"].ToString();
                        PL.ProductName = SDR["ProductName"].ToString();
                        PL.ProductType = SDR["ProductType"].ToString();
                        PL.ProductQuantity = Convert.ToInt32(SDR["ProductQuantity"]);
                        PL.ProductBrand = SDR["ProductBrand"].ToString();
                        PL.ProductPrice = Convert.ToInt32(SDR["ProductPrice"]);
                        PL.ProductImage = (byte[])SDR["ProductImage"];
                        TypeList.Add(PL);
                    }
                }
            }
            return TypeList;
        }
    }
}