using computor_v1;
using computor_v1.Converters;
using computor_v1.EquationInfoExtensions;
using static System.Environment;
using static System.MathF;

namespace UnitTest
{
    public abstract class BaseTest
    {
        private const int digitsToRound = 6;

        protected string Calculate(string input)
            => input
                .ToStringWitoutSpaces()
                .ToEquationInfo()
                .PrintReduceForm()
                .ValidateDegree()
                .SolveEquation();

        protected string SquareEquationResultDGreaterZero(double discriminant, double x1, double x2)
            => 
                 $"Discriminant -> {discriminant} <- is strictly positive, the two solutions are: {NewLine}"
                + $"x1 = {Round((float)x1, digitsToRound)}{NewLine}"
                + $"x2 = {Round((float)x2, digitsToRound)}{NewLine}";

        protected string SquareEquationResultDEqualZero(double x)
            =>
                $"Discriminant -> {0} <- is equal zero. The solution is:{NewLine}"
                    + $"x = {Round((float)x, digitsToRound)}{NewLine}";

        protected string SquareEquationResultDLessZero(double discriminant, double x1, double x2)
            =>
                $"Discriminant -> {discriminant} <- is less than zero."
                    + " So it does not have any real issues."
                    + " But there is a solution with complex numbers."
                    + $" The two solutions are: {NewLine}"
                    + $"x1 = {Round((float)x1, digitsToRound)}{NewLine}"
                    + $"x2 = {Round((float)x2, digitsToRound)}{NewLine}";

        protected string LinialEquationBCEqualZero(double b, double c)
            =>
                $"It is a linial equation with type ax=b.{NewLine} As a = {c} and b = {b}.{NewLine} The solve is any number";

        protected string LinialEquationOnlyBEqualZero(double b, double c)
            =>
                $"It is a linial equation with type ax=b.{NewLine} As a = {b} and b = {c}.{NewLine} The equation has no roots";

        protected string LinialEquation(double b, double c, double x)
            =>
                $"It is a linial equation with type ax=b.{NewLine} As a = {b} and b = {c}.{NewLine} The solve is:"
                + $"x = {Round((float)x, digitsToRound)}";

        protected string SimpleLinialEquationEqualZero(double c)
            => 
                "The solve is any number";

        protected string SimpleLinialEquation(double c)
            =>
                "The equation has no roots";
    }
}
