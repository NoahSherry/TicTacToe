using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace assignment00
{
    public partial class Form1 : Form
    {
        #region Starting Variables
        int x;
        int y;
        int cellSize;
        int margin = 10;
        int row = -1;
        int col = -1;
        int counter = 1;
        int choice = 0;
        int songNumber = 1;
        string LastPlayer;
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8' };
        bool SongPLay = true;
        bool StartFlag = false;
        bool GameReset = false;

        SoundPlayer player = new SoundPlayer(assignment00.Properties.Resources.Hotel);
        #endregion

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            #region Game Start Message
            string GameStart = "Welcome to Tic-Tac-Toe! /Here's a short list of commands for running the game! / /Spacebar: Primes game reset. Click to activate game reset. /Q: Exit the game and close the window. /T: Change starting player. Defaults to X. /M: Enable or Disable the game music. Enabled by default. /H: Display this help menu.";
            GameStart = GameStart.Replace("/", System.Environment.NewLine);
            SoundPlayer GameOpen = new SoundPlayer(assignment00.Properties.Resources.GameStart);
            GameOpen.Play();
            MessageBox.Show(GameStart);
            PlayMusic();
            #endregion

            UpdateSize();
        }

        private void UpdateSize()
        {
            cellSize = (Math.Min(ClientSize.Width, ClientSize.Height) - 2 * margin) / 3;
            if (ClientSize.Width > ClientSize.Height)
            {
                x = (ClientSize.Width - 3 * cellSize) / 2;
                y = margin;
            }
            else
            {
                x = margin;
                y = (ClientSize.Height - 3 * cellSize) / 2;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateSize();
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (StartFlag == true)
            {
                col = (int)Math.Floor((e.X - x) * 1.0 / cellSize);
                row = (int)Math.Floor((e.Y - y) * 1.0 / cellSize);
                Refresh();
                IfWin();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (StartFlag == false)
                {
                    StartFlag = true;
                    Refresh();
                }
                else
                {
                    GameReset = true;
                    NewGame();
                    Refresh();
                    GameReset = false;
                }
            }
            if (e.KeyCode == Keys.J)
            {
                JukeBox();
                PlayMusic();
                System.GC.Collect();
            }
            if (e.KeyCode == Keys.T)
            {
                counter++;
            }
            if (e.KeyCode == Keys.Q)
            {
                if (MessageBox.Show("Are you sure you want to close the game?", "Close Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (MessageBox.Show("Are you really sure?", "Acutally Close the Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                }
            }
            if (e.KeyCode == Keys.H)
            {
                string GameStart = "Welcome to Tic-Tac-Toe! /Here's a short list of commands for running the game! / /Spacebar: Start game and reset game. /Q: Exit the game and close the window. /T: Change starting player. Defaults to X. /M: Enable or Disable the game music. Enabled by default. /H: Display this help menu.";
                GameStart = GameStart.Replace("/", System.Environment.NewLine);
                MessageBox.Show(GameStart);
            }
            if (e.KeyCode == Keys.M)
            {
                if (SongPLay == true)
                {
                    SongPLay = false;
                    PlayMusic();
                }
                else
                {
                    SongPLay = true;
                    PlayMusic();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (StartFlag == false)
            {
                e.Graphics.DrawImage(assignment00.Properties.Resources.TicStartMenu1, 0, 0, ClientSize.Width, ClientSize.Height);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Rectangle rect = new Rectangle(x + i * cellSize, y + j * cellSize, cellSize, cellSize);
                        e.Graphics.DrawRectangle(Pens.Navy, rect);
                        System.Drawing.Font font = new System.Drawing.Font("Comic Sans MS", cellSize * 3 * 72 / 96 / 4);

                        #region Crazy Switch Statement
                        switch (i)
                        {
                            case 0:
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                choice = 0;
                                                break;
                                            }
                                        case 1:
                                            {
                                                choice = 1;
                                                break;
                                            }
                                        case 2:
                                            {
                                                choice = 2;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                choice = 3;
                                                break;
                                            }
                                        case 1:
                                            {
                                                choice = 4;
                                                break;
                                            }
                                        case 2:
                                            {
                                                choice = 5;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                choice = 6;
                                                break;
                                            }
                                        case 1:
                                            {
                                                choice = 7;
                                                break;
                                            }
                                        case 2:
                                            {
                                                choice = 8;
                                                break;
                                            }
                                    }
                                    break;
                                }

                        }
                        #endregion

                        #region Marking Plays On Click
                        if (GameReset == false)
                        {
                           
                                if (i == col && j == row)
                                {
                                if (arr[choice] != 'X' && arr[choice] != 'O')
                                {
                                    if (counter % 2 == 0)
                                    {
                                        counter++;
                                        LastPlayer = "X";
                                        arr[choice] = 'X';
                                    }
                                    else
                                    {
                                        counter++;
                                        LastPlayer = "O";
                                        arr[choice] = 'O';
                                    }

                                }
                            }
                                if (arr[choice] == 'X')
                                {
                                    e.Graphics.DrawString("X", font, Brushes.DarkGreen, x + i * cellSize + 17, y + j * cellSize - 10);
                                }
                                else if (arr[choice] == 'O')
                                {
                                    e.Graphics.DrawString("O", font, Brushes.Purple, x + i * cellSize + 10, y + j * cellSize - 10);
                                }
                        }
                        #endregion

                    }
                }
            }
        }

        private void JukeBox()
        {

            if (songNumber == 10)
            {
                songNumber = 0;
            }

            switch (songNumber)
            {
                case 0:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.Hotel);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 1:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.Tetris);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 2:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.SpiderDance);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 3:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.Shop);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 4:
                    {

                        player = new SoundPlayer(assignment00.Properties.Resources.Midna);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 5:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.ShopZ);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 6:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.Darude);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 7:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.Sonic);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 8:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.RadioB);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
                case 9:
                    {
                        player = new SoundPlayer(assignment00.Properties.Resources.Ocean);
                        PlayMusic();
                        songNumber++;
                        break;
                    }
            }

        }

        private void PlayMusic()
        {
            if (SongPLay == true)
            {
                player.PlayLooping();
            }
            else
            {
                player.Stop();
            }
        }

        private void IfWin()
        {
            if (CheckWin() == 1)
            {
                string winner = String.Format("Player " + LastPlayer + " has won. /Please press Space for a new game.");
                winner = winner.Replace("/", System.Environment.NewLine);
                MessageBox.Show(winner);

            }
            else if (CheckWin() == -1)
            {
                string winner = "Nobody Wins! :(/Please press Space for a new game.";
                winner = winner.Replace("/", System.Environment.NewLine);
                MessageBox.Show(winner);
            }
        }

        private void NewGame()
        {
            for (int f = 0; f < 9; f++)
            {
                string temp = f.ToString();
                char letter = Convert.ToChar(temp);
                arr[f] = letter;
            }
            counter = 0;
        }

        private static int CheckWin()
        {
            #region Horzontal Winning Condtion
            //Winning Condition For First Row   
            if (arr[0] == arr[1] && arr[1] == arr[2])
            {
                return 1;
            }
            //Winning Condition For Second Row   
            else if (arr[3] == arr[4] && arr[4] == arr[5])
            {
                return 1;
            }
            //Winning Condition For Third Row   
            else if (arr[6] == arr[7] && arr[7] == arr[8])
            {
                return 1;
            }
            #endregion

            #region vertical Winning Condtion
            //Winning Condition For First Column       
            else if (arr[0] == arr[3] && arr[3] == arr[6])
            {
                return 1;
            }
            //Winning Condition For Second Column  
            else if (arr[1] == arr[4] && arr[4] == arr[7])
            {
                return 1;
            }
            //Winning Condition For Third Column  
            else if (arr[2] == arr[5] && arr[5] == arr[8])
            {
                return 1;
            }
            #endregion

            #region Diagonal Winning Condition
            else if (arr[0] == arr[4] && arr[4] == arr[8])
            {
                return 1;
            }
            else if (arr[2] == arr[4] && arr[4] == arr[6])
            {
                return 1;
            }
            #endregion

            #region Checking For Draw
            // If all the cells or values filled with X or O then any player has won the match  
            else if (arr[0] != '0' && arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8')
            {
                return -1;
            }
            #endregion

            else
            {
                return 0;
            }
        }
    }
}
