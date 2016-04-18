namespace TicTacToe
{
    class TicTacToe
    {
        static void Main(string[] args)
        {
            Model model = new Model();
            View view = new View(model);
            view.Show();
        }
    }
}
