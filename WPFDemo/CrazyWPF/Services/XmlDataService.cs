using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CrazyWPF.Models;

namespace CrazyWPF.Services
{
    public class XmlDataService : IDataService
    {
        public List<Dish> GetAllDishes()
        {
            List<Dish> dishes = new List<Dish>();
            //加载数据文件
            XDocument xDocument = XDocument.Load(@".\Data\Data.xml");
            var dishNodes = xDocument.Descendants("Dish");
            foreach (var item in dishNodes)
            {
                Dish dish = new Dish() {
                    Name = item.Element("Name").Value,
                    Category = item.Element("Category").Value,
                    Comment = item.Element("Comment").Value,
                    Score = double.Parse(item.Element("Score").Value)
                };
                dishes.Add(dish);
            }
            return dishes;
        }
    }
}
