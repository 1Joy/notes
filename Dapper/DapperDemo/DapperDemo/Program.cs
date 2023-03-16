using Dapper;
using DapperDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DapperExtHelper<User> dapperExtHelper = new DapperExtHelper<User>();

            var user = new User { Name="joy",Age=26,Create=DateTime.Now};
            dapperExtHelper.Insert(user);

            //unchecked：取消算术运算转换的溢出检查
            int i = 0;
            unchecked
            {
                i = 2147483647 + 50;
            }

        }
    }
}
