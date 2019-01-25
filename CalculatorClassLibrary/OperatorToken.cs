using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class OperatorToken : ITokenable
    {
        public ITokenable PreviousToken { get; set; }
        public ITokenable NextToken { get; set; }

        public Expression Expression { get; set; }

        //Needs to know if it is addition, subtraction, ect.
        public OperatorToken(Expression expression)
        {
            Expression = expression;
        }

        public double Evaluation()
        {
            double value = 0;
            switch (this.Expression)
            {
                case Expression.Add:
                    value = PreviousToken.Evaluation() + NextToken.Evaluation();
                    break;
                case Expression.Subtract:
                    value = PreviousToken.Evaluation() - NextToken.Evaluation();
                    break;
                case Expression.Multiply:
                    value = PreviousToken.Evaluation() * NextToken.Evaluation();
                    break;
                case Expression.Divide:
                    if(NextToken.Evaluation() == 0)
                    {
                        throw new RuntimeError("You attempted to divide by zero.");
                    }
                    value = PreviousToken.Evaluation() / NextToken.Evaluation();
                    break;
                case Expression.Positive:
                    value = NextToken.Evaluation();
                    break;
                case Expression.Negative:
                    value = NextToken.Evaluation() * -1;
                    break;
                case Expression.Exponent:
                    value = Math.Pow(PreviousToken.Evaluation(), NextToken.Evaluation());
                    break;
                case Expression.OpenParenthesis:
                    value = NextToken.Evaluation();
                    break;
                default:
                    break;
            }

            return value;
        }
    }
}
