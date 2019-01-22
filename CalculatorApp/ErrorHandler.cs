using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public static class ErrorHandler
    {
        /// <summary>
        /// Goes through the chain of exceptions finding the innermost one and returns the message.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string InnermostExceptionMessager(Exception ex)
        {
            //This will drill through the inner exceptions to find the first one.
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex.Message;
        }
    }
}
