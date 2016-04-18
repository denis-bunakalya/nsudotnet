using System;

namespace TicTacToe
{
    class Model
    {
        public int[,] Field { get; private set; }

        private int _player;
        private int _lastCell;
        public int Winner { get; private set; }

        public Model()
        {
            Field = new int[10, 10];

            _player = 1;
            _lastCell = 0;
            Winner = 0;
        }

        public void MakeMove(int inputField, int inputCell)
        {
            if (!((_lastCell == 0) || (Field[0, _lastCell] == 9) || (inputField == _lastCell)))
            {
                throw new Exception(String.Format("You must choose the field number {0} (the number of the last cell)", _lastCell));
            }

            if (Field[inputField, inputCell] != 0)
            {
                throw new Exception("You must choose a free cell");
            }

            Field[inputField, inputCell] = _player;
            Field[0, inputField]++;

            Field[inputField, inputCell] = _player;
            Field[0, inputField]++;

            if ((Field[inputField, 0] == 0) && CheckWinField(inputField))
            {
                Winner = CheckWinGame();
            }

            _player *= -1;
            _lastCell = inputCell;
        }

        private bool CheckWinField(int inputField)
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

        private int CheckWinGame()
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
