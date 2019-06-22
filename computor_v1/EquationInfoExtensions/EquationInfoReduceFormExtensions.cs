using System;
using System.Collections.Generic;
using System.Linq;
using computor_v1.Infratructure;

namespace computor_v1
{
    public static class EquationInfoReduceFormExtensions
    {
        public static EquationInfo PrintReduceForm(this EquationInfo equationInfo)
        {
            var colection = equationInfo
                .ToCollection();

            ConsolePrinter.Info($"Reduced Form: {PrintFirst(colection)}{PrintOther(colection)} = 0{Environment.NewLine}");

            return equationInfo;
        }

        private static string PrintFirst(this IReadOnlyCollection<CoeficientInfo> coeficientInfos)
        {
            var coefInfo = coeficientInfos.First();

            return PrintByForm(string.Empty, string.Empty, coefInfo);
        }

        private static string PrintOther(this IReadOnlyCollection<CoeficientInfo> coeficientInfos)
        {
            string result = string.Empty;

            coeficientInfos
                .Skip(1)
                .ToList()
                .ForEach(x => result += PrintByForm(" ", "+ ", x));

            return result;
        }

        private static string PrintByForm(string padding, string replaceSign, CoeficientInfo coefInfo)
        {
            var sign = coefInfo.Coeficient >= 0
                ? replaceSign
                : "- ";

            return $"{padding}{sign} {coefInfo.ToPrintForm()}";
        }

        private static IReadOnlyCollection<CoeficientInfo> ToCollection(this EquationInfo equationInfo)
        {
            var colection = new List<CoeficientInfo>();

            var degrees = equationInfo
                .GetDegreeCollection()
                .ToList();

            degrees
                .ForEach(x => colection.TryAdd(equationInfo.GetCoeficientInfoByDegree(x)));

            return colection;
        }

        private static List<CoeficientInfo> TryAdd(this List<CoeficientInfo> colection, Option<CoeficientInfo> coeficientInfo)
        {
            coeficientInfo
                .Map(x =>
                 {
                     colection.Add(x);

                     return true;
                 })
                 .Reduce(false);

            return colection;
        }
    }
}
