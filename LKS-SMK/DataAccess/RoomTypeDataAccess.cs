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
    public class RoomTypeDataAccess
    {
        Database db = new Database();

        public List<RoomTypeModel> GetRoomTypes()
        {
            var result = new List<RoomTypeModel>();

            var dt = db.Read("select * from RoomType");

            for (int i = 0;  i < dt.Rows.Count; i++)
            {
                var data = dt.Rows[i];

                result.Add(new RoomTypeModel()
                {
                    ID = int.Parse(data["ID"].ToString()),
                    Name = data["Name"].ToString(),
                    Capacity = int.Parse(data["Capacity"].ToString()),
                    RoomPrice = int.Parse(data["RoomPrice"].ToString()),
                    Photo = data["Photo"].ToString()
                });
            }
            return result;
        }

        public List<string> Save(List<RoomTypeModel> insert, List<RoomTypeModel> update, List<RoomTypeModel> delete)
        {

            var errors = new List<string>();

            foreach (var roomType in delete)
            {
                try
                {
                    db.Execute($"delete from RoomType where ID = {roomType.ID}");
                }
                catch (Exception ex)
                {
                    errors.Add($"Tidak bisa delete RoomType ID : {roomType.ID}");
                }
            }

            foreach (var roomType in update)
            {
                try
                {
                    db.Execute($@"
                        update RoomType set 
	                        Name = '{roomType.Name}',
	                        Capacity = {roomType.Capacity},
	                        RoomPrice = {roomType.RoomPrice},
	                        Photo = '{roomType.Photo}'
                        where ID = {roomType.ID}
                    ");
                }
                catch (Exception ex)
                {
                    errors.Add($"Tidak bisa update RoomType ID : {roomType.ID}");
                }
            }

            foreach (var roomType in insert)
            {
                try
                {
                    db.Execute($@"
                        insert into RoomType
                        (
                        Name,
                        Capacity,
                        RoomPrice,
                        Photo	                      
                        )
                        values
                        (
                        '{roomType.Name}',
                        {roomType.Capacity},
                        {roomType.RoomPrice},
                        '{roomType.Photo}'
                        )
                    ");
                }
                catch (Exception ex)
                {
                    errors.Add($"Tidak bisa insert roomType ID : {roomType.ID}");
                }
            }

            return errors;
        }
    }
}
