using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnMower
{
    public class Garden
    {
        public static string[,] MakeGarden(int x, int y)
        {
            string[,] garden = new string[x, y];
            Random random = new Random();

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (random.Next(100) < 14)
                    {
                        garden[i, j] = "0 ";
                    }
                    else
                    {
                        garden[i, j] = "~ ";
                    }
                }
            }
            return garden;
        }



    }
}
