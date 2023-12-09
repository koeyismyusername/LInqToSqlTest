using LinqToSqlTest.DAL;
using LinqToSqlTest.Entity;
using LinqToSqlTest.Utils;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace LinqToSqlTest
{
    public class Program
    {

        static void Main(string[] args)
        {
            //InsertUserInfoTest();
            //GetUserInfosTest();
            //UpdateUserInfoTest();
            //DeleteUserInfoTest();
            //TransactionTest();
            //JoinTest2();
            //IEnumerableTest();
            OrderByTest();
        }

        private static void InsertUserInfoTest()
        {
            var userInfo = UserInfo.Of("010-2345-6789", "황장수", new DateTime(1997, 11, 10));
            Console.WriteLine("유저가 생성되었습니다.");
            Console.WriteLine(userInfo.ToString());

            UserInfoDAL.Insert(userInfo);
            Console.WriteLine("유저를 성공적으로 삽입했습니다.");
        }

        private static void GetUserInfosTest()
        {
            var userInfos = UserInfoDAL.GetUserInfos();

            foreach (var userInfo in userInfos)
            {
                Console.WriteLine(userInfo.ToString());
            }
        }

        private static void UpdateUserInfoTest()
        {
            UserInfoDAL.ChangeUserName(x => true, "유관순");
        }

        private static void DeleteUserInfoTest()
        {
            UserInfoDAL.Delete(x => x.Name.Equals("유관순"));
        }

        private static void TransactionTest()
        {
            var userInfo = UserInfo.Of("010-3333-3333", "대중자", new DateTime(2002, 4, 16));
            UserInfoDAL.InsertRollBack2(userInfo);
        }

        private static void JoinTest()
        {
            using (var db = new DataContext(ConnectionStrings.TutorialDB))
            {
                var userInfo = UserInfoDAL.GetFirstUserInfo(db, x => x.Phone.Equals("010-1234-5678"));
                //Console.WriteLine(userInfo.ToString());
                if (userInfo is null)
                {
                    Console.WriteLine("유저정보를 조회하는 데 실패했습니다.");
                    return;
                }

                var userStockRelations = UserStockRelationDAL.GetAll(db, x => x.UserInfoSeq == userInfo.Seq);
                if (userStockRelations is null)
                {
                    Console.WriteLine("UserStockRelation 정보를 조회하는 데 실패했습니다.");
                    return;
                }

                var stockSeqs = from data in userStockRelations
                                select data.StockSeq;

                var stocks = StockDAL.GetAll(x => stockSeqs.Contains(x.Seq));
                if (stocks is null)
                {
                    Console.WriteLine("Stock 정보를 조회하는 데 실패했습니다.");
                    return;
                }

                foreach (var stock in stocks)
                {
                    Console.WriteLine(stock.ToString());
                }
            }
        }

        private static void JoinTest2()
        {
            foreach (var row in StockDAL.GetAllByUserInfoPhone("010-6666-6666"))
            {
                Console.WriteLine(row.ToString());
            }
        }

        private static void IEnumerableTest()
        {
            var stocks = StockDAL.GetAll(x => true);

            foreach (var stock in stocks)
            {
                Console.WriteLine(stock.ToString());
            }
        }

        private static void OrderByTest()
        {
            var stocks = StockDAL.GetAllOrderBy(x => x.ShortCode, new ReverseComparer<string>());

            foreach (var stock in stocks)
            {
                Console.WriteLine(stock.ToString());
            }
        }
    }
}
