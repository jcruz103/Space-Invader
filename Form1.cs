using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        private Game game;
        private List<Keys> keysPressed = new List<Keys>();
        private bool gameOver = true;
        private int frame = 0;
        private int cell = 0;

        public Form1()
        {
            InitializeComponent();
            Constructor();
        }

        private void Constructor()
        {
            animationTimer.Interval = 100;
            animationTimer.Enabled = true;
            gameTimer.Enabled = false;
            gameTimer.Interval = 50;
            game = new Game(this);
            game.GameOver += new EventHandler<GameOverArgs>(game_GameOver);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            frame++;
            if (frame >= 6)
                frame = 0;

            switch (frame)
            {
                case 0: cell = 0; break;
                case 1: cell = 1; break;
                case 2: cell = 2; break;
                case 3: cell = 3; break;
                case 4: cell = 2; break;
                case 5: cell = 1; break;
                default: cell = 0; break;
            }

            game.Draw(e.Graphics, cell);

        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            game.Twinkle();
            this.Refresh();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            game.Go();
            game.GameState();

            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Left)
                {
                    game.MovePlayer(Direction.Left);
                    return;
                }
                else if (key == Keys.Right)
                {
                    game.MovePlayer(Direction.Right);
                    return;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();
            if (gameOver)
                if (e.KeyCode == Keys.S)
                {
                    gameTimer.Enabled = true;
                    animationTimer.Enabled = true;
                    gameOver = false;
                }
                else if (e.KeyCode == Keys.R)
                {
                    Constructor();
                }
                else
                    return;

            if (e.KeyCode == Keys.Space)
                game.FireShot();
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
            keysPressed.Add(e.KeyCode);

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }

        void game_GameOver(object sender, GameOverArgs e)
        {
            GameOverArgs args = e as GameOverArgs;
            this.gameOver = args.gameOver;

            if (gameOver == true)
                gameTimer.Enabled = false;
                                 

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            game = new Game(this);
        }
    }
}
