using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Evosoft_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[,] garden = makeGarden(8, 8);
            DFS(garden);
            Console.ReadLine();
        }

        public static bool inBound(int row, int col, bool[,] isVisited)
        {
            if (row < 0 || col < 0 || row > isVisited.GetLength(0) - 1 || col > isVisited.GetLength(1) - 1) { return false; }
            if (isVisited[row, col]) { return false; }
            return true;
        }

        public static bool[,] isVisited(int row, int col, string[,] arr)
        {
            bool[,] isVisited = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (arr[i, j] == "@")
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

        public static void DFS(string[,] arr)
        {
            bool[,] isVis = isVisited(arr.GetLength(0), arr.GetLength(1), arr);
            int[] startPoint = Start(arr);
            int row = startPoint[0];
            int col = startPoint[1];

            Stack<int> stack = new Stack<int>();
            PrintArray(arr);
            Console.ReadLine();

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

                arr[row, col] = "x";
                Console.Clear();
                PrintArray(arr);
                arr[row, col] = "~";
                //Thread.Sleep(50);
                Console.ReadLine();


                if (inBound(row - 1, col, isVis))
                {
                    row -= 1;
                }
                else if (inBound(row, col + 1, isVis))
                {
                    col += 1;
                }
                else if (inBound(row + 1, col, isVis))
                {
                    row += 1;
                }
                else if (inBound(row, col - 1, isVis))
                {
                    col -= 1;
                }
                else
                {
                    col = stack.Pop();
                    row = stack.Pop();
                }

            } while (stack.Count > 0);
            PrintArray(isVis);
            Console.ReadLine();
            Console.Clear();
            arr[row, col] = "x";
            PrintArray(arr);
        }

        public static string[,] makeGarden(int x, int y)
        {
            string[,] garden = new string[x, y];
            Random random = new Random();

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (random.Next(10) < 1)
                    {
                        garden[i, j] = "@";
                    }
                    else
                    {
                        garden[i, j] = "*";
                    }
                }
            }
            return garden;
        }


        public static void PrintArray(string[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j]);
                }
                Console.Write("\n");
            }
        }
        public static void PrintArray(bool[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j]);
                }
                Console.Write("\n");
            }
        }

        public static int[] Start(string[,] arr)
        {
            int[] startPoint = { 0, 0 };
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == "*")
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
    }
}
