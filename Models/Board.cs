using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Console.Models
{
    class Board
    {
        int[,] board;
        public bool winCondition = false;
        public int winPlayer = 0;

        public Board()
        {
            // Creates the board
            board = new int[6, 7];

            // Initizalizes the Board
            for(int i = 0; i < 7; i++)
            {
                for(int x = 0; x < 6; x++)
                {
                    board[x,i] = 0;
                }
            }
        }

        public void printBoard()
        {
            Console.WriteLine("---------");

            for(int i = 5; i > -1; i--)
            {
                Console.Write("|");
                for(int x = 0; x < 7; x++)
                {
                    Console.Write(board[i, x]);
                }
                
                Console.WriteLine("|");
            }
            
            Console.WriteLine("---------");
        }

        public bool placePiece(int player, int col)
        {
            int row = 5;

            // Checks if move is valid
            if(board[5, col] != 0)
            {
                return false;
            }

            // Finds the lowest valid move
            while(row > 0 && board[row-1, col] == 0)
            {
                row--;
            }

            // updates board
            board[row, col] = player;

            winCondition = (checkWinDirectional(player, row, col, '0', '-') || checkWinDirectional(player, row, col, '-', '0') || checkWinDirectional(player, row, col, '-', '-') || checkWinDirectional(player, row, col, '+', '-'));

            if (winCondition) winPlayer = player;

            return true;
        }

        public bool checkWinDirectional(int player, int x, int y, char xDynamic, char yDynamic)
        {
            int count = 1;
            int tempX = x;
            int tempY = y;

            if(xDynamic == '-') tempX--;
            
            if(xDynamic == '+') tempX++;
          
            if(yDynamic == '-') tempY--;
    
            if(yDynamic == '+') tempY++;

            while(tempY >= 0 && tempY < 6 && tempX >= 0 && tempX < 7 && board[tempX, tempY] == player)
            {
                if (xDynamic == '-') tempX--;

                if (xDynamic == '+') tempX++;

                if (yDynamic == '-') tempY--;

                if (yDynamic == '+') tempY++;

                count++;
            }

            tempX = x;
            tempY = y;

            if (xDynamic == '-') tempX++;

            if (xDynamic == '+') tempX--;

            if (yDynamic == '-') tempY++;

            if (yDynamic == '+') tempY--;

            while (tempY >= 0 && tempY < 6 && tempX >= 0 && tempX < 7 && board[tempX, tempY] == player)
            {
                if (xDynamic == '-') tempX++;

                if (xDynamic == '+') tempX--;

                if (yDynamic == '-') tempY++;

                if (yDynamic == '+') tempY--;

                count++;
            }

            return count >= 4 ? true : false;
        }
    }
}
