using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Service.Helpers
{
    internal static class InputValidationHelper
    {
        public static int GetReuiredIntegerInputFromStandardInput(string question)
        {
            bool condition = false;
            int number = 0;
            do
            {
               Console.WriteLine(question);
               condition = Int32.TryParse(Console.ReadLine(), out number);
            } while (!condition);

            return number;
        }

        public static string GetReuiredStringInputFromStandardInput(string question)
        {
            bool condition = false;
            var result = string.Empty;
            do
            {
                Console.WriteLine(question);
                result = Console.ReadLine();
                condition = string.IsNullOrEmpty(result);
            } while (condition);

            return result;
        }

    }
}
