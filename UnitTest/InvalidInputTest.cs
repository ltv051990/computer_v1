using computor_v1;
using NUnit.Framework;

namespace UnitTest
{
    public class InvalidInputTest : BaseTest
    {
        [Test]
        [TestCase("5 * X^0 + 4 * X^1 - 9.3 * X^2 == 1 * X^0")]
        [TestCase("5 * X^0 + 4 * X^1 - 9.3 * X^2 = 1 * X^0ads")]
        [TestCase("asdadasd5 * X^0 + 4 * X^1 - 9.3 * X^2 = 1 * X^0ads")]
        [TestCase("")]
        [TestCase("0 = 0")]
        [TestCase("=")]
        [TestCase("0 = -1")]
        [TestCase("9 + -5 = -5 - 9")]
        [TestCase("0 = 4 * x ^ 5 = 5")]
        [TestCase("4 * x ^ 5 - 23 * x ^ 2")]
        [TestCase("4 * x ^ 5 - 23 * x ^ 2 =")]
        [TestCase("  = 4 * x ^ 5 - 23 * x ^ 2")]
        [TestCase("2.1 * 5 * x^2 - 5.84 * x^2 + 12 = -x")]
        [TestCase("2.1 * x^2 - 5.84 * x^2 + 8,5 * x = 0")]
        [TestCase("2.1 * y^2 - 7.05 * x^2 + 8.5 * x = 75 * x^2")]
        [TestCase("2.84 * x^2 - 12.84 * x^2 + 12*x = + -x")]
        [TestCase("0.58 * x^2 - 10 * x^2 + -58 * x = 75 * x^3")]
        public void TestEquationWithDegreeOfTwo(string invaildInput)
        {
            Assert.Throws<DomainValidationException>(() =>
            {
                var received = Calculate(invaildInput);
            });
        }
    }
}
