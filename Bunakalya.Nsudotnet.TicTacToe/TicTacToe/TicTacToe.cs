using System;
using System.Linq;

namespace TicTacToe
{
    class TicTacToe
    {
        private static readonly int[,] Field = new int[10, 10];

        static void Main(string[] args)
        {
            int player = 1;
            int lastCell = 0;

            while (true)
            {
                PrintField();
                int inputField;
                int inputCell;

                while (true)
                {
                    Console.WriteLine("Enter the number of field (1-9) and the number of cell (1-9) without spaces:");
                    string input = Console.ReadLine();

                    if ((input.Length != 2)
                        || !int.TryParse(input.Substring(0, 1), out inputField)
                        || !int.TryParse(input.Substring(1, 1), out inputCell)
                        || (inputField == 0) || (inputCell == 0))
                    {
                        continue;
                    }
                    if (!((lastCell == 0) || (Field[0, lastCell] == 9) || (inputField == lastCell)))
                    {
                        Console.WriteLine("You must choose the field number {0} (the number of the last cell)", lastCell);
                        continue;
                    }
                    if (Field[inputField, inputCell] != 0)
                    {
                        Console.WriteLine("You must choose a free cell");
                        continue;
                    }
                    break;
                }

                Field[inputField, inputCell] = player;
                Field[0, inputField]++;

                if ((Field[inputField, 0] == 0) && CheckWinField(inputField))
                {
                    int win = CheckWinGame();
                    if (win != 0)
                    {
                        PrintField();
                        Console.WriteLine("The player {0} has won", (win == 1) ? 'X' : '0');
                        return;
                    }
                }

                player *= -1;
                lastCell = inputCell;
            }
        }

        private static void PrintField()
        {
            for (int m = 0; m <= 6; m = m + 3)
            {
                string s = string.Format("{0}{1}{2}", (Field[m + 1, 0] == -1) ? "0000000" : (Field[m + 1, 0] == 1) ? "XXXXXXX" : "       ",
                    (Field[m + 2, 0] == -1) ? "0000000" : (Field[m + 2, 0] == 1) ? "XXXXXXX" : "       ",
                    (Field[m + 3, 0] == -1) ? "0000000" : (Field[m + 3, 0] == 1) ? "XXXXXXX" : "       ");

                Console.WriteLine(s);
                for (int k = 0; k <= 6; k = k + 3)
                {
                    for (int i = 1 + m; i <= 3 + m; i++)
                    {
                        Console.Write((Field[i, 0] == -1) ? '0' : (Field[i, 0] == 1) ? 'X' : ' ');
                        for (int j = 1 + k; j <= 3 + k; j++)
                        {
                            Console.Write("{0}{1}", (Field[i, j] == -1) ? '0' : (Field[i, j] == 1) ? 'X' : ' ', (j == 3 + k) ? string.Empty : "|");
                        }
                        Console.Write((Field[i, 0] == -1) ? '0' : (Field[i, 0] == 1) ? 'X' : ' ');
                    }
                    Console.WriteLine();
                    Console.Write((k == 6) ? string.Empty : string.Format("{0}-|-|-{0}{1}-|-|-{1}{2}-|-|-{2} \n", s.ElementAt(0), s.ElementAt(7), s.ElementAt(14)));
                }
                Console.WriteLine(s);
            }
        }

        private static bool CheckWinField(int inputField)
        {
            for (int i = 0; i <= 2; i++)
            {
                int horizontal = 0;
                int vertical = 0;
                int diagonal = 0;

                for (int j = 1; j <= 3; j++)
                {
                    horizontal += Field[inputField, i * 3 + j];
                    vertical += Field[inputField, i + 1 + 3 * (j - 1)];
                    if (i == 1)
                    {
                        diagonal += Field[inputField, 1 + (j - 1) * 4];
                    }
                    else if (i == 2)
                    {
                        diagonal += Field[inputField, 3 + (j - 1) * 2];
                    }
                }
                if ((horizontal == 3) || (vertical == 3) || (diagonal == 3))
                {
                    Field[inputField, 0] = 1;
                    return true;
                }
                if ((horizontal == -3) || (vertical == -3) || (diagonal == -3))
                {
                    Field[inputField, 0] = -1;
                    return true;
                }
            }
            return false;
        }

        private static int CheckWinGame()
        {
            for (int i = 0; i <= 2; i++)
            {
                int horizontal = 0;
                int vertical = 0;
                int diagonal = 0;

                for (int j = 1; j <= 3; j++)
                {
                    horizontal += Field[i * 3 + j, 0];
                    vertical += Field[i + 1 + 3 * (j - 1), 0];
                    if (i == 1)
                    {
                        diagonal += Field[1 + (j - 1) * 4, 0];
                    }
                    else if (i == 2)
                    {
                        diagonal += Field[3 + (j - 1) * 2, 0];
                    }
                }
                if ((horizontal == 3) || (vertical == 3) || (diagonal == 3))
                {
                    return 1;
                }
                if ((horizontal == -3) || (vertical == -3) || (diagonal == -3))
                {
                    return -1;
                }
            }
            return 0;
        }

    }

}
