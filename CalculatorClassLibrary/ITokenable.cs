using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public interface ITokenable
    {
        ITokenable PreviousToken { get; set; }
        ITokenable NextToken { get; set; }

        Expression Expression { get; set; }

        double Evaluation();
    }
}
