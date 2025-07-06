using NUnit.Framework;
using System;
using CalcLibrary;


namespace CalcLibrary_Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private SimpleCalculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new SimpleCalculator();
        }

        [TearDown]
        public void Teardown()
        {
            calculator.AllClear();
            calculator = null;
        }

        [Test]
        [TestCase(3, 2, 5)]
        public void Test_Addition(double a, double b, double expected)
        {
            var result = calculator.Addition(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(10, 2, 5)]
        public void Test_Division(double a, double b, double expected)
        {
            var result = calculator.Division(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(0.001));
        }

        [Test]
        public void Test_DivideByZero_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Division(4, 0));
            Assert.That(ex.Message, Is.EqualTo("Second Parameter Can't be Zero"));
        }
    }
}
