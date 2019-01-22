using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorClassLibrary;

namespace CalculatorClassLibraryTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSimplifySimpleString()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("5+2=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(7, evaluator.Value);
        }

        [TestMethod]
        public void TestIsComplete()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("5+2=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(true, evaluator.IsComplete);
        }

        [TestMethod]
        public void TestNegativeUnaryExpression()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("-5+2");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(Expression.Negative, evaluator.FirstToken.Expression);
        }

        [TestMethod]
        public void TestSimplifyParentheses()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("(5-1)(2(4-2))+(1)=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(17, evaluator.Value);
        }

        [TestMethod]
        public void TestExponent()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("10^3=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(1000, evaluator.Value);
        }

        [TestMethod]
        public void TestPi()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("p2=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(6.2832, evaluator.Value);
        }

        [TestMethod]
        public void TestSubtractions()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("-1+3-1-2+4-2-1=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(0, evaluator.Value);
        }

        [TestMethod]
        public void TestOrderOfOperations()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("(1+1)^3*2-32/2+1=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(1, evaluator.Value);
        }

        [TestMethod]
        public void TestNegativeUnary()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("2*-13=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(-26, evaluator.Value);
        }

        [TestMethod]
        public void TestTokenChain()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("(5)4+3-2");

            //Act
            evaluator.Parse();
            //4th token value should be 4
            int value = (int)evaluator.FirstToken.NextToken.NextToken.NextToken.Evaluation();


            //Assert
            Assert.AreEqual(4, value);
        }

        [TestMethod]
        public void TestDecimals()
        {
            //Interesting aside, the unit tests seem to have issues with asserting doubles.

            //Arrange
            Evaluator evaluator = new Evaluator(".5+0.2+.3=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(1, evaluator.Value);
        }

        [TestMethod]
        public void TestRemoveWhitespaces()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("5 + 3 =");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(8, evaluator.Value);
        }

        [TestMethod]
        public void TestComplexCases1()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("(2+(4(1/2)))(1+1(1+1))=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(12, evaluator.Value);
        }

        [TestMethod]
        public void TestComplexCases2()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("-(7-5(2))+(1+(1))((7-(5-2+(1))))=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(9, evaluator.Value);
        }

        [TestMethod]
        public void TestComplexCases3()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("((3-9)+8)-8=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(-6, evaluator.Value);
        }

        [TestMethod]
        public void TestComplexCases4()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("(4-(2(1)))-(7-5(2))+(1+(1))((7-(5-2+(1))))-5=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(6, evaluator.Value);
        }

        [TestMethod]
        public void TestComplexCases5()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("(((1))(2))=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(2, evaluator.Value);
        }

        [TestMethod]
        public void TestComplexCases6()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("(((2)))=");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(2, evaluator.Value);
        }

        [TestMethod]
        public void TestComplexCases7()
        {
            //Arrange
            Evaluator evaluator = new Evaluator("(((3)))((4 - (2(1))) - (7 - 5(2)) + (1 + (1))((7 - (5 - 2 + (1)))) - 5) + 2 =");

            //Act
            evaluator.Parse();

            //Assert
            Assert.AreEqual(20, evaluator.Value);
        }


    }
}
