using LKS_SMK.db;
using LKS_SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKS_SMK.DataAccess
{
    public class FoodDrinkDataAccess
    {
        Database db = new Database();

        public List<FoodDrinkModel> getFds()
        {
            var result = new List<FoodDrinkModel>();

            var dt = db.Read("select * from FoodsAndDrinks");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var data = dt.Rows[i];

                result.Add(new FoodDrinkModel
                {
                    ID = int.Parse(data["ID"].ToString()),
                    Name = data["Name"].ToString(),
                    Type = data["Type"].ToString(),
                    Price = double.Parse(data["Price"].ToString()),
                    Photo = data["Photo"].ToString()
                });
            }
            return result;
        }

        public void Save(FoodDrinkModel foodDrink)
        {
            db.Execute("delete from FoodsAndDrinks");

            try
            {
                db.Execute($@"
                    insert into FoodsAndDrinks
                    (
                    Name,
                    Type,
                    Price
                    Photo
                    )
                    values
                    (
                    '{foodDrink.Name}',
                    '{foodDrink.Type}'
                    '{foodDrink.Price}',
                    '{foodDrink.Photo}'
                ");
            }
            catch (Exception e)
            {

            }
        }
    }
}
