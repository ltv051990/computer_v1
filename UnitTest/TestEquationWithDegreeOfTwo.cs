using NUnit.Framework;
using UnitTest;

namespace Tests
{
    public class Tests : BaseTest
    {
        [Test]
        [TestCase("5 * X^0 + 4 * X^1 - 9.3 * X^2 = 1 * X^0", 164.8, -0.475131, 0.905239)]
        [TestCase("1 * x ^ 2 - 26 * x ^ 1 + 120 * x ^ 0 = 0", 196, 20, 6)]
        [TestCase("2 * x ^ 2 + 1 * x ^ 1 - 3 * x ^ 0 = 0", 25, 1, -1.5)]
        [TestCase("10 * x ^ 2 + 56 * x ^ 1 + 20 * x ^ 0 = 0", 2336, -0.383391, -5.216609)]
        public void TestEquationWithDegreeOfTwoDGreaterZero(string equation, double discriminat, double x1, double x2)
        {
            var received = Calculate(equation);

            var expected = SquareEquationResultDGreaterZero(discriminat, x1, x2);

            Assert.AreEqual(expected, received);
        }

        [Test]
        [TestCase("9 * X^2 - 12 * X^1 + 4 * X^0 = 0", 0.666667)]
        [TestCase("10 * X^2 + 20 * X^1 + 10 * X^0 = 0", -1)]
        public void TestEquationWithDegreeOfTwoDEqualZero(string equation, double x)
        {
            var received = Calculate(equation);

            var expected = SquareEquationResultDEqualZero(x);

            Assert.AreEqual(expected, received);
        }
    }
}