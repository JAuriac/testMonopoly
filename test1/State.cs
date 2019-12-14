using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Auriac_Barreau
{
    abstract class State
    {
        protected Joueur _joueur;

        public void SetJoueur(Joueur joueur)
        {
            this._joueur = joueur;
        }

        public abstract void Handle1();

        public abstract void Handle2();
    }
}
