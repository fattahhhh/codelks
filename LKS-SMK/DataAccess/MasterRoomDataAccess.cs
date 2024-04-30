using LKS_SMK.db;
using LKS_SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LKS_SMK.DataAccess
{
    public class MasterRoomDataAccess
    {
        Database db = new Database();

        public List<MasterRoomModel> GetMasterRooms()
        {
            var result = new List<MasterRoomModel>();

            var dt = db.Read("select * from Room");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var data = dt.Rows[i];

                result.Add(new MasterRoomModel
                {
                    ID = int.Parse(data["ID"].ToString()),
                    RoomTypeID = int.Parse(data["RoomTypeID"].ToString()),
                    RoomNumber = data["RoomNumber"].ToString(),
                    Description = data["Description"].ToString()
                });
            }
            return result;
        }

        public List<string> Save(List<MasterRoomModel> insert, List<MasterRoomModel> update, List<MasterRoomModel> delete)
        {

            var errors = new List<string>();

            foreach (var room in delete)
            {
                try
                {
                    db.Execute($"delete from Room where ID = {room.ID}");
                }
                catch (Exception ex)
                {
                    errors.Add($"Tidak bisa delete Room ID : {room.ID}");
                }
            }

            foreach (var room in update)
            {
                try
                {
                    db.Execute($@"
                        update Room set 
	                        RoomTypeID = {room.RoomTypeID},
	                        Room Number = '{room.RoomNumber}',
	                        RoomFloor = '{room.RoomFloor}',
	                        Description = '{room.Description}'
                        where ID = {room.ID}
                    ");
                }
                catch (Exception ex)
                {
                    errors.Add($"Tidak bisa update Room ID : {room.ID}");
                }
            }

            foreach (var room in insert)
            {
                try
                {
                    db.Execute($@"
                        insert into Room
                        (
                        RoomTypeID,
                        RoomNumber,
                        RoomFloor,
                        Description	                      
                        )
                        values
                        (
                        {room.RoomTypeID},
                        '{room.RoomNumber}',
                        '{room.RoomFloor}',
                        '{room.Description}'
                        )
                    ");
                }
                catch (Exception ex)
                {
                    errors.Add($"Tidak bisa insert Room ID : {room.ID}");
                }
            }

            return errors;
        }

        public List<RoomTypeModel> GetRoomTypes()
        {
            return new RoomTypeDataAccess().GetRoomTypes();
        }
    }
}
