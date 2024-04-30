using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKS_SMK.Model
{
    public class MasterRoomModel
    {
        public int ID { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomFloor {  get; set; }
        public string Description { get; set; }
        public int GetRoomTypeIndex()
        {
            return RoomTypeID;
        }
    }
}
