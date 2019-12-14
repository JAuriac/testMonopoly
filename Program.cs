using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly_ANG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rules : Each time a player finishes its move in a residential area or an hotel area,\nthe player can buy one house or one hotel respectively.");
            Console.WriteLine("Number of players ? ");
            int nbJoueurs = Convert.ToInt32(Console.ReadLine());
            Board board = Board.GetInstance(40, nbJoueurs);
            List<Player> lesJoueurs = new List<Player>();
            NormalState deb = new NormalState();
            int posInit = 0;
            String nom = "";
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.WriteLine("What is the name of the player " + i + "?");
                nom = Console.ReadLine();
                Console.WriteLine("What is the initial position of the player " + i + "? (From 0 to 39, except 30)");
                posInit = Convert.ToInt32(Console.ReadLine());
                lesJoueurs.Add(new Player(deb, nom, posInit));
                for (int j = 0; j < board.Grid.GetLength(0); j++) { board.Grid[j, i] = 0; }
                board.Grid[posInit, i] = 1;
            }
            int d;
            int whichTurn = 0;
            int numJ;
            bool b;
            while (!isOver(lesJoueurs))
            {
                numJ = whichTurn + 1;
                Console.WriteLine("It's the turn of the player " + lesJoueurs[whichTurn].Name + ".");
                Console.ReadKey();
                d = lancerDés();
                if (d == 99) { toJailSir(lesJoueurs[whichTurn]); for (int i = 0; i < board.Grid.GetLength(0); i++) { board.Grid[i, whichTurn] = 0; } board.Grid[10, whichTurn] = 1; }
                if (lesJoueurs[whichTurn].getState() == deb)
                {
                    Console.WriteLine("The player " + lesJoueurs[whichTurn].Name + " launched the dice : " + d);
                    for (int i = 0; i < board.Grid.GetLength(0); i++) { board.Grid[i, whichTurn] = 0; }
                    if (d + lesJoueurs[whichTurn].Position >= 40) { lesJoueurs[whichTurn].Position -= 39; lesJoueurs[whichTurn].Moneyyy += lesJoueurs[whichTurn].Income; }
                    board.Grid[d + lesJoueurs[whichTurn].Position, whichTurn] = 1;
                    lesJoueurs[whichTurn].Position = d + lesJoueurs[whichTurn].Position;
                    if (lesJoueurs[whichTurn].Position == 30) { toJailSir(lesJoueurs[whichTurn]); for (int i = 0; i < board.Grid.GetLength(0); i++) { board.Grid[i, whichTurn] = 0; } board.Grid[10, whichTurn] = 1; }
                    if (lesJoueurs[whichTurn].Position % 5 == 0 && lesJoueurs[whichTurn].Position != 10 && lesJoueurs[whichTurn].Position != 30) { wannaHotel(lesJoueurs[whichTurn]); }
                    if (lesJoueurs[whichTurn].Position % 2 == 0 && lesJoueurs[whichTurn].Position != 10 && lesJoueurs[whichTurn].Position != 30 && lesJoueurs[whichTurn].Position % 5 != 0) { wannaHouse(lesJoueurs[whichTurn]); }
                }
                else
                {
                    if (lesJoueurs[whichTurn].GetOut != 0)
                    {
                        b = lancerDésPrison();
                        while (true)
                        {
                            if (b == true) { Console.WriteLine("How lucky! The player " + lesJoueurs[whichTurn].Name + " get out of jail. "); lesJoueurs[whichTurn].GetOut = 0; lesJoueurs[whichTurn].TransitionTo(deb); break; }
                            else { lesJoueurs[whichTurn].GetOut++; }
                            if (lesJoueurs[whichTurn].GetOut == 3) { Console.WriteLine("The player has served his sentence. The player " + lesJoueurs[whichTurn].Name + " get out of jail."); lesJoueurs[whichTurn].GetOut = 0; lesJoueurs[whichTurn].TransitionTo(deb); break; }
                            else { Console.WriteLine("The player " + lesJoueurs[whichTurn].Name + " stays in jail."); break; }
                        }
                    }
                    else { lesJoueurs[whichTurn].GetOut++; }
                }
                Board.printBoard(board.Grid);
                if (whichTurn == nbJoueurs - 1) { whichTurn = 0; endOfTurn(lesJoueurs); }
                else { whichTurn++; Console.WriteLine(); }
            }
            Console.WriteLine("Game over.");
        }

        static public bool isOver(List<Player> tab)
        {
            bool b = false;
            foreach (Player j in tab)
            {
                if (j.Moneyyy >= 100000) { b = true; }
            }
            return b;
        }

        static public int lancerDés()
        {
            int zePrison = 0;
            int result = 0;
            while (true)
            {
                if (zePrison == 3) { result = 99; Console.WriteLine("Triple"); break; }
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
            Console.Write("The first die made a : " + un);
            int deux = RNG.Next(1, 6);
            Console.WriteLine(" and the second made a : " + deux);
            if (un == deux) { result = true; }
            return result;
        }

        static public void toJailSir(Player who)
        {
            Console.WriteLine("HereInJail");
            who.Position = 10;
            who.Moneyyy -= 10000;
            who.TransitionTo(new JailState());
        }

        static public void endOfTurn(List<Player> tab)
        {
            Console.WriteLine("\nEnd of turn");
            foreach (Player j in tab)
            {
                Console.WriteLine("The player " + j.Name + " has a wallet of : " + j.Moneyyy + ".");
            }
            Console.WriteLine();
        }

        static public void wannaHotel(Player who)
        {
            Console.WriteLine("Does the player " + who.Name + " wants to buy a hotel y/n ? price : 50000, income : 20000");
            string rep = Console.ReadLine();
            if ((rep == "y" || rep == "Y") && who.Moneyyy >= 50000) { who.Moneyyy -= 50000; who.Income += 20000; }
            else if ((rep == "y" || rep == "Y") && who.Moneyyy < 50000) { Console.WriteLine("Not enough funds."); }
        }

        static public void wannaHouse(Player who)
        {
            Console.WriteLine("Does the player " + who.Name + " wants to buy a house y/n ? price : 20000, income : 50000");
            string rep = Console.ReadLine();
            if ((rep == "y" || rep == "Y") && who.Moneyyy >= 20000) { who.Moneyyy -= 20000; who.Income += 5000; }
            else if ((rep == "y" || rep == "Y") && who.Moneyyy < 20000) { Console.WriteLine("Not enough funds."); }
        }
    }
}
