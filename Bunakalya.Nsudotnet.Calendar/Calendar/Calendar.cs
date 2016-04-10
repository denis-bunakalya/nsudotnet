using System;

namespace Calendar
{
    class Calendar
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter date (dd.mm.yyyy):");
            DateTime inputDate;

            if (!DateTime.TryParse(Console.ReadLine(), out inputDate))
            {
                Console.WriteLine("Wrong date format");
                return;
            }

            DateTime dayOfWeek = new DateTime(2016, 04, 11);
            for (int i = 0; i < 7; i++)
            {
                Console.Write("{0:ddd} ", dayOfWeek);
                dayOfWeek = dayOfWeek.AddDays(1);
            }
            Console.WriteLine();

            DateTime firstDay = inputDate.AddDays(-inputDate.Day + 1);
            Console.Write(new String(' ', (((int)firstDay.DayOfWeek + 6) % 7) * 3));

            ConsoleColor initialForeground = Console.ForegroundColor;
            ConsoleColor initialBackground = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            int numberOfWorkdays = 0;

            for (DateTime i = firstDay; i.Month == inputDate.Month; i = i.AddDays(1))
            {
                if ((i.DayOfWeek == DayOfWeek.Saturday) || (i.DayOfWeek == DayOfWeek.Sunday))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    numberOfWorkdays++;
                    if ((i.DayOfWeek == DayOfWeek.Monday) && (i.Day != 1))
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
            }
            Console.WriteLine();

            Console.ForegroundColor = initialForeground;
            Console.BackgroundColor = initialBackground;
            Console.WriteLine("Number of workdays: {0}", numberOfWorkdays);
        }

    }

}
