using System;

namespace ChessConsole
{
    public class Program
    {
        static ChessGame game;
        static void Main(string[] args)
        {
            Console.WriteLine("Randomize start peices (y/n)");
            string input = Console.ReadLine();
            if(input == "y")
            {
                game = new ChessGame(true);
            } 
            else
            {
                game = new ChessGame(false);
            }

            Console.CursorVisible = false;
            ConsoleGraphics graphics = new ConsoleGraphics();

            do
            {
                game.Draw(graphics);
                graphics.SwapBuffers();
                game.Update();
            } while (game.Running);

            Console.Read();
        }
    }
}
