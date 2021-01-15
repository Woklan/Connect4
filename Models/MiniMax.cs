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
            int tempValue = 0;
            int[] xy;

            for(int i = 0; i < 7; i++)
            {
                if(possibleMoves[i] != null)
                {
                    possibleMoves[i].updateValue();
                    tempValue += possibleMoves[i].getValue();
                }
                else
                {
                    xy = currentBoard.xy;
                    value = currentBoard.checkNumDirectional(player == 1 ? 2 : 1, xy[0], xy[1], '0', '-');
                    value += currentBoard.checkNumDirectional(player == 1 ? 2 : 1, xy[0], xy[1], '-', '0');
                    value += currentBoard.checkNumDirectional(player == 1 ? 2 : 1, xy[0], xy[1], '-', '-');
                    value += currentBoard.checkNumDirectional(player == 1 ? 2 : 1, xy[0], xy[1], '+', '-');

                    value += currentBoard.checkNumDirectional(player, xy[0], xy[1], '0', '-');
                    value += currentBoard.checkNumDirectional(player, xy[0], xy[1], '-', '0');
                    value += currentBoard.checkNumDirectional(player, xy[0], xy[1], '-', '-');
                    value += currentBoard.checkNumDirectional(player, xy[0], xy[1], '+', '-');
                }
            }

            value = tempValue;
        }
    }
}
