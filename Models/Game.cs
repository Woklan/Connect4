﻿using System;
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
        }

        public void pvp()
        {
            String userInput = "";
            int userNum = 0;

            while(true)
            {
                board.printBoard();

                do
                {
                    Console.WriteLine("Player 1: Please choose a column starting from 0 to 6");
                    userInput = Console.ReadLine();

                    userNum = int.Parse(userInput);
                } while (userNum > 0 && userNum < 7);

                board.placePiece(1, userNum);

                if (board.winCondition) break;

                do
                {
                    Console.WriteLine("Player 2: Please choose a column starting from 0 to 6");
                    userInput = Console.ReadLine();

                    userNum = int.Parse(userInput);
                } while (userNum > 0 && userNum < 7);

                board.placePiece(2, userNum);

                if (board.winCondition) break;
            }

            if(board.winPlayer == 1)
            {
                Console.WriteLine("Congrats Player 1, You Won!");
            }
            else
            {
                Console.WriteLine("Congrats Player 2, You Won!");
            }
        }
    }
}
