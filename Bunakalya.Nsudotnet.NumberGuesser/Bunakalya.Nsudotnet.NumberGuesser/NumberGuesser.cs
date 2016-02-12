using System;
namespace Bunakalya.Nsudotnet.NumberGuesser
{
    public class NumberGuesser
    {
        private static readonly string[] Insults
    =
        {
            "loser",
            "fool",
            "stupid",
            "idiot"
        };

        private static readonly int[] Attempts = new int[1000];

        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            Random random = new Random();
            int number = random.Next(101);

            Console.WriteLine("Guess my number:");

            DateTime dateTime = DateTime.Now;

            for (int i = 0; ; i++)
            {
                int attempt;

                while (true)
                {
                    string input = Console.ReadLine();

                    if (input.Equals("q"))
                    {
                        Console.WriteLine("I'm sorry");
                        return;
                    }

                    try
                    {
                        attempt = int.Parse(input);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Enter a number");
                    }
                }

                Attempts[i] = attempt;

                if ((attempt != number) && (i % 4 == 3))
                {
                    Console.WriteLine(name + ", you are " + Insults[random.Next(4)]);
                }

                if (attempt > number)
                {
                    Console.WriteLine("Your number is bigger");
                }
                else if (attempt < number)
                {
                    Console.WriteLine("Your number is smaller");
                }
                else
                {
                    TimeSpan timeSpan = DateTime.Now - dateTime;
                    Console.WriteLine("That's right");
                    Console.WriteLine("Number of attempts: " + i);

                    for (int j = 0; j < i; j++)
                    {
                        Console.WriteLine(Attempts[j] + " " + ((Attempts[j] < number) ? "smaller" : "bigger"));
                    }
                    Console.WriteLine("Time: " + timeSpan.Minutes + "m " + timeSpan.Seconds + "s");
                    break;
                }
            }
        }

    }

}