using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class SyntaxError: Exception
    {
        string input;
        public override string Message { get { return input; } }

        public SyntaxError(string input)
        {
            this.input = input;
        }
    }
}
