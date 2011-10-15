using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class Stars
    {
        public List<Star> listOfStars;
        private List<Star> tempRemovedStars;
        private Rectangle rect;
        private Point point;
        private Random random;
        private Pen pen;
        private Star star;
        private Star tempStar;
        private Graphics grap;

        public Stars(Rectangle rect, Random r)
        {
            this.rect = rect;
            random = r;
            listOfStars = new List<Star>();
            star = new Star();
            tempStar = new Star();
        }

        public void Draw(Graphics g)
        {
            grap = g;
            for (int i = 0; i < 401; i++)
            {
                RandomPen();
                if (listOfStars.Count >= 300)
                    break;
            }

            using (Pen penWhite = new Pen(Color.White, 3))
            using (Pen penYellow = new Pen(Color.Yellow, 3))
            using (Pen penGold = new Pen(Color.Gold, 3))
            using (Pen penGray = new Pen(Color.Gray, 3))
            {
                foreach (Star starToDraw in listOfStars)
                {
                    tempStar = starToDraw;
                    tempStar.point.X = starToDraw.point.X + 1;
                    if (star.pen.Color == penWhite.Color)
                        pen = penWhite;
                    if (star.pen.Color == penYellow.Color)
                        pen = penYellow;
                    if (star.pen.Color == penGold.Color)
                        pen = penGold;
                    if (star.pen.Color == penGray.Color)
                        pen = penGray;

                    g.DrawLine(pen, tempStar.point, starToDraw.point);
                }

            }

           
        }

        public void RandomPen()
        {
            using (Pen penWhite = new Pen(Color.White, 3))
            using (Pen penYellow = new Pen(Color.Yellow, 3))
            using (Pen penGold = new Pen(Color.Gold, 3))
            using (Pen penGray = new Pen(Color.Gray, 3))
            {
                    switch (random.Next(5))
                    {
                        case 1: point = new Point(random.Next(rect.Width), random.Next(rect.Height));
                            star = new Star(point, penWhite);
                            listOfStars.Add(star);
                            break;
                        case 2: point = new Point(random.Next(rect.Width), random.Next(rect.Height));
                            star = new Star(point, penYellow);
                            listOfStars.Add(star);
                            break;
                        case 3: point = new Point(random.Next(rect.Width), random.Next(rect.Height));
                            star = new Star(point, penGold);
                            listOfStars.Add(star);
                            break;
                        case 4: point = new Point(random.Next(rect.Width), random.Next(rect.Height));
                            star = new Star(point, penGray);
                            listOfStars.Add(star);
                            break;
                    }
            }
            
        }

        public void Twinkle(Random random)
        {
            try
            {
                listOfStars.RemoveRange(random.Next(300), 5);
            }
            catch (Exception e)
            {
                return;
            }
        }

    }
}
