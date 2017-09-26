using System;
using System.Runtime.Serialization;
using System.Text;

namespace Spheris.Common
{
    [Serializable]
    public class SqlException : Exception
    {
        public SqlException()
            : base()
        {
        }

        public SqlException(string message)
            : base(message)
        {
        }

        public SqlException(string message, Exception ex)
            : base(message, ex)
        {
        }

        protected SqlException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
