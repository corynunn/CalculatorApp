using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class RuntimeError: Exception
    {
        
        public RuntimeError(string input) : base(input)
        {

        }
    }
}
