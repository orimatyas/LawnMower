using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnMower
{
    public abstract class Robot
    {
        private double battery = 100;
        public double BatteryLife
        {
            get
            {
                return battery;
            }
            set
            {
                if (value >= 0)
                {
                    battery = value;
                }
                else
                {
                    throw new Exception("Recharge battery");
                }
            }
        }
        protected void ShowGarden(string[,] arr)
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
        public abstract void Cut(string[,] arr);
        public abstract void DecreaseBattery(double b);

    }
}
