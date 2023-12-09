using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace LinqToSqlTest.Entity
{
    [Table(Name = "Stock")]
    public class Stock
    {
        [Column(Name = "seq", CanBeNull = false, DbType = "int", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Seq { get; private set; }
        [Column(Name = "exchange", CanBeNull = false, DbType = "nvarchar(10)")]
        public string Exchange { get; private set; }
        [Column(Name = "short_code", CanBeNull = false, DbType = "nvarchar(9)")]
        public string ShortCode { get ; private set; }
        [Column(Name = "name", CanBeNull = false, DbType = "nvarchar(100)")]
        public string Name { get; private set; }
        [Column(Name = "created_at", CanBeNull = false, DbType = "datetime", IsDbGenerated = true)]
        public DateTime CreatedAt { get; private set; }
        [Column(Name = "modified_at", DbType = "datetime")]
        public DateTime? ModifiedAt { get; private set; }
        [Column(Name = "deleted_at", DbType = "datetime")]
        public DateTime? DeletedAt { get; private set; }

        public override string ToString()
        {
            return $"{{{nameof(Seq)}:{Seq}, {nameof(Exchange)}:{Exchange}, {nameof(ShortCode)}:{ShortCode}, {nameof(Name)}:{Name}, {nameof(CreatedAt)}:{CreatedAt}, {nameof(ModifiedAt)}:{ModifiedAt}, {nameof(DeletedAt)}:{DeletedAt}}}";
        }
    }
}
