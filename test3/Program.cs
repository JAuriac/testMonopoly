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
            Console.WriteLine("Rules : Each time a player finish its move in a residential area or an hotel area,\nthe player can buy one house or one hotel respectively.");
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
            /*for (int i = 0; i < nbJoueurs; i++)
            {
                lesJoueurs.Add(new Joueur(deb));
            }*/
            int posInit = 0;
            String nom = "";
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.WriteLine("Quel est le nom du joueur " + i +"?");
                nom = Console.ReadLine();
                Console.WriteLine("Quel est la position initiale du joueur " + i + "? (De 0 à 39, sauf 30)");
                posInit = Convert.ToInt32(Console.ReadLine());
                lesJoueurs.Add(new Joueur(deb, nom, posInit));
                for (int j = 0; j < board.Grille.GetLength(0); j++) { board.Grille[j, i] = 0; } board.Grille[posInit, i] = 1;
            }
            int d;
            int whichTurn = 0;
            int numJ;
            bool b;
            while (!isOver(lesJoueurs))
            {
                numJ = whichTurn + 1;
                //Console.WriteLine("C'est le tour du joueur " + numJ + ".");
                Console.WriteLine("C'est le tour de " + lesJoueurs[whichTurn].Name + ".");
                Console.ReadKey();
                d = lancerDés();
                if (d == 99) { toJailSir(lesJoueurs[whichTurn]); for (int i = 0; i < board.Grille.GetLength(0); i++) { board.Grille[i, whichTurn] = 0; } board.Grille[10, whichTurn] = 1; }
                if (lesJoueurs[whichTurn].getState() == deb)
                {
                    Console.WriteLine("Le joueur " + lesJoueurs[whichTurn].Name + " a lancé les dés : " + d);
                    for (int i = 0; i < board.Grille.GetLength(0); i++) { board.Grille[i, whichTurn] = 0; }
                    if (d + lesJoueurs[whichTurn].Position >= 40) { lesJoueurs[whichTurn].Position -= 39; lesJoueurs[whichTurn].Moneyyy += lesJoueurs[whichTurn].Income; } //Income / 10000
                    board.Grille[d + lesJoueurs[whichTurn].Position, whichTurn] = 1;
                    lesJoueurs[whichTurn].Position = d + lesJoueurs[whichTurn].Position;
                    if (lesJoueurs[whichTurn].Position == 30) { toJailSir(lesJoueurs[whichTurn]); for (int i = 0; i < board.Grille.GetLength(0); i++) { board.Grille[i, whichTurn] = 0; } board.Grille[10, whichTurn] = 1; }
                    if (lesJoueurs[whichTurn].Position%5 == 0 && lesJoueurs[whichTurn].Position != 10 && lesJoueurs[whichTurn].Position != 30) { wannaHotel(lesJoueurs[whichTurn]); }
                    if (lesJoueurs[whichTurn].Position % 2 == 0 && lesJoueurs[whichTurn].Position != 10 && lesJoueurs[whichTurn].Position != 30 && lesJoueurs[whichTurn].Position % 5 != 0) { wannaHouse(lesJoueurs[whichTurn]); }
                }
                else
                {
                    if (lesJoueurs[whichTurn].GetOut != 0)
                    {
                        b = lancerDésPrison();
                        while (true)
                        {
                            if (b == true) { Console.WriteLine("Quelle chance! Le joueur " + lesJoueurs[whichTurn].Name + " sort de prison. "); lesJoueurs[whichTurn].GetOut = 0; lesJoueurs[whichTurn].TransitionTo(deb); break; }
                            else { lesJoueurs[whichTurn].GetOut++; }
                            if (lesJoueurs[whichTurn].GetOut == 3) { Console.WriteLine("Le joueur a purgé sa peine. Le joueur " + lesJoueurs[whichTurn].Name + " sort de prison. "); lesJoueurs[whichTurn].GetOut = 0; lesJoueurs[whichTurn].TransitionTo(deb); break; }
                            else { Console.WriteLine("Le joueur " + lesJoueurs[whichTurn].Name + " reste en prison."); break; }
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
            //int compteur = 1;
            foreach(Joueur j in tab)
            {
                Console.WriteLine("Le joueur " + j.Name + " a un portefeuille de : " + j.Moneyyy +".");
                //Console.WriteLine("Le joueur " + compteur + " a un portefeuille de : " + j.Moneyyy + ".");
                //compteur++;
            }
            Console.WriteLine();
        }

        static public void wannaHotel(Joueur who)
        {
            Console.WriteLine("Does the player " + who.Name + " want to buy a hotel y/n ? price : 50000, income : 20000");
            string rep = Console.ReadLine();
            if ((rep == "y" || rep == "Y") && who.Moneyyy >= 50000) { who.Moneyyy -= 50000; who.Income += 20000; }
            else if ((rep == "y" || rep == "Y") && who.Moneyyy < 50000) { Console.WriteLine("Not enough funds."); }
        }

        static public void wannaHouse(Joueur who)
        {
            Console.WriteLine("Does the player " + who.Name + " want to buy a house y/n ? price : 20000, income : 50000");
            string rep = Console.ReadLine();
            if ((rep == "y" || rep == "Y") && who.Moneyyy >= 20000) { who.Moneyyy -= 20000; who.Income += 5000; }
            else if ((rep == "y" || rep == "Y") && who.Moneyyy < 20000) { Console.WriteLine("Not enough funds."); }
        }
    }
}
