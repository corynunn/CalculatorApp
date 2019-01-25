using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class RuntimeError: Exception
    {
        public override string Message { get { return "Attempted to divide by zero."; } }
    }
}
