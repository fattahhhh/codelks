using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKS_SMK.db
{
    public class Database
    {
        private string _connectionString = @"Data Source=DESKTOP-2712V6I\SQLEXPRESS;Initial Catalog=""LKS-SMK JATI"";Integrated Security=True;";

        public DataTable Read(string query)
        {
            var dt = new DataTable();

            using (var cn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, cn))
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void Execute(string query)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
