using System;
using System.Collections.Generic;
using System.Linq;
using computor_v1.Infratructure;

namespace computor_v1
{
    public class EquationInfo
    {
        public EquationInfo(List<CoeficientInfo> xCoeficients)
        {
            Coeficients = xCoeficients ?? throw new ArgumentNullException(nameof(xCoeficients));
        }

        public List<CoeficientInfo> Coeficients { get; }

        public void AddCoeficient(double coeficient, int degree) => Coeficients.Add(new CoeficientInfo(coeficient, degree));

        public static EquationInfo Empty
            => new EquationInfo(new CoeficientInfo[0].ToList());

        public int MaxDegree
            => Coeficients
                .Select(x => x.Degree)
                .Max();

        public IEnumerable<int> GetDegreeCollection()
            => Coeficients
                .Select(x => x.Degree)
                .Distinct()
                .OrderBy(x => x)
                .AsEnumerable();
    
        public CoeficientInfo GetCoeficientInfo(int degree, double reduceIfNone)
            => GetCoeficientInfoByDegree(degree)
               .Map(x => x)
               .Reduce(() => new CoeficientInfo(reduceIfNone, degree));

        public Option<CoeficientInfo> GetCoeficientInfoByDegree(int degree)
        {
            var coeficients = Coeficients
            .Where(x => x.Degree == degree)
            .Select(x => x.Coeficient)
            .AsEnumerable();

            if (!coeficients.Any())
            {
                return None.Value; 
            }

            return new CoeficientInfo(coeficients.Sum(), degree);
        }

    }
}
