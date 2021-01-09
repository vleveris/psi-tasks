using System;
using System.IO;
using System.Collections.Generic;

namespace FileTask
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("1 argument expected - file name");
                Environment.Exit(1);
            }
            List<Lazy<Person>> people = new List<Lazy<Person>>();
            Console.WriteLine("R - show file contents");
            Console.WriteLine("F - Filter by name");
            string answer = Console.ReadLine().ToUpper();
            string letter = string.Empty;
            bool show = false;
            switch (answer)
            {
                case "R":
                    show = true;
                    break;
                case "F":
                    bool ok = false;
                    do
                    {
                        letter = Console.ReadLine();
                        if (letter.Length != 1)
                        {
                            Console.WriteLine("Only 1 character allowed");
                            continue;
                        }
                        else
                        {
                            // check by ASCII

                            int code = (int)letter[0];
                            if (!((code > 64 && code < 91) || (code > 96 && code < 123)))
                            {
                                Console.WriteLine("Only letter can be typed");
                                continue;
                            }
                            else
                                ok = true;
                        }
                    }
                    while (!ok);
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
            try
            {
                using (var input = new StreamReader(args[0]))
                {
                    string line;
                    string[] values = new string[2];
                    if (show)
                    {
                        Console.WriteLine("File contents:");
                    }
                    while ((line = input.ReadLine()) != null)
                    {
                        values = line.Split(",", 2, StringSplitOptions.RemoveEmptyEntries);
                        people.Add(new Lazy<Person>(new Person(values[0], int.Parse(values[1]))));
                        if (show)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    if (letter != string.Empty)
                    {
                        Console.WriteLine("Filtered people list");
                        Console.WriteLine("Name (age)");
                        List<Lazy<Person>> filtered = FilterByName(people, letter[0]);
                        foreach (Lazy<Person> p in filtered)
                        {
                            Person pr = p.Value;
                            Console.WriteLine("{0} ({1})", pr.Name, pr.Age);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static List<Lazy<Person>> FilterByName(List<Lazy<Person>> p, char letter)
        {
            List<Lazy<Person>> persons = p.FindAll(person => (person.Value.Name.StartsWith(letter)));
            return persons;
        }

    }
}