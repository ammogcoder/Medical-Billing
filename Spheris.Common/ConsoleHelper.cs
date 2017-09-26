using System;
using System.Collections.Generic;
using System.Text;

namespace Spheris.Common
{
    public static class ConsoleHelper
    {
        public static void WriteLine(string text)
        {
            Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("G", System.Globalization.CultureInfo.InvariantCulture), text);
        }

        public static void WriteLine(string text, params object[] args)
        {
            WriteLine(String.Format(text, args));
        }
    }
}
