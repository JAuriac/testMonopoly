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
        int[,] grille;
        private static Object _mutex = new Object();


        private Plateau(int nbC, int nbJ)
        {
            grille = new int[nbC, nbJ];
            for (int i = 0; i < nbJ; i++)
            {
                grille[0, nbJ] = 1;
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
    }
}
