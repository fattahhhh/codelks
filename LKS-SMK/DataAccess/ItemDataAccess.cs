using LKS_SMK.db;
using LKS_SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKS_SMK.DataAccess
{
    public class ItemDataAccess
    {
        Database db = new Database();

        public List<ItemModel> getItems()
        {
            var result = new List<ItemModel>();

            var dt = db.Read("select * from Item");

            for (int i = 0; i <dt.Rows.Count; i++)
            {
                var data = dt.Rows[i];

                result.Add(new ItemModel
                {
                    ID = int.Parse(data["ID"].ToString()),
                    Name = data["Name"].ToString(),
                    RequestPrice = data["RequestPrice"].ToString(),
                    CompensationFee = data["CompensationFee"].ToString()
                });
            }
            return result;
        }

        public void Save(ItemModel newItems)
        {
            db.Execute("delete from Item");

            try
            {
                db.Execute($@"
                    insert into Item
                    (
                    Name,
                    Request Price,
                    CompensationFee
                    )
                    values
                    (
                    '{newItems.Name}',
                    '{newItems.RequestPrice}',
                    '{newItems.CompensationFee}'
                    )
                ");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
