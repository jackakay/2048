using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;

namespace _2048
{
    
    class coord
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    class Startup
    {
        [DllImport("User32.dll")]
        public static extern bool GetAsyncKeyState(int vKey);

        public static int[,] grid = { {0,0,0,0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 2 }, { 0, 0, 0, 2 } };

        static void Main(string[] args)
        {
            
            //37 left 38 up 39 right 40 down
            while (true)
            {

                #region draw
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for(int j = 0; j < grid.GetLength(1); j++)
                    {
                        int spot = grid[i,j];
                        
                        if(spot == 0)
                        {
                            Console.Write("   ");
                        }
                        else
                        {
                            Console.Write(spot);
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                
                Thread.Sleep(1000);
                Console.Clear();
                #endregion

                if (GetAsyncKeyState(38))
                {
                    MoveAllSpots(0);
                    //spawnSpot();
                    Thread.Sleep(100);
                }

            }
            
        }
        static List<coord> getAvailableSpots()
        {
            List<coord> spots = new List<coord>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 0) {
                        coord spot = new coord();
                        spot.x = i;
                        spot.y = j;
                        spots.Add(spot);
                    }
                }
              
            }
            return spots;
        }
        static void spawnSpot()
        {
            Random rand = new Random();
            List<coord> possibleSpots = getAvailableSpots();
            int newSpot = rand.Next(0, possibleSpots.Count);
            grid[possibleSpots[newSpot].x, possibleSpots[newSpot].y] = 2;
        }
        static void MoveAllSpots(int direction)
        {
            //direction -> 0 - up, 1- right, 2-down, 3-left

            switch(direction)
            {
                case 0:
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            if (grid[i, j] == 0) break;
                            bool placed = false;
                            while (!placed)
                            {
                                if (--i >= 0) {
                                    if (grid[--i, j] == 0)
                                    {
                                        grid[--i, j] = grid[i, j];
                                        grid[i, j] = 0;
                                        placed = true;
                                    } else if (grid[--i, j] == grid[i, j])
                                    {
                                        grid[--i, j] *= 2;
                                        grid[i, j] = 0;
                                        placed = true;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            /*
                            bool placed = false;
                            int count = 0;
                            while (!placed)
                            {

                                if (i == 0) break;
                                if (grid[i,j] == 0) break;
                                if (grid[0,j] == 0)
                                {
                                    grid[0,j] = grid[i, j];
                                    grid[i,j] = 0;
                                    placed = true;
                                    break;
                                }
                                else
                                {
                                    if (grid[i - count, j] == 0)
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        grid[i - (count + 1), j] = grid[i, j];
                                        grid[i, j] = 0;
                                        placed = true;
                                    }
                                }
                            }
                            */

                        }

                    }
                    break;
            }
        }
    }
}
