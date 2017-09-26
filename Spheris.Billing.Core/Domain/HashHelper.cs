using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    public static class HashHelper
    {
        public static int CombineHashCode<T>(this int hashCode, T arg)
        {
            unchecked
            {
                return 31 * hashCode + ((arg != null) ? arg.GetHashCode() : 0);
            }
        }
    }
}
