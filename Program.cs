using Connect4Console.Models;
using System;

namespace Connect4Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Board board = new Board();

            board.placePiece(1, 1);
            board.placePiece(1, 2);
            board.placePiece(1, 3);
            board.placePiece(1, 4);

            board.printBoard();

            if (board.winCondition)
            {
                Console.WriteLine("OOF");
            }
        }
    }
}
