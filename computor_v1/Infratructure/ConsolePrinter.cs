using System;

namespace computor_v1.Infratructure
{
    public static class ConsolePrinter
    {
        private const string WAITING_FOR_EQUATION = "Waiting for equation\n";

        public static void Error(string result)
            => Print(result, ConsoleColor.Red);

        public static void Result(string result)
            => Print(result, ConsoleColor.Green);

        public static void Info(string result)
            => Print(result, ConsoleColor.Blue);

        public static void AppInfo(string result)
            => Print(result, ConsoleColor.Yellow);

        public static void WaitForInput()
            => Print(WAITING_FOR_EQUATION, ConsoleColor.Gray);

        private static void Print(string result, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;

            Console.WriteLine(result);

            Console.ResetColor();
        }
    }
}
