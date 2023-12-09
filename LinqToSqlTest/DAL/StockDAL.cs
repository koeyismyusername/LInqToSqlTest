using LinqToSqlTest.Entity;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace LinqToSqlTest.DAL
{
    public static class StockDAL
    {
        private static DataContext TutorialDb { get => new DataContext(ConnectionStrings.TutorialDB); }

        public static List<Stock> GetAll(Func<Stock, bool> filter, int size = 1000, int page = 1)
        {
            int skipCount = (page - 1) * size;
            using (var db = TutorialDb)
            {
                return db.GetTable<Stock>()
                    .Where(filter)
                    .Skip(skipCount)
                    .Take(size)
                    .ToList();
            }
        }

        public static List<Stock> GetAll(int size = 1000, int page = 1)
        {
            int skipCount = (page -1 ) * size;
            using (var db = TutorialDb)
            {
                return db.GetTable<Stock>()
                    .Skip(skipCount)
                    .Take(size)
                    .ToList();
            }
        }

        public static List<Stock> GetAllOrderBy<TKey>(Func<Stock, TKey> keySelector, IComparer<TKey> comparer, int size = 1000, int page = 1) where TKey : IComparable
        {
            int skipCount = (page - 1) * size;
            using (var db = TutorialDb)
            {
                return db.GetTable<Stock>()
                    .OrderBy(keySelector, comparer)
                    .Skip(skipCount)
                    .Take(size)
                    .ToList();
            }
        }

        public static List<Stock> GetAllOrderBy<TKey>(Func<Stock, bool> filter, Func<Stock, TKey> keySelector, IComparer<TKey> comparer, int size = 1000, int page = 1) where TKey : IComparable
        {
            int skipCount = (page - 1) * size;
            using (var db = TutorialDb)
            {
                return db.GetTable<Stock>()
                    .Where(filter)
                    .OrderBy(keySelector, comparer)
                    .Skip(skipCount)
                    .Take(size)
                    .ToList();
            }
        }

        public static List<Stock> GetAllByUserInfoPhone(string phone)
        {
            using (var db = TutorialDb)
            {
                var query = from stock in db.GetTable<Stock>()
                            join userStockRelation in db.GetTable<UserStockRelation>() on stock.Seq equals userStockRelation.StockSeq
                            join userInfo in db.GetTable<UserInfo>() on userStockRelation.UserInfoSeq equals userInfo.Seq
                            where userInfo.Phone.Equals(phone)
                            select stock;

                return query.ToList();
            }
        }
    }
}
