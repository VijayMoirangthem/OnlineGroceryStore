using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GrocerySysWeb_DAL
{
    public class clsConnectionDB
    {
        public static SqlConnection openConnection()
        {
            string str = "Data Source=SIDDANT\\SQLEXPRESS;Initial Catalog=OnlineGroceryStore;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            return conn;
        }
    }
}