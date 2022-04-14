using System;
using TheSphinx.TheSphinxConsole.Enums;

namespace TheSphinx.TheSphinxConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Interpreter interpreter = new Interpreter();

            while (true)
            {
                string cmd = Console.ReadLine();
                ResultStatus result = interpreter.Execute(cmd);

                if (result == ResultStatus.Exit)
                    break;
            }
        }
    }
}
