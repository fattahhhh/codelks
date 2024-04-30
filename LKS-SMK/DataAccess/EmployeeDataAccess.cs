using LKS_SMK.db;
using LKS_SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKS_SMK.DataAccess
{
    public class EmployeeDataAccess
    {
        Database db = new Database();

        public List<EmployeeModel> GetEmployees()
        {
            var result = new List<EmployeeModel>();

            var dt = db.Read($"select * from Employee");

            for ( int i = 0; i < dt.Rows.Count; i++)
            {
                var data = dt.Rows[i];

                result.Add(new EmployeeModel
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
                });
            }
            return result;
        }
    }
}
