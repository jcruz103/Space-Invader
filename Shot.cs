using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class Shot
    {
        private const int moveInterval = 65;
        private const int width = 5;
        private const int height = 15;

        public Point Location { get; private set; }

        private Direction direction;
        private Rectangle boundaries;

        public Shot(Point location, Direction directoin, Rectangle rect)
        {
            this.Location = location;
            this.direction = directoin;
            this.boundaries = rect;
        }

        public void Draw(Graphics g)
        {
                using (Pen pen = new Pen(Brushes.Yellow))
                {
                    g.DrawRectangle(pen, Location.X, Location.Y, width, height);
                    g.FillRectangle(Brushes.Yellow, Location.X, Location.Y, width, height);
                }
        }

      
        public bool Move()
        {
            if (boundaries.Top < Location.Y)
            {
                Location = new Point(Location.X, Location.Y - moveInterval);
                return true;
            }
            else
                return false;
        }

        public bool Move2()
        {
            if (boundaries.Bottom > Location.Y)
            {
                Location = new Point(Location.X, Location.Y + moveInterval);
                return true;
            }
            else
                return false;
        }
    }
}
