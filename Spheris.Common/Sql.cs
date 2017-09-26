using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Spheris.Common
{
    public static class SqlHelper
    {
        /// <summary>
        /// Splits a SQL command that has GO command separators into
        /// an array of command strings that do not contain the GO
        /// statements so that they can be executed separately.
        /// </summary>
        /// <param name="statements">GO separated SQL commands.</param>
        /// <returns>String array of SQL commands in the order they were listed originally.</returns>
        public static string[] SplitCommandsAtGo(string statements)
        {
            return Regex.Split(statements,@"\s*GO\s*");
        }
    }
}
