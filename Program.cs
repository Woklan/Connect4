using Connect4Console.Models;
using System;

namespace Connect4Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.gameSetup();
        }
    }
}
