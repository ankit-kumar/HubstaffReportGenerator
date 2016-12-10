using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HubstaffReportGenerator.Helper
{
   public static class DebugHelper
    {
        public static void PrintStaticProperties(object obj)
        {
            var dict = GetFieldValues(obj);
            foreach (var kvp in dict)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Key = {0}, ", kvp.Key);
                Console.ResetColor();
                Console.ForegroundColor = kvp.Value == null ? ConsoleColor.Red : ConsoleColor.White;
                Console.WriteLine("Value = {0}", kvp.Value == null ? "null" : kvp.Value);
                Console.ResetColor();
            }
        }
        private static Dictionary<string, string> GetFieldValues(object obj)
        {
            return obj.GetType()
                      .GetProperties(BindingFlags.Public | BindingFlags.Static)
                      .Where(f => f.PropertyType == typeof(string))
                      .ToDictionary(f => f.Name,
                                    f => (string)f.GetValue(null));
        }
    }
}
