using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_ANG
{
    class Board
    {
        private static Board _instance = null;
        private int[,] grid;
        private static Object _mutex = new Object();

        public int[,] Grid { get { return grid; } set { grid = value; } }

        private Board(int nbC, int nbJ)
        {
            grid = new int[nbC, nbJ];
            for (int i = 0; i < nbJ; i++)
            {
                grid[0, i] = 1;
            }
        }

        public static Board GetInstance(int nbC, int nbJ)
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new Board(nbC, nbJ);
                    }
                }
            }
            return _instance;
        }

        static public void printBoard(int[,] tab)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                for (int i = 0; i < tab.GetLength(0); i++)
                {
                    Console.Write(tab[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
