using System;
using System.Collections.Generic;
using System.Linq;
using System.Regex;

namespace computor_v1
{
    public class Degree
    {
        public Degree(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static Degree X0 => new Degree(0);

        public static Degree X1 => new Degree(1);

        public static Degree X2 => new Degree(2);
    }

    public class EquationInfo
    {
        public EquationInfo(DegreeInfo degreeInfo, IEnumerable<XCoeficient> xCoeficients)
        {
            XCoeficients = xCoeficients ?? throw new ArgumentNullException(nameof(xCoeficients));
            DegreeInfo = degreeInfo ?? throw new ArgumentNullException(nameof(degreeInfo));
        }

        public DegreeInfo DegreeInfo { get; }

        public IEnumerable<XCoeficient> XCoeficients { get; }

        public static EquationInfo Empty
            => new EquationInfo(new DegreeInfo(), new XCoeficient[0].AsEnumerable());
    }

    public class DegreeInfo
    {
        public DegreeInfo()
        {
        }

        public DegreeInfo(List<Degree> degrees)
        {
            Degrees = degrees;
        }

        private readonly List<Degree> Degrees = new List<Degree>();

        public int MaxDegree => Degrees.Select(XCoeficient => XCoeficient.Value).AsEnumerable().Max();

        public int MinDegree => Degrees.Select(XCoeficient => XCoeficient.Value).AsEnumerable().Min();

        public void AddDegree(Degree value) => Degrees.Add(value);

        public static DegreeInfo Copy(DegreeInfo info) => new DegreeInfo(info.Degrees.ToList());
    }

    public class XCoeficient
    {
        private XCoeficient(Degree xType)
        {
            XType = xType;
        }

        private readonly List<int> Coeficients = new List<int>();

        public int Coeficient => Coeficients.Sum();

        public void AddCoeficient(int value) => Coeficients.Add(value);

        public Degree XType { get; }

        public override string ToString() => $"{Coeficient} * X^{XType}";

        public static XCoeficient CreateX0 => new XCoeficient(Degree.X0);

        public static XCoeficient CreateX1 => new XCoeficient(Degree.X1);

        public static XCoeficient CreateX2 => new XCoeficient(Degree.X2);
    }

    public static class Converters
    {
        private static readonly string fullPattern = @"(?<left>([+-]?\d+\*x\^\d+){1,}|0)=(?<right>([+-]?\d+\*x\^\d+){1,}|0)";
        private static readonly string expressionPattern = @"[+-]?\d+\*x\^\d+";
        private static readonly string operandPattern = $"(?<operand>{expressionPattern})";

        public static EquationInfo ToEquationInfo(this string input)
        {
            var leftPart = Regex.Match(input, fullPattern, RegexOptions.IgnoreCase);
            var left = leftPart.Groups["left"];

            var leftParse = Regex.Matches(left, operandPattern, RegexOptions.IgnoreCase);
            var lef = leftParse.Cast<Group>()
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value)
                .ToList();

            var rightPart = Regex.Match(input, fullPattern, RegexOptions.IgnoreCase);
            var right = rightPart.Groups["right"];

            return EquationInfo.Empty
                               .ParseLeftPart()
                               .ParseRightPart();
        }

        private static List<string> ToGroupCollection(this Match match, string groupName)
        {
            if (string.IsNullOrWhiteSpace(match?.Groups?[groupName]?.Value))
            {
                throw new ArgumentException($"Invalid {nameof(groupName)} -> {groupName}");
            }

            var stringFromGroups = match.Groups[groupName].Value;

            var leftParse = Regex.Matches(stringFromGroups, operandPattern, RegexOptions.IgnoreCase);

            var result = leftParse.Cast<Group>()
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value)
                .ToList();

            return result;
        }
    }

    public static class EquationInfoExtensions
    {
        public static EquationInfo ParseLeftPart(this EquationInfo equationInfo, List<Group> groups)
            => CreateInfoFromRegex(equationInfo, groups, 1);

        public static EquationInfo ParseRightPart(this EquationInfo equationInfo, List<Group> groups)
            => CreateInfoFromRegex(equationInfo, groups, -1);

        private static EquationInfo CreateInfoFromRegex(this EquationInfo equationInfo, List<Group> groups, int coeficient)
        {
           
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
            var input = "10*X^2+20*X^3=10*X^2+20*X^3";

            var bla = input.ToEquationInfo();
            var result = ValidateInput(input);
        }
    }
}
