using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class Invader
    {
        private const int HorizontalInterval = 10;
        private const int VerticalInterval = 40;

        private Bitmap image;

        public Point Location { get; private set; }

        public ShipType InvaderType { get; private set; }

        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, image.Size);
            }
        }

        public int Score { get; private set; }

        public Invader(ShipType invaderType, Point location, int score)
        {
            this.InvaderType = invaderType;
            this.Location = location;
            this.Score = score;
            image = InvaderImage(0);            
         }

        public void Move(Direction direction)
        {
            if (direction == Direction.Left)
            {
                    Location = new Point(Location.X - VerticalInterval, Location.Y);
            }

            else if (direction == Direction.Right)
            {
                Location = new Point(Location.X + VerticalInterval, Location.Y);
            }

            else if (direction == Direction.Down)
            {
                Location = new Point(Location.X, Location.Y + HorizontalInterval);
            }

            
        }

        public void Draw(Graphics g, int animationCell)
        {
            image = InvaderImage(animationCell);
            g.DrawImage(image, Location);
        }

        private Bitmap InvaderImage(int animationCell)
        {
            if(InvaderType == ShipType.Bug)
            {
                if (animationCell == 0)
                    image = ResizeImage(Properties.Resources.bug1, 35, 41);

                if (animationCell == 1)
                    image = ResizeImage(Properties.Resources.bug2, 35, 41);

                if (animationCell == 2)
                    image = ResizeImage(Properties.Resources.bug3, 35, 41);

                if (animationCell == 3)
                    image = ResizeImage(Properties.Resources.bug4, 35, 41);
                                               
            }

            if (InvaderType == ShipType.Satellite)
            {
                if (animationCell == 0)
                    image = ResizeImage(Properties.Resources.satellite1, 40, 39);

                if (animationCell == 1)
                    image = ResizeImage(Properties.Resources.satellite2, 40, 39);

                if (animationCell == 2)
                    image = ResizeImage(Properties.Resources.satellite3, 40, 39);

                if (animationCell == 3)
                    image = ResizeImage(Properties.Resources.satellite4, 40, 39);

            }

            if (InvaderType == ShipType.Saucer)
            {
                if (animationCell == 0)
                    image = ResizeImage(Properties.Resources.flyingsaucer1, 29, 35);

                if (animationCell == 1)
                    image = ResizeImage(Properties.Resources.flyingsaucer2, 29, 35);

                if (animationCell == 2)
                    image = ResizeImage(Properties.Resources.flyingsaucer3, 29, 35);

                if (animationCell == 3)
                    image = ResizeImage(Properties.Resources.flyingsaucer4, 29, 35);

            }

            if (InvaderType == ShipType.SpaceShip)
            {
                if (animationCell == 0)
                    image = ResizeImage(Properties.Resources.spaceship1, 43, 33);

                if (animationCell == 1)
                    image = ResizeImage(Properties.Resources.spaceship2, 43, 33);

                if (animationCell == 2)
                    image = ResizeImage(Properties.Resources.spaceship3, 43, 33);

                if (animationCell == 3)
                    image = ResizeImage(Properties.Resources.spaceship4, 43, 33);

            }

            if (InvaderType == ShipType.star)
            {
                if (animationCell == 0)
                    image = ResizeImage(Properties.Resources.star1, 41, 41);

                if (animationCell == 1)
                    image = ResizeImage(Properties.Resources.star2, 41, 41);

                if (animationCell == 2)
                    image = ResizeImage(Properties.Resources.star3, 41, 41);

                if (animationCell == 3)
                    image = ResizeImage(Properties.Resources.star4, 41, 41);
            }


            return image;
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
    }
}
