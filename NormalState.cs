using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_ANG
{
    class NormalState : State
    {
        public override void Handle1()
        {
            Console.WriteLine("NormalState handles request1.");
            Console.WriteLine("NormalState wants to change the state of the context.");
            this._player.TransitionTo(new JailState());
        }

        public override void Handle2()
        {
            Console.WriteLine("NormalState handles request2.");
        }
    }
}
