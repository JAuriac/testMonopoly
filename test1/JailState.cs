using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Auriac_Barreau
{
    class JailState : State
    {
        public override void Handle1()
        {
            Console.Write("JailState handles request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("JailState handles request2.");
            Console.WriteLine("JailState wants to change the state of the context.");
            this._joueur.TransitionTo(new NormalState());
        }
    }
}
