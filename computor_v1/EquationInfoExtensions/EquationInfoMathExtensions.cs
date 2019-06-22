using System;
using computor_v1.Infratructure;
using computor_v1.Math;

namespace computor_v1.EquationInfoExtensions
{
    public static class EquationInfoMathExtensions
    {
        public static string SolveEquation(this EquationInfo equationInfo)
        {
            var maxDegree = equationInfo.MaxDegree;

            var equationSolver = EquationSolverFactory.CreateSolver(maxDegree);

            var result = equationSolver.Solve(equationInfo);

            return result;
        }
    }
}
