using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    delegate void GameOverCallBack(GameOverArgs e);

    class Game
    {
        private int score = 0;
        private int livesLeft = 3;
        private int wave = 0;
        private int framesSkipped = 0;

        private Rectangle boundaries;
        private Random random;
        private Point invadersLocation;
        private Point tempLocation;

        private Direction invaderDirection;
        private List<Invader> invaders;

        private PlayerShip playerShip;
        private List<Shot> playerShots;
        private List<Shot> invaderShots;
        private Shot shotToRemove;
        private Invader invaderToRemove;
        private Invader NULLINVADER;

        private Stars stars;
        private Form1 form;
        private int i = 0;
        private bool enemyPastShip;

        private bool gameOver = true;

        public event EventHandler<GameOverArgs> GameOver;
        private GameOverCallBack gameOverCallBack;


        public Game(Form1 form)
        {
            this.form = form;

            boundaries = new Rectangle();
            boundaries = form.ClientRectangle;
            random = new Random();
            invadersLocation = new Point(boundaries.Left + 175, boundaries.Top + 20);
            tempLocation = new Point();
            tempLocation = invadersLocation;
            invaderDirection = new Direction();
            invaderDirection = Direction.Left;
            framesSkipped = new int();
            wave = new int();
            framesSkipped = 6;
            wave = 0;
            NULLINVADER = new Invader(ShipType.Bug, new Point(0,0), 0);
            enemyPastShip = false;

            Constructor();
        }


        private void NewWave()
        {
            invadersLocation = tempLocation;
           
            for (int i = 0; i < 7; i++)
            {
                invaders.Add(new Invader(ShipType.Satellite, invadersLocation, 50));
                invadersLocation.X = invadersLocation.X + 80;
            }

            invadersLocation = tempLocation;
            invadersLocation.Y = invadersLocation.Y + 80;

            for (int i = 0; i < 7; i++)
            {
                invaders.Add(new Invader(ShipType.Bug, invadersLocation, 40));
                invadersLocation.X = invadersLocation.X + 80;
            }

            invadersLocation = tempLocation;
            invadersLocation.Y = invadersLocation.Y + 160;

            for (int i = 0; i < 7; i++)
            {
                invaders.Add(new Invader(ShipType.Saucer, invadersLocation, 30));
                invadersLocation.X = invadersLocation.X + 80;
            }

            invadersLocation = tempLocation;
            invadersLocation.Y = invadersLocation.Y + 240;

            for (int i = 0; i < 7; i++)
            {
                invaders.Add(new Invader(ShipType.SpaceShip, invadersLocation, 20));
                invadersLocation.X = invadersLocation.X + 80;
            }

            invadersLocation = tempLocation;
            invadersLocation.Y = invadersLocation.Y + 320;

            for (int i = 0; i < 7; i++)
            {
                invaders.Add(new Invader(ShipType.star, invadersLocation, 10));
                invadersLocation.X = invadersLocation.X + 80;
            }
        }

        private void Constructor()
        {
            stars = new Stars(boundaries, random);
            playerShip = new PlayerShip(boundaries);

            playerShots = new List<Shot>();
            invaders = new List<Invader>();
            invaderShots = new List<Shot>();

            NewWave();

           
        }

        public void GameState()
        {
            var invadYPlayerShipY =
                from invad in invaders
                where invad.Location.Y > playerShip.Location.Y
                select new { invad.Location.Y};

            GameOverStart(new GameOverCallBack(GameOverCall));

            if (livesLeft <= 0)
                gameOver = true;
            
            else
                gameOver = false;

            foreach (var invadP in invadYPlayerShipY)
            {
                gameOver = true;
                enemyPastShip = true;
            }

            
            GameOverArgs noGameOver = new GameOverArgs(gameOver);
            GameoverSubscribtion(noGameOver);
        }

        public void GameoverSubscribtion(GameOverArgs e)
        {
            if (gameOverCallBack != null)
                gameOverCallBack(e);

        }

        private void GameOverCall(GameOverArgs e)
        {
            EventHandler<GameOverArgs> gameOver = GameOver;
            if (gameOver != null)
                gameOver(this, e);

        }

        public void GameOverStart(GameOverCallBack gameO)
        {
            this.gameOverCallBack = new GameOverCallBack(gameO);
            
        }

        public void Draw(Graphics g, int animationCell)
        {

            g.FillRectangle(Brushes.Black, boundaries);
            stars.Draw(g);
            playerShip.DrawLittleShip(g, livesLeft - 1);

            if (playerShip.Alive)
            {
                playerShip.Draw(g);
            }

            if (!playerShip.Alive && livesLeft > 0)
            {
                for (int i = 0; i < 5; i++)
                {

                }

                playerShip.Alive = true;
            }

            Font areial12 = new Font("Arial", 12);
            Font areial50 = new Font("Arial", 50);

            foreach (Invader invad in invaders)
            {
                invad.Draw(g, animationCell);
            }

            foreach (Shot shot in playerShots)
            {
                shot.Draw(g);
            }

            foreach (Shot shot in playerShots)
            {
                if (!shot.Move())
                {
                    RemoveShot(shot);
                    break;
                }
            }

            foreach (Shot shotInvader in invaderShots)
            {
                shotInvader.Draw(g);
            }

            foreach (Shot shotInvader in invaderShots)
            {
                if (!shotInvader.Move2())
                {
                    RemoveInvaderShot(shotInvader);
                    break;
                }
            }
                        

            if (gameOver && enemyPastShip)
            {
                g.DrawString("Press R to restart or Q to quit", areial12, Brushes.Yellow, boundaries.Height - 125, boundaries.Width - 200);
                g.DrawString("Game Over", areial50, Brushes.Yellow, (boundaries.Height / 2) - 150, (boundaries.Width / 2) - 100);
            }

            else if (gameOver && livesLeft == 0)
            {
                g.DrawString("Press R to restart or Q to quit", areial12, Brushes.Yellow, boundaries.Height - 125, boundaries.Width - 200);
                g.DrawString("Game Over", areial50, Brushes.Yellow, (boundaries.Height / 2) - 150, (boundaries.Width / 2) - 100);
            }

            else if (gameOver && livesLeft == 3)
            {

                g.DrawString("Press S to start a new Game or Q to quit", areial12, Brushes.Yellow, boundaries.Height - 125, boundaries.Width - 200);

            }


            g.DrawString(score.ToString(), areial12, Brushes.Yellow, boundaries.Top, boundaries.Left);
        }

        private void RemoveShot(Shot shot)
        {
            playerShots.Remove(shot);
        }

        private void RemoveInvaderShot(Shot shot)
        {
            invaderShots.Remove(shot);
        }

        private void RemoveInvader(Invader invader)
        {
            try
            {
                score += invader.Score;
            }
            catch (Exception e)
            {

            }
            invaders.Remove(invader);
        }

        public void Twinkle()
        {
            stars.Twinkle(random);
        }

        public void Go()
        {
            CheckForInvaderCollisions();
            ReturnFire();
            CheckForPlayerCollisions();

            i++;
            if (framesSkipped == i)
            {
                MoveInvader();
                i = 0;
            }

            if (invaders.Count() <= 0)
            {
                NewWave();
                wave++;
            }

            if (gameOver)
                return;
                
        }

        public void FireShot()
        {
            if (playerShots.Count() < 2)
            {
                Shot shot = new Shot(playerShip.Location, Direction.Up, boundaries);
                playerShots.Add(shot);
            }
        }

        public void MovePlayer(Direction direction)
        {
            playerShip.Move(direction);
        }

        private int MoveInvader()
        {
            int i = 0;
            var InvadersRight =
                from invadX in invaders
                select new { invadX.Location.X };

            foreach (var invadLocation in InvadersRight)
            {
                if (invadLocation.X > boundaries.Right - 150)
                {
                    invaderDirection = Direction.Down;
                    i = 1;
                    break;
                    
                }
                if (invadLocation.X < boundaries.Left + 100)
                {
                    invaderDirection = Direction.Down;
                    i = 2;
                    break;
                }

            }

            foreach (Invader invad in invaders)
                invad.Move(invaderDirection);

            if (invaderDirection == Direction.Down && i == 1)
            {
                invaderDirection = Direction.Left;
            }
            if (invaderDirection == Direction.Down && i == 2)
            {
                invaderDirection = Direction.Right;
            }

            foreach (Invader invad in invaders)
                invad.Move(invaderDirection);

            if (wave == 0)
                framesSkipped = 8;
            if (wave == 1)
                framesSkipped = 7;
            if (wave == 2)
                framesSkipped = 6;

            return framesSkipped;
        }

        private void CheckForInvaderCollisions()
        {

            foreach (Invader invaderA in invaders)
            {
                foreach (Shot shot in playerShots)
                {
                    if (invaderA.Area.Contains(shot.Location))
                    {
                        shotToRemove = shot;
                        invaderToRemove = invaderA;
                    }

                    if (invaderA.Location.X > playerShip.Location.X)
                        gameOver = true;

                }
            }

            RemoveShot(shotToRemove);
            RemoveInvader(invaderToRemove);
            invaderToRemove = NULLINVADER;
        }

        private void CheckForPlayerCollisions()
        {
            foreach (Shot shot in invaderShots)
            {
                if (playerShip.Area.Contains(shot.Location))
                {
                    shotToRemove = shot;
                    playerShip.Alive = false;
                    livesLeft -= 1;
                }
            }

            RemoveInvaderShot(shotToRemove);
        }

        private void ReturnFire()
        {
            var invaderClosestToPlayer =
                from invaderCTP in invaders
                orderby invaderCTP.Location.Y descending
                select new { invaderCTP };

            if (random.Next(10) < 5)
            {
                if (invaderShots.Count() < 2)
                {
                    Shot shot = new Shot(invaderClosestToPlayer.First().invaderCTP.Location, Direction.Down, boundaries);
                    invaderShots.Add(shot);
                }
            }
        }
    }
}
