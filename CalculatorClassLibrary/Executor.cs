using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class Executor
    {
        ITokenable firstToken;
        ITokenable currentToken;

        public double Value { get; private set; }

        public Executor(ITokenable firstToken)
        {
            this.firstToken = firstToken;
        }

        public ITokenable SimplifyExpression()
        {
            currentToken = firstToken;
            //Since the order of operations are stored as enums we can use this value to track which expressions
            //need to be worked on now. The max value is the last operator on the enum list.
            int orderOfOperations = 0;
            int maxOperation = (int)Enum.GetValues(typeof(Expression)).Cast<Expression>().Max() - 3;

            //A first pass must be made to check for open parenthesis
            while (currentToken != null && currentToken.Expression != Expression.CloseParenthesis)
            {
                if (currentToken.Expression == Expression.OpenParenthesis)
                {
                    //A new open parenthesis has been found, send the next token to avoid infinite method calls
                    Executor executor = new Executor(currentToken.NextToken);
                    ITokenable newToken = executor.SimplifyExpression();
                    if (newToken.PreviousToken == null)
                    {
                        firstToken = newToken;
                    }

                    currentToken = newToken;

                    //If this is true we just came out of parentheses, they should have been removed from the chain so this resets the chain
                    if (firstToken.Expression == Expression.OpenParenthesis)
                    {
                        firstToken = currentToken;
                    }
                }
                currentToken = currentToken.NextToken;
            }

            //Now we will loop through the order of operations.
            do
            {
                currentToken = firstToken;
                while (currentToken != null && currentToken.Expression != Expression.CloseParenthesis)
                {
                    //Check each token to see if the expression equals the current order of operation
                    if ((int)currentToken.Expression == orderOfOperations)
                    {
                        //The operator matches the current operator that we are looking for.
                        //Binary and unary operations are handled differently
                        if (orderOfOperations > 1)
                        {
                            //A binary operation acts on the two numbers before and after it, changing the value of the
                            //previous number and then removing the operator and the second number from the link list of nodes.
                            NumberToken firstOperand = (NumberToken)currentToken.PreviousToken;
                            firstOperand.Value = currentToken.Evaluation();
                            firstOperand.NextToken = currentToken.NextToken.NextToken;
                            if (firstOperand.NextToken != null)
                            {
                                firstOperand.NextToken.PreviousToken = firstOperand;
                            }
                            currentToken = firstOperand;
                        }
                        else
                        {
                            //This is a unary operation, the operand will be updated and this token removed from the link list
                            NumberToken operand = (NumberToken)currentToken.NextToken;
                            operand.Value = currentToken.Evaluation();
                            operand.PreviousToken = currentToken.PreviousToken;
                            if (operand.PreviousToken != null)
                            {
                                operand.PreviousToken.NextToken = operand;
                            }
                            //If the unary operator was the first token in the chain we need to update that
                            if (firstToken == currentToken)
                            {
                                firstToken = operand;
                            }
                            currentToken = operand;
                        }
                    }
                    currentToken = currentToken.NextToken;
                }
                //Iterate to the next order of operation
                orderOfOperations++;
            } while (orderOfOperations <= maxOperation);

            //At this point there is either only one token in the link chain or we have parentheses that need cleaning up
            if(firstToken.Expression == Expression.OpenParenthesis && firstToken.PreviousToken == null)
            {
                firstToken = firstToken.NextToken;
                firstToken.PreviousToken = null;
            }

            if (firstToken.PreviousToken != null && firstToken.PreviousToken.Expression == Expression.OpenParenthesis)
            {
                //This code cleans up the open parenthesis

                

                //If a number is next to a parenthesis then the parenthesis needs to be converted into a multiply operator,
                //if an operator is there then just delete this parenthesis from the chain, if it is null then delete this token.
                if (firstToken.PreviousToken.PreviousToken != null)
                {
                    if (firstToken.PreviousToken.PreviousToken.Expression != Expression.Number)
                    {
                        //Delete parenthesis token, linking the next number token into the chain.
                        firstToken.PreviousToken = firstToken.PreviousToken.PreviousToken;
                        firstToken.PreviousToken.NextToken = firstToken;
                    }
                    else
                    {
                        //The previous token is a number, convert the parenthesis token into a multiply token
                        firstToken.PreviousToken.Expression = Expression.Multiply;
                    }
                }
                else
                {
                    //Delete parenthesis token, there is nothing before it.
                    firstToken.PreviousToken = null;
                }

                //Clean up the close parenthesis

                //Now find the close parenthesis (either the second or third token in the sub-chain we are working on) and apply similar logic
                currentToken = firstToken;
                while (currentToken.Expression != Expression.CloseParenthesis)
                {
                    currentToken = currentToken.NextToken;
                }

                if (currentToken.NextToken != null)
                {
                    if (currentToken.NextToken.Expression == Expression.Number || currentToken.NextToken.Expression == Expression.OpenParenthesis)
                    {
                        currentToken.Expression = Expression.Multiply;
                    }
                    else
                    {
                        //The next token is an operator, just delete this
                        currentToken.PreviousToken.NextToken = currentToken.NextToken;
                        currentToken.NextToken.PreviousToken = currentToken.PreviousToken;
                        currentToken.NextToken = null;
                        currentToken.PreviousToken = null;
                    }
                }
                else
                {
                    //This is the end of the chain
                    currentToken.PreviousToken.NextToken = null;
                }
            }

            Value = firstToken.Evaluation();


            return firstToken;
        }
    }
}
