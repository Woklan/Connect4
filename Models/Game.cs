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
            board = new Board(false);
        }

        public void gameSetup()
        {
            String userInput = "";
            Console.WriteLine("Please select an option from below:");
            Console.WriteLine("(1) User vs. User");
            Console.WriteLine("(2) Random Bot vs. User");
            Console.WriteLine("(3) MiniMax Bot vs. User");

            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    pvp();
                    break;

                case "2":
                    randomBot();
                    break;

                case "3":
                    miniMaxBot();
                    break;

                default:
                    gameSetup();
                    break;
            }
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
            } while (!board.placePiece(1, userNum));
        }

        public void miniMaxBot()
        {
            MiniMax bot;
            Board tempBoard;

            while (true)
            {
                board.printBoard();

                __userPlace(1);

                if (board.winCondition) break;

                board.printBoard();

                tempBoard = Board.GetInstance(board);

                bot = new MiniMax(tempBoard, 1);

                board.placePiece(2, bot.getBestMove()[1]);

                if (board.winCondition) break;
            }
        }

        public void randomBot()
        {
            Random random = new Random();

            while (true)
            {
                board.printBoard();

                __userPlace(1);

                if (board.winCondition) break;

                board.printBoard();

                while (!board.placePiece(2, random.Next(7) - 1));

                if (board.winCondition) break;
            }

            switch (board.winPlayer)
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

                if (board.winCondition) break;

                __userPlace(2);

                if (board.winCondition) break;
            }

            switch (board.winPlayer)
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
