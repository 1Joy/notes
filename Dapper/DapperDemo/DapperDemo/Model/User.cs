using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo.Model
{
    [Table("User")]  //指定数据库表名，不然会在Contrib中自动对 使用的实体名称添加s
    public class User:BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }

    }
}
