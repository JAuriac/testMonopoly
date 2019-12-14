using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly_Auriac_Barreau
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine(lancerDés());
            int milliseconds = 200;
            Thread.Sleep(milliseconds);
            Console.WriteLine(lancerDés());*/
            bool fin = false;
            Console.WriteLine("Nombre de joueurs ? ");
            int nbJoueurs = Convert.ToInt32(Console.ReadLine());
            Plateau board = Plateau.GetInstance(40, nbJoueurs);
            List<Joueur> lesJoueurs = new List<Joueur>();
            NormalState deb = new NormalState();
            /*Joueur init = new Joueur(deb);
            for (int i = 0; i < nbJoueurs; i++)
            {
                lesJoueurs.Add(init);
            }*/
            for (int i = 0; i < nbJoueurs; i++)
            {
                lesJoueurs.Add(new Joueur(deb));
            }
            /*String nom = "";
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.WriteLine("Quel est le nom du joueur " + i);
                nom = Console.ReadLine();
                lesJoueurs.Add(init, nom);
            }*/
            int d;
            int whichTurn = 0;
            int numJ;
            bool b;
            while (!isOver(lesJoueurs))
            {
                numJ = whichTurn + 1;
                Console.WriteLine("C'est le tour du joueur " + numJ + ".");
                Console.ReadKey();
                d = lancerDés();
                if (d == 99) { toJailSir(lesJoueurs[whichTurn]); for (int i = 0; i < board.Grille.GetLength(0); i++) { board.Grille[i, whichTurn] = 0; } board.Grille[10, whichTurn] = 1; }
                if (lesJoueurs[whichTurn].getState() == deb)
                {
                    Console.WriteLine("Le joueur a lancé les dés : " + d);
                    for (int i = 0; i < board.Grille.GetLength(0); i++) { board.Grille[i, whichTurn] = 0; }
                    if (d + lesJoueurs[whichTurn].Position >= 40) { lesJoueurs[whichTurn].Position -= 39; lesJoueurs[whichTurn].Moneyyy += 10000; } //Income
                    board.Grille[d + lesJoueurs[whichTurn].Position, whichTurn] = 1;
                    lesJoueurs[whichTurn].Position = d + lesJoueurs[whichTurn].Position;
                    if (lesJoueurs[whichTurn].Position == 30) { toJailSir(lesJoueurs[whichTurn]); for (int i = 0; i < board.Grille.GetLength(0); i++) { board.Grille[i, whichTurn] = 0; } board.Grille[10, whichTurn] = 1; }
                    
                }
                else
                {
                    if (lesJoueurs[whichTurn].GetOut != 0)
                    {
                        b = lancerDésPrison();
                        while (true)
                        {
                            if (b == true) { Console.WriteLine("Quelle chance! Le joueur " + numJ + " sort de prison. "); lesJoueurs[whichTurn].GetOut = 0; lesJoueurs[whichTurn].TransitionTo(deb); break; }
                            else { lesJoueurs[whichTurn].GetOut++; }
                            if (lesJoueurs[whichTurn].GetOut == 3) { Console.WriteLine("Le joueur a purgé sa peine. Le joueur " + numJ + " sort de prison. "); lesJoueurs[whichTurn].GetOut = 0; lesJoueurs[whichTurn].TransitionTo(deb); break; }
                            else { Console.WriteLine("Le joueur " + numJ + " reste en prison."); break; }
                        }
                    }
                    else { lesJoueurs[whichTurn].GetOut++; }
                }
                Plateau.printBoard(board.Grille);
                if (whichTurn == nbJoueurs - 1) { whichTurn = 0; endOfTurn(lesJoueurs); }
                else { whichTurn++; Console.WriteLine(); }
            }
            Console.WriteLine("Partie finie");
        }

        static public bool isOver(List<Joueur> tab)
        {
            bool b = false;
            foreach(Joueur j in tab)
            {
                if (j.Moneyyy >= 100000) { b = true; }
            }
            return b;
        }

        static public int lancerDés()
        {
            int zePrison = 0;
            int result = 0;
            while(true)
            {
                if ( zePrison == 3) { result = 99; Console.WriteLine("Triplé"); break; }
                Random RNG = new Random();
                int un = RNG.Next(1, 6);
                int deux = RNG.Next(1, 6);
                result += un + deux;
                if (un != deux) { break; }
                else { zePrison++; }
            }
            return result;
        }

        static public bool lancerDésPrison()
        {
            bool result = false;
            Random RNG = new Random();
            int un = RNG.Next(1, 6);
            Console.Write("Le premier dé a fait un : " + un);
            int deux = RNG.Next(1, 6);
            Console.WriteLine(" et le deuxième dé a fait un : " + deux);
            if (un == deux) { result = true; }
            return result;
        }

        static public void toJailSir(Joueur who)
        {
            Console.WriteLine("HereInJail");
            who.Position = 10;
            who.Moneyyy -= 10000;
            who.TransitionTo(new JailState());
        }

        static public void endOfTurn(List<Joueur> tab)
        {
            Console.WriteLine("\nFin du tour");
            int compteur = 1;
            foreach(Joueur j in tab)
            {
                Console.WriteLine("Le joueur " + compteur + " a un portefeuille de : " + j.Moneyyy +".");
                compteur++;
            }
            Console.WriteLine();
        }
    }
}
