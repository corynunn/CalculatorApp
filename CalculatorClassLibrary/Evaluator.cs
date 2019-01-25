using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorClassLibrary
{
    public class Evaluator
    {
        //This class iterates through the string building tokens out of the string
        //Fields
        string input;
        int parenthesesBalance;

        //For building the tokens
        OperatorToken token;
        NumberToken numToken;
        OperatorToken openToken;
        OperatorToken closeToken;

        //Location on the input.
        int indexString;
        char currentChar;


        //Properties
        public ITokenable FirstToken { get; private set; }
        public ITokenable CurrentToken { get; private set; }

        public bool IsComplete { get; private set; }
        public double Value { get; private set; }

        //Constructor
        public Evaluator(string input)
        {
            this.input = RemoveWhitespace(input);
            indexString = -1;
            parenthesesBalance = 0;
        }

        //Methods
        public ITokenable Parse()
        {
            while (indexString < input.Length - 1)
            {
                ReadNextChar();
                if (double.TryParse(currentChar.ToString(), out _) || currentChar == '.')
                {
                    //this is a number, use a while loop to build the token, storing the value as a string for now
                    string numberString = "";
                    //numbers can optionally contain a single decimal point, but no more than that, use a bool to track
                    bool isDecimal = false;

                    //if the number input started with a period
                    if (currentChar == '.')
                    {
                        numberString = "0";
                        isDecimal = true;
                    }
                    while ((int.TryParse(currentChar.ToString(), out _) || currentChar == '.') && indexString < input.Length - 1)
                    {
                        numberString += currentChar;
                        ReadNextChar();
                        if (currentChar == '.')
                        {
                            //the user is attempting to add a decilmal, which might be acceptable
                            if (!isDecimal)
                            {
                                //this is the first decimal, that's ok
                                numberString += currentChar;
                                isDecimal = true;
                                if (indexString < input.Length - 1)
                                {
                                    ReadNextChar();
                                }
                            }
                            else
                            {
                                //the user is attempting to add a second decimal to the number, throw an error
                                throw new Exception("You can only have one decimal point in the same number.");
                            }
                        }
                    }
                    //the current character isn't a number, so the numberToken's value is now complete.
                    double.TryParse(numberString, out double value);
                    numToken = new NumberToken(value);
                    AddTokenToChain(numToken);
                }


                switch (currentChar)
                {
                    case '+':
                        //This an addition opperator
                        token = new OperatorToken(Expression.Add);
                        AddTokenToChain(token);
                        break;
                    case 'π':
                    case 'p':
                        //This is adding pi, it will add three token, including open and close parentheses
                        openToken = new OperatorToken(Expression.OpenParenthesis);
                        numToken = new NumberToken(3.1416d);
                        closeToken = new OperatorToken(Expression.CloseParenthesis);

                        AddTokenToChain(openToken);
                        AddTokenToChain(numToken);
                        AddTokenToChain(closeToken);
                        break;
                    case 'e':
                        //This is adding e, it will add three token, including open and close parentheses
                        openToken = new OperatorToken(Expression.OpenParenthesis);
                        numToken = new NumberToken(2.71828d);
                        closeToken = new OperatorToken(Expression.CloseParenthesis);

                        AddTokenToChain(openToken);
                        AddTokenToChain(numToken);
                        AddTokenToChain(closeToken);
                        break;
                    case 'α':
                    case 'a':
                        //This is adding alpha, it will add three token, including open and close parentheses
                        openToken = new OperatorToken(Expression.OpenParenthesis);
                        numToken = new NumberToken(2.5029d);
                        closeToken = new OperatorToken(Expression.CloseParenthesis);

                        AddTokenToChain(openToken);
                        AddTokenToChain(numToken);
                        AddTokenToChain(closeToken);
                        break;
                    case 'δ':
                    case 'd':
                        //This is adding delta, it will add three token, including open and close parentheses
                        openToken = new OperatorToken(Expression.OpenParenthesis);
                        numToken = new NumberToken(4.6692d);
                        closeToken = new OperatorToken(Expression.CloseParenthesis);

                        AddTokenToChain(openToken);
                        AddTokenToChain(numToken);
                        AddTokenToChain(closeToken);
                        break;
                    case 'ζ':
                    case 'z':
                        //This is adding zeta, it will add three token, including open and close parentheses
                        openToken = new OperatorToken(Expression.OpenParenthesis);
                        numToken = new NumberToken(1.20206d);
                        closeToken = new OperatorToken(Expression.CloseParenthesis);

                        AddTokenToChain(openToken);
                        AddTokenToChain(numToken);
                        AddTokenToChain(closeToken);
                        break;
                    case 'φ':
                    case 'f':
                        //This is adding phi, it will add three token, including open and close parentheses
                        openToken = new OperatorToken(Expression.OpenParenthesis);
                        numToken = new NumberToken(1.618d);
                        closeToken = new OperatorToken(Expression.CloseParenthesis);

                        AddTokenToChain(openToken);
                        AddTokenToChain(numToken);
                        AddTokenToChain(closeToken);
                        break;
                    case 'γ':
                    case 'g':
                        //This is adding phi, it will add three token, including open and close parentheses
                        openToken = new OperatorToken(Expression.OpenParenthesis);
                        numToken = new NumberToken(0.57721d);
                        closeToken = new OperatorToken(Expression.CloseParenthesis);

                        AddTokenToChain(openToken);
                        AddTokenToChain(numToken);
                        AddTokenToChain(closeToken);
                        break;
                    case '-':
                        //This a subtraction opperator
                        token = new OperatorToken(Expression.Subtract);
                        AddTokenToChain(token);
                        break;
                    case '*':
                        //This a multiply opperator
                        token = new OperatorToken(Expression.Multiply);
                        AddTokenToChain(token);
                        break;
                    case '/':
                        //This a division opperator
                        token = new OperatorToken(Expression.Divide);
                        AddTokenToChain(token);
                        break;
                    case '^':
                        //This an exponent opperator
                        token = new OperatorToken(Expression.Exponent);
                        AddTokenToChain(token);
                        break;
                    case '(':
                        //This an openParenthesis token, but not really an operator
                        token = new OperatorToken(Expression.OpenParenthesis);
                        AddTokenToChain(token);

                        parenthesesBalance++;
                        break;
                    case ')':
                        //This ac closeParenthesis token
                        token = new OperatorToken(Expression.CloseParenthesis);
                        AddTokenToChain(token);

                        parenthesesBalance--;
                        if (parenthesesBalance < 0)
                        {
                            throw new Exception("You must close all parentheses.");
                        }
                        break;
                    case '=':
                        //Error for bad input
                        if (parenthesesBalance != 0)
                        {
                            throw new Exception("You must close all parentheses.");
                        }
                        //this is the end of the equation
                        Executor executor = new Executor(FirstToken);
                        executor.SimplifyExpression();
                        Value = executor.Value;
                        IsComplete = true;
                        break;
                    default:
                        if (!Int32.TryParse(currentChar.ToString(), out _) && currentChar != '.')
                        {
                            throw new SyntaxError("Invalid input: " + currentChar);
                        }
                        break;
                }


            }
            return CurrentToken;
        }

        /// <summary>
        /// Adds the token to the token chain.
        /// </summary>
        /// <param name="token"></param>
        void AddTokenToChain(ITokenable token)
        {
            //First check if this is the first token in the chain
            if (FirstToken == null)
            {
                FirstToken = token;
            }
            else
            {
                //If this isn't the first token then add it in.
                CurrentToken.NextToken = token;
                token.PreviousToken = CurrentToken;
            }

            //Unary adding logic and error messages for bad input.
            switch (token.Expression)
            {
                case Expression.Exponent:
                    
                case Expression.Multiply:
                    
                case Expression.Divide:
                    if (token.PreviousToken == null)
                    {
                        throw new Exception("Binary operator requires a first operand.");
                    }
                    else if (token.PreviousToken.Expression != Expression.Number && token.PreviousToken.Expression != Expression.CloseParenthesis)
                    {
                        throw new Exception("Binary operator cannot be another binary operator.");
                    }
                    break;
                case Expression.Add:
                    if (token.PreviousToken == null || token.PreviousToken.Expression == Expression.Add || token.PreviousToken.Expression == Expression.Subtract || token.PreviousToken.Expression == Expression.Divide || token.PreviousToken.Expression == Expression.Multiply || token.PreviousToken.Expression == Expression.Exponent || token.PreviousToken.Expression == Expression.OpenParenthesis)
                    {
                        token.Expression = Expression.Positive;
                    }
                    break;
                case Expression.Subtract:
                    if (token.PreviousToken == null || token.PreviousToken.Expression == Expression.Add || token.PreviousToken.Expression == Expression.Subtract || token.PreviousToken.Expression == Expression.Divide || token.PreviousToken.Expression == Expression.Multiply || token.PreviousToken.Expression == Expression.Exponent || token.PreviousToken.Expression == Expression.OpenParenthesis)
                    {
                        token.Expression = Expression.Negative;
                    }
                    break;
                default:
                    break;
            }

            //Set this to the currentToken
            CurrentToken = token;
        }

        string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Reads the current char.
        /// </summary>
        void ReadNextChar()
        {

            indexString++;
            currentChar = input[indexString];

        }

    }
}
