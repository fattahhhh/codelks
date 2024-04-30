using LKS_SMK.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKS_SMK.Model
{
    public class RoomTypeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public double RoomPrice { get; set; }
        public string Photo {  get; set; }
    }
}
