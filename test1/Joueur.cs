using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Auriac_Barreau
{
    class Joueur
    {
        private State _state = null;
        private int moneyyy;

        public int Moneyyy { get { return moneyyy; } set { moneyyy = value; } }

        public Joueur(State state)
        {
            this.TransitionTo(state);
        }

        public void TransitionTo(State state)
        {
            Console.WriteLine($"Joueur: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetJoueur(this);
            this.moneyyy = 10000;
        }

        public void Request1()
        {
            this._state.Handle1();
        }

        public void Request2()
        {
            this._state.Handle2();
        }
    }
}
