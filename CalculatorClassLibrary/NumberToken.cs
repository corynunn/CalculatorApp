using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class NumberToken: ITokenable
    {
        
        public double Value { get; set; }
        public ITokenable PreviousToken { get; set; }
        public ITokenable NextToken { get; set; }
        public Expression Expression { get; set; }

        public NumberToken(double value)
        {
            this.Value = value;
            Expression = Expression.Number;
        }

        /// <summary>
        /// Returns the double of the value of this toke.
        /// </summary>
        /// <returns></returns>
        public double Evaluation()
        {
            return Value;
        }
    }
}
