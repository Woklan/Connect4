using System;
using System.Collections.Generic;
using System.Text;

// There are at most 6 possible moves

namespace Connect4Console.Models
{
    class MiniMax
    {
        Board currentBoard, tempBoard;
        MiniMax[] possibleMoves;
        int value;
        int counter;
        int depth;
        int depthLimit;
        int player;

        public MiniMax(Board board, int depthLimit, int depth = 0, int player = 1)
        {
            this.currentBoard = board;
            counter = 0;
            this.depth = depth;
            this.depthLimit = depthLimit;
            this.player = player;

            possibleMoves = new MiniMax[7];

            growTree();
            updateValue();
        }

        public int[] getBestMove()
        {
            int highestValue = 0;
            int iterator = 0;
            
            updateValue();

            for(int i = 0; i < 7; i++)
            {
                Console.WriteLine("Row: " + possibleMoves[i].currentBoard.xy[0] + "|Col: " + possibleMoves[i].currentBoard.xy[1] + "|Value: " + possibleMoves[i].getValue());
                if(possibleMoves[i].getValue() > highestValue)
                {
                    highestValue = possibleMoves[i].getValue();
                    iterator = i;
                }
            }
            return possibleMoves[iterator].currentBoard.xy;
        }

        public int getValue()
        {
            return value;
        }

        public void growTree()
        {
            if (depth == depthLimit) return;

            for (int i = 0; i < 7; i++)
            {
                tempBoard = Board.GetInstance(currentBoard);

                if(tempBoard.placePiece(2, i))
                {
                    possibleMoves[counter] = new MiniMax(tempBoard, depthLimit, depth + 1, player == 1 ? 2 : 1);
                    counter++;
                }
            }

            while (counter < 7)
            {
                possibleMoves[counter] = null;
                counter++;
            }
        }

        public void updateValue()
        {
            value = 0;
            int[] xy;

            if (possibleMoves[0] == null)
            {
                xy = currentBoard.xy;

                value = upDown(player, xy[0], xy[1]);
                value += leftRight(player, xy[0], xy[1]);
                value += leftDiagonal(player, xy[0], xy[1]);
                value += rightDiagonal(player, xy[0], xy[1]);

                value += upDown(player == 1 ? 2 : 1, xy[0], xy[1]);
                value += leftRight(player == 1 ? 2 : 1, xy[0], xy[1]);
                value += leftDiagonal(player == 1 ? 2 : 1, xy[0], xy[1]);
                value += rightDiagonal(player == 1 ? 2 : 1, xy[0], xy[1]);

                value = value - 7;

                if(player == 1)
                {
                    value = value * -1;
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    if(possibleMoves[i] != null)
                    {
                        possibleMoves[i].updateValue();

                        if (possibleMoves[i].getValue() > value)
                        {
                            value = possibleMoves[i].getValue();
                        }
                    }
                }
            }
        }

        public int upDown(int player, int row, int col)
        {
            int tempRow = row + 1;
            int tempCol = col;
            int count = 1;
            int multiplier = 1;

            while (tempRow < 6 && currentBoard.getXY(tempRow, tempCol) == player)
            {
                count += 1 * multiplier;
                tempRow++;
                multiplier++;
            }

            tempRow = row - 1;

            while (tempRow > -1 && currentBoard.getXY(tempRow, tempCol) == player)
            {
                count += 1 * multiplier;
                tempRow--;
                multiplier++;
            }


            return count;
        }

        public int leftRight(int player, int row, int col)
        {
            int tempRow = row;
            int tempCol = col + 1;
            int count = 1;
            int multiplier = 1;

            while (tempCol < 7 && currentBoard.getXY(tempRow, tempCol) == player)
            {
                count += 1 * multiplier;
                tempCol++;
                multiplier++;
            }

            tempCol = col - 1;

            while (tempCol > -1 && currentBoard.getXY(tempRow, tempCol) == player)
            {
                count += 1 * multiplier;
                tempCol--;
                multiplier++;
            }

            return count;
        }

        public int rightDiagonal(int player, int row, int col)
        {
            int tempRow = row + 1;
            int tempCol = col + 1;
            int count = 1;
            int multiplier = 1;

            while (tempCol < 7 && tempRow < 6 && currentBoard.getXY(tempRow, tempCol) == player)
            {
                count += 1 * multiplier;
                tempRow++;
                tempCol++;
                multiplier++;
            }

            tempRow = row - 1;
            tempCol = col - 1;

            while (tempCol > -1 && tempRow > -1 && currentBoard.getXY(tempRow, tempCol) == player)
            {
                count += 1 * multiplier;
                tempRow--;
                tempCol--;
                multiplier++;
            }

            return count;
        }

        public int leftDiagonal(int player, int row, int col)
        {
            int tempRow = row - 1;
            int tempCol = col + 1;
            int count = 1;
            int multiplier = 1;

            while (tempCol < 7 && tempRow > -1 && currentBoard.getXY(tempRow, tempCol) == player)
            {
                count += 1 * multiplier;
                tempRow--;
                tempCol++;
                multiplier++;
            }

            tempRow = row + 1;
            tempCol = col - 1;

            while (tempCol > -1 && tempRow < 6 && currentBoard.getXY(tempRow, tempCol) == player)
            {
                count += 1 * multiplier;
                tempRow++;
                tempCol--;
                multiplier++;
            }

            return count;
        }
    }
}
