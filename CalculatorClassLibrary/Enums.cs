using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public enum Expression
    {
        Positive,
        Negative,
        Exponent,
        Multiply,
        Divide,
        Subtract,
        Add,
        OpenParenthesis,
        CloseParenthesis,
        Number
    }
}
