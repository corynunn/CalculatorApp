using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class ResultBuilder
    {
        public string Output { get; private set; }

        public string BuildOutput(string equation, string result)
        {
            string newResult = (equation + "\n" + result + "\n");
            newResult += Output;
            Output = newResult;
            return Output;
        }
    }
}
