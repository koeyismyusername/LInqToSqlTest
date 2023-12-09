using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace LinqToSqlTest.Entity
{
    [Table(Name = "UserStockRelation")]
    public class UserStockRelation
    {
        [Column(Name = "seq", CanBeNull = false, DbType = "int", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Seq { get; private set; }
        [Column(Name = "userInfo_seq", CanBeNull = false, DbType = "int")]
        public int UserInfoSeq { get; private set; }
        [Column(Name = "stock_seq", CanBeNull = false, DbType = "int")]
        public int StockSeq { get; private set; }
    }
}
