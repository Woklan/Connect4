using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Console.Models
{
    class Game
    {
        Board board;

        public Game()
        {
            board = new Board();
        }

        public void gameSetup()
        {
            String userInput = "";
            Console.WriteLine("Please select an option from below:");
            Console.WriteLine("(1) User vs. User");

            userInput = Console.ReadLine();

            if (userInput == "1") pvp();
            if (userInput == "2") randomBot();
        }

        private void __userPlace(int playerNum)
        {
            String userInput = "";
            int userNum = 0;

            do
            {
                Console.WriteLine("Player " + playerNum + ": Please choose a column starting from 0 to 6");
                userInput = Console.ReadLine();

                userNum = int.Parse(userInput);
            } while (userNum < 0 && userNum > 6 || !board.placePiece(1, userNum));
        }

        public void randomBot()
        {
            Random random = new Random();

            while (true)
            {
                board.printBoard();

                __userPlace(1);

                if (board.getWinCondition()) break;

                board.printBoard();

                while (!board.placePiece(2, random.Next(7) - 1));

                if (board.getWinCondition()) break;
            }

            switch (board.getWinPlayer())
            {
                case -1:
                    Console.WriteLine("Unfortunately, it is a tie");
                    break;
                case 1:
                    Console.WriteLine("Congrats Player 1, You Won!");
                    break;
                case 2:
                    Console.WriteLine("Congrats Player 2, You Won!");
                    break;
            }
        }

        public void pvp()
        {

            while(true)
            {
                board.printBoard();

                __userPlace(1);

                board.printBoard();

                if (board.getWinCondition()) break;

                __userPlace(2);

                if (board.getWinCondition()) break;
            }

            switch (board.getWinPlayer())
            {
                case -1:
                    Console.WriteLine("Unfortunately, it is a tie");
                    break;
                case 1:
                    Console.WriteLine("Congrats Player 1, You Won!");
                    break;
                case 2:
                    Console.WriteLine("Congrats Player 2, You Won!");
                    break;
            }
        }
    }
}
