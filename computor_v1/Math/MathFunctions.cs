using static System.Environment;
using static System.MathF;

namespace computor_v1.Math
{
    public static class MathFunctions
    {
        private const int digitsToRound = 6;
        /// <summary>
        /// Solves the square equation.
        /// </summary>
        /// <returns>The square equation.</returns>
        /// <param name="a">coeficient when x ^ 2</param>
        /// <param name="b">coeficient when x ^ 1</param>
        /// <param name="c">coeficient when x ^ 0</param>
        public static string SolveSquareEquation(double a, double b, double c)
        {
            var discriminant = Pow((float)b, 2) - 4 * a * c;

            if (discriminant > 0)
            {
                var x1 = (-b + Sqrt((float)discriminant)) / (2 * a);
                var x2 = (-b - Sqrt((float)discriminant)) / (2 * a);
                
                return $"Discriminant -> {discriminant} <- is strictly positive, the two solutions are: {NewLine}"
                    + $"x1 = {Round((float)x1, digitsToRound)}{NewLine}"
                    + $"x2 = {Round((float)x2, digitsToRound)}{NewLine}";
            }

            if (0 == (int)discriminant)
            {
                var x = -b / (2 * a);
                
                return $"Discriminant -> {discriminant} <- is equal zero. The solution is:{NewLine}"
                    + $"x = {Round((float)x, digitsToRound)}{NewLine}";
            }

            // if D < 0
            var k = b / 2;
            var sqrt = Sqrt((float)(-Pow((float)b, 2) + (4 * a * c)));

            var x_1 = (-k + sqrt) / 2 * a;
            var x_2 = (-k - sqrt) / 2 * a;

            return $"Discriminant -> {discriminant} <- is less than zero."
                + " So it does not have any real issues."
                + " But there is a solution with complex numbers."
                + $" The two solutions are: {NewLine}"
                + $"x1 = {Round((float)x_1, digitsToRound)}{NewLine}"
                + $"x2 = {Round((float)x_2, digitsToRound)}{NewLine}";
        }

        /// <summary>
        /// Solves the square equation.
        /// </summary>
        /// <returns>The square equation.</returns>
        /// <param name="b">coeficient when x ^ 1</param>
        /// <param name="c">coeficient when x ^ 0</param>
        public static string SolveLinialEquation(double b, double c)
        {
            if ((int)b == 0 && (int)c == 0)
            {
                return $"It is a linial equation with type ax=b.{NewLine} As a = {c} and b = {b}.{NewLine} The solve is any number";
            }

            if ((int)b == 0 && (int)c != 0)
            {
                return $"It is a linial equation with type ax=b.{NewLine} As a = {b} and b = {c}.{NewLine} The equation has no roots";
            }

            var x = (-c) / b;

            return $"It is a linial equation with type ax=b.{NewLine} As a = {b} and b = {c}.{NewLine} The solve is:"
                + $"x = {Round((float)x, digitsToRound)}";
        }

        public static string SolveZeroDegreeEquation(double c)
        {
            if ((int)c == 0)
            {
                return "The solve is any number";
            }

            return "The equation has no roots";
        }
    }
}
