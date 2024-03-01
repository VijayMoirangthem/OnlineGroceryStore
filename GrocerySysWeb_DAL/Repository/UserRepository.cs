using GrocerySysWeb_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GrocerySysWeb_DAL.Repository
{
    public class UserRepository
    {
        public UserModel verifyUser(string Email , string Password)
        {
            UserModel User = new UserModel();   
            using(SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_getByIDUserLogin", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@password", Password);
                using(SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while(SDR.Read())
                    {
                        User.UserEmail = SDR["UserEmail"].ToString();
                        User.UserName = SDR["UserName"].ToString();
                        User.UserMobile = SDR["UserMobile"].ToString();
                        User.UserPassword = SDR["UserPassword"].ToString();
                        User.UserAddress = SDR["UserAddress"].ToString();
                    }
                }
            }
            return User;
        }


        public void insertUser(UserModel User)
        {
            using (SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_insertUserLogin", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", User.UserEmail);
                cmd.Parameters.AddWithValue("@name", User.UserName);
                cmd.Parameters.AddWithValue("@mobile", User.UserMobile);
                cmd.Parameters.AddWithValue("@password", User.UserPassword);
                cmd.Parameters.AddWithValue("@address", User.UserAddress);
                cmd.ExecuteNonQuery();
            }
        }
    }
}