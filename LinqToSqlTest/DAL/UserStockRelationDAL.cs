using LinqToSqlTest.Entity;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace LinqToSqlTest.DAL
{
    public static class UserStockRelationDAL
    {
        private static DataContext Db { get => new DataContext(ConnectionStrings.TutorialDB); }

        public static IQueryable<UserStockRelation> GetAll(DataContext db, Func<UserStockRelation, bool> Where)
        {
            var table = db.GetTable<UserStockRelation>();

            return from userStockRelation in table
                   where Where(userStockRelation)
                   select userStockRelation;
        }
    }
}
