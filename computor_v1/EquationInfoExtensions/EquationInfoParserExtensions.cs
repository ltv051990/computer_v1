using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace computor_v1
{
    public static class EquationInfoParserExtensions
    {
        public static EquationInfo ParseLeftPart(this EquationInfo equationInfo, List<string> groups)
            => CreateInfoFromRegex(equationInfo, groups, 1);

        public static EquationInfo ParseRightPart(this EquationInfo equationInfo, List<string> groups)
            => CreateInfoFromRegex(equationInfo, groups, -1);

        private static EquationInfo CreateInfoFromRegex(this EquationInfo equationInfo, List<string> groups, int singCoef)
        {
            const string SIGN = "sign";
            const string COEFICIENT = "coeficient";
            const string DEGREE = "degree";

            var pattern = $"(?<{SIGN}>[+-]?)(?<{COEFICIENT}>\\d*\\.?\\d*)\\*x\\^(?<{DEGREE}>\\d+)";

            groups.ForEach(str =>
            {
                var operandParse = Regex.Match(str, pattern, RegexOptions.IgnoreCase);

                var signStr = operandParse?.Groups?[SIGN]?.Value ?? "+";
                var coeficientStr = operandParse.Groups[COEFICIENT].Value;
                var degreeStr = operandParse.Groups[DEGREE].Value;

                var signCoeficient = GetSignCoeficient(signStr);
                var degree = int.Parse(degreeStr);
                var coeficient = double.Parse(coeficientStr);
                var coeficientWithSign = coeficient * signCoeficient * singCoef;

                equationInfo.AddCoeficient(coeficientWithSign, degree);
            });

            return equationInfo;
        }

        private static int GetSignCoeficient(string signStr)
        {
            switch (signStr)
            {
                case "":
                case "+":
                    return 1;
                case "-":
                    return -1;
                default:
                    throw new ArgumentException($"Invalid {nameof(signStr)} with value {signStr}");
            }
        }
    }
}