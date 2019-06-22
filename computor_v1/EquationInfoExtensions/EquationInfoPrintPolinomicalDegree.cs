using System;
using computor_v1.Infratructure;

namespace computor_v1.EquationInfoExtensions
{
    public static class EquationInfoPrintPolinomicalDegree
    {
        public static EquationInfo PrintPolinomicalDegree(this EquationInfo equationInfo)
        {
            ConsolePrinter.Info($"Polynomial degree: {equationInfo.MaxDegree}");

            return equationInfo;
        }
    }
}
