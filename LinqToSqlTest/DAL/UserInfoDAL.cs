using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Data.Linq;
using LinqToSqlTest.Entity;
using System.Runtime.Remoting.Contexts;
using System.Transactions;

namespace LinqToSqlTest.DAL
{
    public static class UserInfoDAL
    {
        private static DataContext Db { get => new DataContext(ConnectionStrings.TutorialDB); }
        public static void Insert(UserInfo userInfo)
        {
            using (var db = Db)
            {
                var table = db.GetTable<UserInfo>();
                table.InsertOnSubmit(userInfo);
                db.SubmitChanges();
            }
        }

        public static IQueryable<UserInfo> GetUserInfos()
        {
            using (var db = Db)
            {
                var table = db.GetTable<UserInfo>();

                return from userinfo in table
                       select userinfo;
            }
        }

        public static void ChangeUserName(Func<UserInfo, bool> Where, string name)
        {
            using (var db = Db)
            {
                var table = db.GetTable<UserInfo>();

                var userInfo = table.FirstOrDefault(Where);
                //var userInfo = (from u in table where u.Name == name select u).FirstOrDefault();
                if (userInfo is null)
                {
                    Console.WriteLine("유저정보를 불러오지 못했습니다.");
                    return;
                }

                userInfo.ChangeName(name);

                db.SubmitChanges();
                Console.WriteLine("이름을 성공적으로 변경했습니다.");
            }
        }

        public static void Delete(Func<UserInfo, bool> Where)
        {
            using (var db = Db)
            {
                var table = db.GetTable<UserInfo>();

                var userInfoToDelete = table.FirstOrDefault(Where);
                if (userInfoToDelete != null)
                {
                    table.DeleteOnSubmit(userInfoToDelete);
                }

                db.SubmitChanges();
                Console.WriteLine("성공적으로 삭제했습니다.");
            }
        }

        internal static void InsertRollBack(UserInfo userInfo)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                try
                {
                    using (var db = Db)
                    {
                        var table = db.GetTable<UserInfo>();
                        table.InsertOnSubmit(userInfo);

                        Console.WriteLine("성공적으로 인서트했습니다");

                        //Console.WriteLine("그리고 아무것도 하지 않았습니다.");

                        //transactionScope.Complete();
                        //Console.WriteLine("그리고 Complete 했습니다.");

                        db.SubmitChanges();
                        transactionScope.Complete();
                        Console.WriteLine("그리고 서밋했습니다.");
                    }
                }
                catch
                {
                    Console.WriteLine("쿼리 실행 도중 에러가 발생하여 롤백했습니다.");
                }
            }
        }

        public static void InsertRollBack2(UserInfo userInfo)
        {
            using (var db = Db)
            {
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                var table = db.GetTable<UserInfo>();
                table.InsertOnSubmit(userInfo);

                db.SubmitChanges();
                Console.WriteLine("성공적으로 인서트했습니다");

                //db.Transaction.Rollback();
                //Console.WriteLine("그리고 서밋했습니다.");
            }
        }

        public static UserInfo GetFirstUserInfo(DataContext db, Func<UserInfo, bool> Where)
        {
            var table = db.GetTable<UserInfo>();

            return table.FirstOrDefault(Where);
        }
    }
}
