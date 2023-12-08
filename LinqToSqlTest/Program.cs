using LinqToSqlTest.DAL;
using LinqToSqlTest.Entity;
using System;
using System.Collections.Generic;
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
            TransactionTest();
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
    }
}
