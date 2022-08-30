using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutConsole
{
    internal class LightsOutGame
    {
        bool[,] grid = new bool[3, 3];

        public int GridSize
        {
            get { return grid.GetLength(0); }
            set
            {
                if (value >= 3 && value <= 7)
                {
                    grid = new bool[value, value];
                    NewGame();
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(GridSize), $"GridSize value must be between 3 and 7.");
                }
            }
        }

        public LightsOutGame()
        {
            GridSize = -1;
        }

        public void NewGame()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Random rand = new Random();
                    int randLight = rand.Next(0, 2);
                    if (randLight == 0)
                    {
                        grid[i, j] = false;
                    }
                    else if (randLight == 1)
                    {
                        grid[i, j] = true;
                    }
                }
            }
        }

        public bool IsOn(int row, int col)
        {
            return grid[row, col];
        }

        public void FlipLight(int row, int col)
        {
            //Fliping light at row, col
            if (grid[row, col] == false)
            {
                grid[row, col] = true;
            }
            else{
                grid[row, col] = false;
            }

            //Orthogonal positions: top, bottom, left, right (with respect to row, col)
            int[,] OrthoPositions = new int[,] {{ row, col - 1 },
                                                { row, col + 1 },
                                                { row - 1, col },
                                                { row + 1, col }};


            for (int i = 0; i < OrthoPositions.GetLength(0); i++)
            {
                int rowPos = OrthoPositions[i, 0];
                int colPos = OrthoPositions[i, 1];

                //Checking if Orthogonal position is in bounds
                if ((rowPos >= 0 && rowPos <= GridSize-1) && (colPos >= 0 && colPos <= GridSize-1))
                {
                    if (grid[rowPos, colPos] == false)
                    {
                        grid[rowPos, colPos] = true;
                    }
                    else
                    {
                        grid[rowPos, colPos] = false;
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == true)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void Cheat()
        {
            Console.WriteLine("Cheat Function");
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = false;
                }
            }

            grid[0, 0] = true;
            grid[0, 1] = true;
            grid[1, 0] = true;
        }

        public override string ToString()
        {
            string gridStr = "";
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == true)
                    {
                        gridStr += "T";
                    }
                    else
                    {
                        gridStr += "F";
                    }

                    if (j == GridSize - 1)
                    {
                        gridStr = gridStr + Environment.NewLine;
                    }
                }
            }
            return gridStr;
        }

        //Function to test NewGame as example
        public void NewGameTest()
        {
            grid[0, 2] = true;
            grid[1, 0] = true;
            grid[2, 0] = true;
            grid[2, 2] = true;
        }
    }
}
