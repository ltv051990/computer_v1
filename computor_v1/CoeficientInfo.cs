using System;

namespace computor_v1
{
    public class CoeficientInfo
    {
        public CoeficientInfo(double coeficient, int degree)
        {
            Coeficient = coeficient;
            Degree = degree;
        }

        public double Coeficient { get; }
        public int Degree { get; }

        public string ToPrintForm()
            => $"{(Coeficient < 0 ? Coeficient * -1 : Coeficient)} * X ^ {Degree}";
    }
}
