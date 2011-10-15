using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class PlayerShip
    {
        public Point Location;
        private Rectangle rect;
        public bool Alive = true;
        private Bitmap playersShip;
        private Bitmap[] LittlePlayerShip;

        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, playersShip.Size);
            }
        }

        public PlayerShip(Rectangle r)
        {
            rect = r;
            Location = new Point((rect.Width / 2) - 20, rect.Height - 50);
            playersShip = ResizeImage(Properties.Resources.player, 44, 23);
            LittlePlayerShip = new Bitmap[2];
            LittlePlayerShip[0] = ResizeImage(Properties.Resources.player, 44, 23);
            LittlePlayerShip[1] = ResizeImage(Properties.Resources.player, 44, 23);
        }

        public void DrawLittleShip(Graphics g, int life)
        {
            if (life == 2)
            {
                g.DrawImage(LittlePlayerShip[0], rect.Right - 45, rect.Top);
                g.DrawImage(LittlePlayerShip[1], rect.Right - 100, rect.Top);
            }
            if (life == 1)
            {
                g.DrawImage(LittlePlayerShip[0], rect.Right - 45, rect.Top);
            }
            else
                return;
        }

        public static Bitmap ResizeImage(Bitmap picture, int width, int heigth)
        {
            Bitmap resizedPicture = new Bitmap(width, heigth);

            using (Graphics graphics = Graphics.FromImage(resizedPicture))
            {
                graphics.DrawImage(picture, 0, 0, width, heigth);
            }

            return resizedPicture;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(playersShip, Location.X, Location.Y);

        }
                        
        public void Move(Direction direction)
        {
            if (direction == Direction.Left)
            {
                if (Location.X > rect.Left + 100)
                    Location.X = Location.X - 5;
            }

            else if (direction == Direction.Right)
            {
                if (Location.X < rect.Right - 100)
                Location.X = Location.X + 5;
            }
            else
                return;
        }
    }
}
