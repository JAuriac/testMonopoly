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
        private int position;
        private int moneyyy;
        private int getOut;
        private int income;
        private string name;

        public int Position { get { return position; } set { position = value; } }
        public int Moneyyy { get { return moneyyy; } set { moneyyy = value; } }
        public int GetOut { get { return getOut; } set { getOut = value; } }
        public int Income { get { return income; } set { income = value; } }
        public string Name { get { return name; } set { name = value; } }

        public Joueur(State state, string n, int p)
        {
            this.TransitionTo(state);
            this.position = p;
            this.getOut = 0;
            this.name = n;
            this.moneyyy = 10000;
            this.income = 10000;
        }

        public void TransitionTo(State state)
        {
            Console.WriteLine($"Joueur: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetJoueur(this);
        }

        public void Request1()
        {
            this._state.Handle1();
        }

        public void Request2()
        {
            this._state.Handle2();
        }

        public State getState()
        {
           return _state;
        }
    }
}
