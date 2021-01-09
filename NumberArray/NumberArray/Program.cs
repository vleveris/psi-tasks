using System;

namespace NumberArray
{
    class Program
    {
        public event EventHandler GreaterThan15;

        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Invalid number of arguments");
                Environment.Exit(1);
            }
            int size = int.Parse(args[0]);
            int div = int.Parse(args[1]);
            int lim = int.Parse(args[2]);
            Calculations calc = new Calculations(size, div, lim);
            calc.GreaterThan15 += Greater;
            calc.Start();
        }

        private static void Greater(object sender, GreaterThan15EventArgs info)
        {
            Console.WriteLine("Event fired");
            Console.WriteLine("There are " + info.X + " integers greater than " + info.Y);
        }

    }
}
