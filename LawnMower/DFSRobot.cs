using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnMower
{
    public class DFSRobot : Robot
    {
        private int[] Start(string[,] arr)
        {
            int[] startPoint = { 0, 0 };
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == "~ ")
                    {
                        startPoint[0] = i;
                        startPoint[1] = j;
                        break;
                    }
                }
                break;
            }
            return startPoint;
        }

        private bool[,] IsVisited(int row, int col, string[,] arr)
        {
            bool[,] isVisited = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (arr[i, j] == "0 ")
                    {
                        isVisited[i, j] = true;
                    }
                    else
                    {
                        isVisited[i, j] = false;
                    }
                }
            }
            return isVisited;
        }

        private bool CanMove(int row, int col, bool[,] isVisited)
        {
            if (row < 0 || col < 0 || row > isVisited.GetLength(0) - 1 || col > isVisited.GetLength(1) - 1)
            {
                return false;
            }
            if (isVisited[row, col])
            {
                return false;
            }
            return true;
        }
        public override void DecreaseBattery(double b)
        {
            try
            {
                BatteryLife = BatteryLife - b;
            }
            catch
            {
                Console.WriteLine("Recharge battery");
                BatteryLife = 100;
                Console.ReadKey();
            }
        }

        public override void Cut(string[,] arr)
        {
            bool[,] isVis = IsVisited(arr.GetLength(0), arr.GetLength(1), arr);
            int[] startPoint = Start(arr);

            int row = startPoint[0];
            int col = startPoint[1];

            Stack<int> stack = new Stack<int>();

            ShowGarden(arr);
            Console.ReadKey();

            int tempCol = row;
            int tempRow = col;

            do
            {
                if (!isVis[row, col])
                {
                    if (isVis[tempRow, tempCol])
                    {
                        int peekCol = stack.Pop();
                        int peekRow = stack.Peek();

                        if (peekCol == tempCol && peekRow == tempRow)
                        {
                            stack.Push(peekCol);
                        }
                        else
                        {
                            stack.Push(peekCol);
                            stack.Push(tempRow);
                            stack.Push(tempCol);
                        }
                    }
                    stack.Push(row);
                    stack.Push(col);
                }

                tempRow = row;
                tempCol = col;

                isVis[row, col] = true;

                arr[row, col] = "R ";
                Console.Clear();
                ShowGarden(arr);
                arr[row, col] = "- ";
                Thread.Sleep(50);


                if (CanMove(row - 1, col, isVis))
                {
                    row -= 1;
                    DecreaseBattery(1);
                }
                else if (CanMove(row, col + 1, isVis))
                {
                    col += 1;
                    DecreaseBattery(1);
                }
                else if (CanMove(row + 1, col, isVis))
                {
                    row += 1;
                    DecreaseBattery(1);
                }
                else if (CanMove(row, col - 1, isVis))
                {
                    col -= 1;
                    DecreaseBattery(1);
                }
                else
                {
                    col = stack.Pop();
                    row = stack.Pop();
                    DecreaseBattery(0.5);
                }

            } while (stack.Count > 0);


            Console.Clear();
            arr[row, col] = "R ";
            ShowGarden(arr);
            Console.WriteLine(BatteryLife + " %");
        }


    }
}
