using System;
using computor_v1.Converters;
using computor_v1.EquationInfoExtensions;
using computor_v1.Infratructure;

namespace computor_v1
{
    public class Program
    {
        private const string Start = "Computor_v1 started\n";

        private const string StringForStart =

            "         ______________\n" +
            "        /             /|\n" +
            "       /             / |\n" +
            "      /____________ /  |\n" +
            "     | ___________ |   |\n" +
            "     ||           ||   |\n" +
            "     ||           ||   |\n" +
            "     ||           ||   |\n" +
            "     ||___________||   |\n" +
            "     |   _______   |  /\n" +
            "    /|  (_______)  | /\n" +
            "   ( |_____________|/\n" +
            "    \\\n.=======================.\n" +
            "| ::::::::::::::::  ::: |\n" +
            "| ::::::::::::::[]  ::: |\n" +
            "|   -----------     ::: |\n" +
            "'-----------------------'\n";

        public static void Main(string[] args)
        {
            ConsolePrinter.Info(Start);
            ConsolePrinter.AppInfo(StringForStart);

            while (true)
            {
                ConsolePrinter.WaitForInput();

                var input = Console.ReadLine();

                if (input.Equals("Q") || input.Equals("q"))
                    break;

                try
                {
                    var result = input
                                    .ToStringWitoutSpaces()
                                    .ToEquationInfo()
                                    .PrintReduceForm()
                                    .PrintPolinomicalDegree()
                                    .ValidateDegree()
                                    .SolveEquation();

                    ConsolePrinter.Result(result);
                }
                catch (DomainException e)
                {
                    ConsolePrinter.Error(e.ExceptionInfo);
                }
            }
        }
    }
}