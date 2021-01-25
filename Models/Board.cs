using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Console.Models
{
    class Board
    {
        int[,] board;
        public bool winCondition { get; private set; }
        public int winPlayer { get; private set; }
        public int[] xy { get; private set; }
        public bool virtualBoard { get; set; }

        public Board(bool virtualFlag = true)
        {
            // Creates the board
            board = new int[6, 7];

            xy = new int[2];

            // Initizalizes the Board
            for(int i = 0; i < 7; i++)
            {
                for(int x = 0; x < 6; x++)
                {
                    board[x,i] = 0;
                }
            }

            winPlayer = 0;
            winCondition = false;

            virtualBoard = virtualFlag;
        }

        // Copy Constructor
        public Board(Board board)
        {
            this.board = new int[6,7];
            for(int i = 0; i < 6; i++)
            {
                for(int x = 0; x < 7; x++)
                {
                    this.board[i, x] = board.board[i, x];
                }
            }

            xy = new int[2];

            winPlayer = 0;
            winCondition = false;
            virtualBoard = true;
        }

        // Copy Factory
        public static Board GetInstance(Board board)
        {
            return new Board(board);
        }

        public int getXY(int x, int y)
        {
            return board[x, y];
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
            if(col < 0 || col > 6 || board[5, col] != 0)
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
            xy[0] = row;
            xy[1] = col;

            // Handles if bot is making virtual moves
            if (!virtualBoard)
            {
                Console.WriteLine("Player " + player + " played Row: " + row + " | Col: " + col);
            }
            
            winCondition = (checkWinDirectional(player, row, col, '0', '-') || checkWinDirectional(player, row, col, '-', '0') || checkWinDirectional(player, row, col, '-', '-') || checkWinDirectional(player, row, col, '+', '-'));

            if (winCondition) winPlayer = player;

            return true;
        }

        public bool checkFullBoard()
        {
            for(int i = 0; i < 7; i++)
            {
                if (board[6,i] == 0) return false;
            }

            winPlayer = -1;
            winCondition = true;

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

            while(tempX >= 0 && tempX < 6 && tempY >= 0 && tempY < 7 && board[tempX, tempY] == player)
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

            while (tempX >= 0 && tempX < 6 && tempY >= 0 && tempY < 7 && board[tempX, tempY] == player)
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
