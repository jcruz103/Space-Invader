using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class GameOverArgs : EventArgs
    {
        public bool gameOver {get; private set;}

        public GameOverArgs(bool gameOver)
        {
            this.gameOver = gameOver;
        }
    }
}
