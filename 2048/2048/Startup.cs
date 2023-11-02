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

        public static int[,] grid = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 2 } };
        public static int score = 0;

        static void Main(string[] args)
        {

            //37 left 38 up 39 right 40 down
            while (true)
            {

                #region draw
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        int spot = grid[i, j];

                        if (spot == 0)
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write(spot);
                        }
                        Console.Write('|');
                    }
                    Console.WriteLine();
                    
                }
                Console.WriteLine("Score: " + score.ToString());
                Console.WriteLine();

                Thread.Sleep(1000);
                Console.Clear();
                #endregion

                if (GetAsyncKeyState(38))
                {
                    MoveAllSpots(0);
                    spawnSpot();
                    Thread.Sleep(100);
                }else if (GetAsyncKeyState(40))
                {
                    MoveAllSpots(1);
                    spawnSpot();
                    Thread.Sleep(100);
                }else if (GetAsyncKeyState(39))
                {
                    MoveAllSpots(2);
                    spawnSpot();
                    Thread.Sleep(100);
                }else if (GetAsyncKeyState(37))
                {
                    MoveAllSpots(3);
                    spawnSpot();
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
            //direction -> 0 - up, 1- down, 2-right, 3-left

            switch (direction)
            {
                case 0:
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            //i want to kms
                            if (grid[i, j] != 0)
                            {
                                for(int k = 1; k <= i; k++)
                                {
                                    if(grid[i - k, j] == grid[i, j])
                                    {
                                        grid[i - k, j] = grid[i, j] * 2;
                                        score += grid[i, j] * 2;
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    if (grid[i - k, j] != 0)
                                    {
                                        grid[i - k + 1, j] = grid[i, j];
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    if (i - k == 0)
                                    {
                                        grid[0, j] = grid[i, j];
                                        grid[i, j] = 0;
                                        break;
                                    }
                                }
                            } 
                        }
                    }
                    break;
                case 1:
                    for (int i = grid.GetLength(0)-1; i >=0;  i--)
                    {
                        for (int j = grid.GetLength(1)-1;  j >=0; j--)
                        {
                            //i want to kms
                            if (grid[i, j] != 0)
                            {
                                for (int k = 1; k <= 3-i; k++)
                                {
                                    if (grid[i + k, j] == grid[i, j])
                                    {
                                        grid[i + k, j] = grid[i, j] * 2;
                                        score += grid[i, j] * 2;
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    if (grid[i + k, j] != 0)
                                    {
                                        grid[i+k-1,j] = grid[i, j];
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    if (i + k == 3)
                                    {
                                        grid[3, j] = grid[i, j];
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    
                                }
                            }

                        }
                    }
                    break;
                case 2:
                    for (int i = grid.GetLength(0) - 1; i >= 0; i--)
                    {
                        for (int j = grid.GetLength(1) - 1; j >= 0; j--)
                        {
                            //i want to kms
                            if (grid[i, j] != 0)
                            {
                                for (int k = 1; k <= 3 - j; k++)
                                {
                                    if (grid[i, j+k] == grid[i, j])
                                    {
                                        grid[i, j + k] = grid[i, j] * 2;
                                        score += grid[i, j] * 2;
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    if (grid[i , j + k] != 0)
                                    {
                                        grid[i , j + k - 1] = grid[i, j];
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    if (j + k == 3)
                                    {
                                        grid[i, 3] = grid[i, j];
                                        grid[i, j] = 0;
                                        break;
                                    }

                                }
                            }

                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            //i want to kms
                            if (grid[i, j] != 0)
                            {
                                for (int k = 1; k <= j; k++)
                                {
                                    if (grid[i, j-k] == grid[i, j])
                                    {
                                        grid[i , j -k] = grid[i, j] * 2;
                                        score += grid[i, j] * 2;
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    if (grid[i, j-k] != 0)
                                    {
                                        grid[i , j - k+1] = grid[i, j];
                                        grid[i, j] = 0;
                                        break;
                                    }
                                    if (j - k == 0)
                                    {
                                        grid[i, 0] = grid[i, j];
                                        grid[i, j] = 0;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        } 
                    
    }
 }
    

