using System;
using System.Collections.Generic;
using System.Linq;
using System.Regex;

namespace computor_v1
{
    public class EquationInfo
    {
        public EquationInfo(List<CoeficientInfo> xCoeficients)
        {
            Coeficients = xCoeficients ?? throw new ArgumentNullException(nameof(xCoeficients));
        }

        public List<CoeficientInfo> Coeficients { get; }

        public void AddCoeficient(int coeficient, int degree) => Coeficients.Add(new CoeficientInfo(coeficient, degree));

        public static EquationInfo Empty
            => new EquationInfo(new CoeficientInfo[0].ToList());
    }

    public class CoeficientInfo
    {
        public CoeficientInfo(int coeficient, int degree)
        {
            Coeficient = coeficient;
            Degree = degree;
        }

        public int Coeficient { get; }
        public int Degree { get; }

        public override string ToString() => $"{Coeficient} * X^{Degree}";
    }

    public static class Converters
    {
        private static readonly string fullPattern = @"(?<left>([+-]?\d+\*x\^\d+){1,}|0)=(?<right>([+-]?\d+\*x\^\d+){1,}|0)";
        private static readonly string expressionPattern = @"[+-]?\d+\*x\^\d+";
        private static readonly string operandPattern = $"(?<operand>{expressionPattern})";
        private static readonly string zeroPattern = "0";

        public static EquationInfo ToEquationInfo(this string input)
        {
            var parts = Regex.Match(input, fullPattern, RegexOptions.IgnoreCase);
            var left = parts.ToGroupCollection("left");
            var right = parts.ToGroupCollection("right");

            return EquationInfo.Empty
                               .ParseLeftPart(left)
                               .ParseRightPart(right);
        }

        private static List<string> ToGroupCollection(this Match match, string groupName)
        {
            if (string.IsNullOrWhiteSpace(match?.Groups?[groupName]?.Value))
            {
                throw new ArgumentException($"Invalid {nameof(groupName)} -> {groupName}");
            }

            var stringFromGroups = match.Groups[groupName].Value;

            var operandParse = Regex.Matches(stringFromGroups, operandPattern, RegexOptions.IgnoreCase);

            if (operandParse != null)
            {
                var result = operandParse.Cast<Group>()
                                      .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                                      .Select(x => x.Value)
                                      .ToList();
                
                return result;
            }

            if (!Regex.IsMatch(stringFromGroups, zeroPattern))
                throw new ArgumentException("Invalid { nameof(groupName) }-> { groupName}");

            return new List<string>();
        }
    }

    public static class EquationInfoExtensions
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

            var pattern = $"(?<{SIGN}>[+-]?)(?<{COEFICIENT}>\\d+)\\*x\\^(?<{DEGREE}>\\d+)";

            groups.ForEach(str =>
            {
                var operandParse = Regex.Match(str, pattern, RegexOptions.IgnoreCase);

                var signStr = operandParse?.Groups?[SIGN]?.Value ?? "+";
                var coeficientStr = operandParse.Groups[COEFICIENT].Value;
                var degreeStr = operandParse.Groups[DEGREE].Value;

                var signCoeficient = 1;
                switch(signStr)
                {
                    case "" :
                    case "+" :
                        signCoeficient = 1;
                        break;
                    case "-":
                        signCoeficient = -1;
                        break;
                }

                var degree = int.Parse(degreeStr);
                var coeficient = int.Parse(coeficientStr);

                var coeficientWithSign = coeficient * signCoeficient * singCoef;

                equationInfo.AddCoeficient(coeficientWithSign, degree);
            });

            return equationInfo; 
        }
    }

    public class Program
    {
        public static string ValidateInput(string input)
        {
            var result = input.ToEquationInfo();

            return string.Empty;
        }

        public static void Main(string[] args)
        {
            var input = "8*X^0-6*X^1+0*X^2-5.6*X^3=3*X^0";

            var bla = input.ToEquationInfo();
            var result = ValidateInput(input);
        }
    }
}
