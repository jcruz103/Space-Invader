using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    public struct Star
    {
        public Point point;
        public Pen pen;

        public Star(Point point, Pen pen)
        {
            this.point = point;
            this.pen = pen;
        }

    }
}
