using System;

namespace PreCoord.Core.Consoles
{
    public static class ExitConsole
    {
        public static void WhenCtrlC()
        {
            while (true)
            {
                Console.WriteLine("Hit Ctrl-C to end.");
                var cmd = Console.ReadKey(true);
                Console.WriteLine();
                if (cmd.Modifiers == ConsoleModifiers.Control && cmd.KeyChar == 'c')
                    break;
            }
        }
    }
}