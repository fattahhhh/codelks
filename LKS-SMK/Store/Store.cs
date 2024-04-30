using LKS_SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKS_SMK.Store
{
    public static class MainStore
    {
        public static EmployeeModel user;

        public static void Clear()
        {
            user = null;
        }
    }
}
