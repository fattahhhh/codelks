using LKS_SMK.db;
using LKS_SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKS_SMK.DataAccess
{
    public class LoginDataAccess
    {
        Database db = new Database();

        public EmployeeModel Login(string Username, string Password)
        {
            var dt = db.Read($"select * from Employee where Username = '{Username}' and Password = '{Password}'");

            if (dt.Rows.Count == 0) throw new Exception("Data Tidak Ada");

            var data = dt.Rows[0];

            return new EmployeeModel
            {
                ID = int.Parse(data["ID"].ToString()),
                Username = data["Username"].ToString(),
                Password = data["Password"].ToString(),
                Name = data["Name"].ToString(),
                Email = data["Email"].ToString(),
                Address = data["Address"].ToString(),
                DateOfBirth = DateTime.Parse(data["DateOfBirth"].ToString()),
                JobID = int.Parse(data["JobID"].ToString()),
                Photo = data["Photo"].ToString()
            };
        }
    }
}
