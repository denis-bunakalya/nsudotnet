using System;
using System.Linq;

namespace TicTacToe
{
    class View
    {
        private readonly Model _model;

        public View(Model model)
        {
            _model = model;
        }

        public void Show()
        {
            while (true)
            {
                PrintField(_model.Field);

                while (true)
                {
                    Console.WriteLine("Enter the number of field (1-9) and the number of cell (1-9) without spaces:");
                    string input = Console.ReadLine();

                    int inputField;
                    int inputCell;

                    if ((input.Length != 2)
                        || !int.TryParse(input.Substring(0, 1), out inputField)
                        || !int.TryParse(input.Substring(1, 1), out inputCell)
                        || (inputField == 0) || (inputCell == 0))
                    {
                        continue;
                    }
                    try
                    {
                        _model.MakeMove(inputField, inputCell);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                    break;
                }
                if (_model.Winner != 0)
                {
                    PrintField(_model.Field);
                    Console.WriteLine("The player {0} has won", (_model.Winner == 1) ? 'X' : '0');
                    return;
                }
            }
        }

        private static void PrintField(int[,] field)
        {
            for (int m = 0; m <= 6; m = m + 3)
            {
                string s = string.Format("{0}{1}{2}", 
                    (field[m + 1, 0] == -1) ? "0000000" : (field[m + 1, 0] == 1) ? "XXXXXXX" : "       ",
                    (field[m + 2, 0] == -1) ? "0000000" : (field[m + 2, 0] == 1) ? "XXXXXXX" : "       ",
                    (field[m + 3, 0] == -1) ? "0000000" : (field[m + 3, 0] == 1) ? "XXXXXXX" : "       ");

                Console.WriteLine(s);
                for (int k = 0; k <= 6; k = k + 3)
                {
                    for (int i = 1 + m; i <= 3 + m; i++)
                    {
                        Console.Write((field[i, 0] == -1) ? '0' : (field[i, 0] == 1) ? 'X' : ' ');
                        for (int j = 1 + k; j <= 3 + k; j++)
                        {
                            Console.Write("{0}{1}", (field[i, j] == -1) ? '0' : (field[i, j] == 1) ? 'X' : ' ', 
                                (j == 3 + k) ? string.Empty : "|");
                        }
                        Console.Write((field[i, 0] == -1) ? '0' : (field[i, 0] == 1) ? 'X' : ' ');
                    }
                    Console.WriteLine();
                    Console.Write((k == 6) ? string.Empty : string.Format("{0}-|-|-{0}{1}-|-|-{1}{2}-|-|-{2} \n", 
                        s.ElementAt(0), s.ElementAt(7), s.ElementAt(14)));
                }
                Console.WriteLine(s);
            }
        }
    }
}
