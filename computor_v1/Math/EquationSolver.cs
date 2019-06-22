using static computor_v1.Math.MathFunctions;

namespace computor_v1.Math
{
    public interface IEquationSolver
    {
        string Solve(EquationInfo equationInfo);
    }

    public abstract class BaseEquationSolver
    {
        protected int ReduceIfNone { get; } = 0;
    }

    public class ZeroDegreeSolver : BaseEquationSolver, IEquationSolver
    {
        /// <summary>
        /// / Solve Equation when max degree equal zero
        /// </summary>
        /// <returns>The string with solving.</returns>
        /// <param name="equationInfo">Equation info.</param>
        public string Solve(EquationInfo equationInfo)
        {
            var coef_C = equationInfo.GetCoeficientInfo(0, ReduceIfNone).Coeficient;

            return SolveZeroDegreeEquation(coef_C);
        }
    }

    public class UnoDegreeSolver : BaseEquationSolver, IEquationSolver
    {
        /// <summary>
        /// / Solve Equation when max degree equal one
        /// </summary>
        /// <returns>The string with solving.</returns>
        /// <param name="equationInfo">Equation info.</param>
        public string Solve(EquationInfo equationInfo)
        {
            var coef_B = equationInfo.GetCoeficientInfo(1, ReduceIfNone).Coeficient;
            var coef_C = equationInfo.GetCoeficientInfo(0, ReduceIfNone).Coeficient;

            return SolveLinialEquation(coef_B, coef_C);
        }
    }

    public class SquareDegreeSolver : BaseEquationSolver, IEquationSolver
    {
        /// <summary>
        /// / Solve Equation when max degree equal Two
        /// </summary>
        /// <returns>The string with solving.</returns>
        /// <param name="equationInfo">Equation info.</param>
        public string Solve(EquationInfo equationInfo)
        {
            var coef_A = equationInfo.GetCoeficientInfo(2, ReduceIfNone).Coeficient;
            var coef_B = equationInfo.GetCoeficientInfo(1, ReduceIfNone).Coeficient;
            var coef_C = equationInfo.GetCoeficientInfo(0, ReduceIfNone).Coeficient;

            return SolveSquareEquation(coef_A, coef_B, coef_C);
        }
    }

    public static class EquationSolverFactory
    {
        public static IEquationSolver CreateSolver(int degree)
        {
            switch(degree)
            {
                case 0:
                {
                    return new ZeroDegreeSolver();
                }
                case 1:
                {
                    return new UnoDegreeSolver();
                }
                case 2:
                {
                    return new SquareDegreeSolver();
                }
                case var e when (degree < 0):
                {
                    throw new DomainValidationException("Invalid input!!! Degree is less then 0");
                }
                default:
                {
                    throw new DomainValidationException($"The polynomial degree is stricly greater than 2 (--> {degree} <--), I can't solve.");
                }
            }
        }
    }
}
