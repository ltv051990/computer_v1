using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace computor_v1
{
    public static class EquationInfoConverters
    {
        private static readonly string fullPattern = @"^(?<left>([+-]?(\d*\.?\d*)\*x\^\d+){1,}|0)=(?<right>([+-]?(\d*\.?\d*)\*x\^\d+){1,}|0)$";

        private static readonly string expressionPattern = @"[+-]?(\d*\.?\d*)\*x\^\d+";

        private static readonly string operandPattern = $"(?<operand>{expressionPattern})";

        private static readonly string zeroPattern = "0";

        public static EquationInfo ToEquationInfo(this string input)
        {
            if (input.Equals("0=0"))
            {
                throw new DomainValidationException($"Invalid input -> {input}");
            }

            var parts = Regex.Match(input, fullPattern, RegexOptions.IgnoreCase);

            if (!parts.Success)
            {
                throw new DomainValidationException($"Invalid input -> {input}");
            }

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
                throw new DomainValidationException($"Invalid {nameof(groupName)} -> {groupName}");
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
                throw new DomainValidationException("Invalid { nameof(groupName) }-> { groupName}");

            return new List<string>();
        }
    }
}
