using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Auriac_Barreau
{
    class Program
    {
        static void Main(string[] args)
        {
            bool fin = false;
            Console.WriteLine("Nombre de joueurs ? ");
            int nbJoueurs = Convert.ToInt32(Console.ReadLine());
            Plateau board = Plateau.GetInstance(40, nbJoueurs);
            List<Joueur> lesJoueurs = new List<Joueur>();
            NormalState deb = new NormalState();
            Joueur init = new Joueur(deb);
            for (int i = 0; i < nbJoueurs; i++)
            {
                lesJoueurs.Add(init);
            }

            fin = isOver(lesJoueurs);

            while (!fin)
            {

            }
        }

        static public bool isOver(List<Joueur> tab)
        {
            bool b = false;
            foreach(Joueur j in tab)
            {
                if (j.Moneyyy > 100000) { b = true; }
            }
            return b;
        }
    }
}
