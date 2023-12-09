using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToSqlTest
{
    public sealed class ExchangeType
    {
        private static ExchangeType _kospi = new ExchangeType() { Seq = 1, Name = "코스피" };
        private static ExchangeType _kosdaq = new ExchangeType() { Seq = 2, Name = "코스닥" };
        private static ExchangeType _konex = new ExchangeType() { Seq = 3, Name = "코넥스" };

        public static ExchangeType KOSPI { get => _kospi; }
        public static ExchangeType KOSDAQ { get => _kosdaq; }
        public static ExchangeType KONEX { get => _konex; }

        public int Seq { get; private set; }
        public string Name { get; private set; }
        private ExchangeType() { }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is ExchangeType otherExchangetype) return GetHashCode().Equals(otherExchangetype.GetHashCode());

            return false;
        }

        public override int GetHashCode()
        {
            return Seq.GetHashCode();
        }
    }
}
