using Dapper.Contrib.Extensions;
using DapperDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo
{
    public  class DapperExtHelper<T> where T : BaseModel, new()
    {
        public T Get(int id)
        {
            return DbContext.DbConnection.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbContext.DbConnection.GetAll<T>();
        }

        public long Insert(T t)
        {
            return DbContext.DbConnection.Insert<T>(t);
        }

        public bool Update(T t)
        {
            return DbContext.DbConnection.Update<T>(t);
        }

        public bool Delete(T t)
        {
            return DbContext.DbConnection.Delete<T>(t);
        }

        public bool DeleteAll()
        {
            return DbContext.DbConnection.DeleteAll<T>();
        }
    }
}
