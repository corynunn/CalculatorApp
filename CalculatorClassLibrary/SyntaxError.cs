using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class SyntaxError: Exception
    {
        

        public SyntaxError(string input) : base(input)
        {
            
        }
    }
}
