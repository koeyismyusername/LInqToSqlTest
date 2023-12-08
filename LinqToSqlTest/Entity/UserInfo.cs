using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace LinqToSqlTest.Entity
{
    [Table(Name ="UserInfo")]
    public class UserInfo
    {
        [Column(Name ="Seq", CanBeNull = false, DbType ="int", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Seq { get; private set; }
        [Column(Name ="phone", CanBeNull = false, DbType ="nvarchar(14)")]
        public string Phone { get; private set; }
        [Column(Name ="name", CanBeNull = false, DbType ="nvarchar(100)")]
        public string Name { get; private set; }
        [Column(Name ="birthday", CanBeNull = false, DbType ="datetime")]
        public DateTime Birthday { get; private set; }
        [Column(Name ="created_at", CanBeNull = false, DbType ="datetime", IsDbGenerated = true)]
        public DateTime CreatedAt { get; private set; }
        [Column(Name ="modified_at", DbType ="datetime")]
        public DateTime? ModiriedAt { get; private set; }
        [Column(Name ="deleted_at", DbType ="datetime")]
        public DateTime? DeletedAt { get; private set; }

        public static UserInfo Of(string phone, string name, DateTime birthday)
        {
            return new UserInfo() { Phone = phone, Name = name, Birthday = birthday };
        }

        public override string ToString()
        {
            return $"{{{nameof(Seq)}:{Seq}, {nameof(Phone)}:{Phone}, {nameof(Name)}:{Name}, {nameof(Birthday)}:{Birthday}, {nameof(CreatedAt)}:{CreatedAt}, {nameof(ModiriedAt)}:{ModiriedAt}, {nameof(DeletedAt)}:{DeletedAt}}}";
        }

        internal void ChangeName(string name)
        {
            Name = name;
        }
    }
}
