using NUnit.Framework;

namespace UnitTest
{
    public class LinialEquationTests : BaseTest
    {
        [Test]
        [TestCase("-8 * X^0 + 3 * X^1 = 1 * X^0", 3, -9, 3)]
        [TestCase("10 * X^0 + 2 * X^0 + 3 * X^1 = 0", 3, 12, -4)]
        public void LinialEquationTest(string equation, double b, double c, double x)
        {
            var received = Calculate(equation);

            var expected = LinialEquation(b, c, x);

            Assert.AreEqual(expected, received);
        }
    }
}
