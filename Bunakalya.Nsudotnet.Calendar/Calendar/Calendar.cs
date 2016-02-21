using System;

namespace Calendar
{
    class Calendar
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter date:");
            DateTime inputDate;

            if (!DateTime.TryParse(Console.ReadLine(), out inputDate))
            {
                Console.WriteLine("Wrong date format");
                return;
            }

            Console.WriteLine("Mo Tu We Th Fr Sa Su");
            DateTime firstDay = inputDate.AddDays(-inputDate.Day + 1);

            int position = ((int)firstDay.DayOfWeek + 6) % 7;
            Console.Write(new String(' ', position * 3));

            ConsoleColor initialForeground = Console.ForegroundColor;
            ConsoleColor initialBackground = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            int numberOfWorkdays = 0;

            for (DateTime i = firstDay; i.Month == inputDate.Month; i = i.AddDays(1))
            {
                if ((position == 5) || (position == 6))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    numberOfWorkdays++;
                    if ((position == 0) && (i.Day != 1))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                }

                if (i.Day == inputDate.Day)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                if (i.Date == DateTime.Now.Date)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }

                Console.Write("{0,2}", i.Day);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");

                position = (position + 1) % 7;
            }
            Console.WriteLine();

            Console.ForegroundColor = initialForeground;
            Console.BackgroundColor = initialBackground;
            Console.WriteLine("Number of workdays: {0}", numberOfWorkdays);
        }

    }

}
