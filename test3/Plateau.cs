using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Auriac_Barreau
{
    class Plateau
    {
        private static Plateau _instance = null;
        private int[,] grille;
        private static Object _mutex = new Object();

        public int[,] Grille { get { return grille; } set { grille = value; } }

        private Plateau(int nbC, int nbJ)
        {
            grille = new int[nbC, nbJ];
            for (int i = 0; i < nbJ; i++)
            {
                grille[0, i] = 1;
            }
        }

        public static Plateau GetInstance(int nbC, int nbJ)
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new Plateau(nbC, nbJ);
                    }
                }
            }
            return _instance;
        }

        static public void printBoard(int [,] tab)
        {
            /*for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i, j]);
                }
                Console.WriteLine();
            }*/
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
