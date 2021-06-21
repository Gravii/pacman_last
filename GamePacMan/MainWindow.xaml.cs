using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GamePacMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Весь ниженаписанный бред будет распихан по классам и переделан
        // Это пока что первоначальная версия этого позора
        // И разработчик данной, прости господи, "игры" вам крайне не рекомендует её проверять
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool left, right, down, up;
        bool stopLeft, stopRight, stopDown, stopUp;

        int speed = 10;
        Rect pacmanInformation;
        int ghostSpeed = 10;
        int ghostMoveStep = 100;
        int currentGhostStep;
        int score = 0; 
        Game game;
        public MainWindow()
        {
            InitializeComponent();
            //game = new Game();
            GameSetUp();
        }

        private void keyDownCanvas(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Left && stopLeft == false)
            {
                stopRight = stopUp = stopDown = false;
                right = up = down = false;

                left = true;
                pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Right && stopRight == false)
            {
                stopLeft = stopDown = stopUp = false;
                left = down = up = false;

                right = true;
                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Up && stopUp == false)
            {
                stopLeft = stopDown = stopRight = false;
                left = down = right = false;

                up = true;
                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Down && stopDown == false)
            {
                stopLeft = stopRight = stopUp = false;
                left = right = up = false;

                down = true;
                pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
            }
        }

        private void GameSetUp()
        {
            mainCanvas.Focus();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            currentGhostStep = ghostMoveStep;

            ImageBrush pacmanPicture = new ImageBrush();
            pacmanPicture.ImageSource = new BitmapImage(new Uri("C:/Users/Uik11/source/repos/GamePacMan/GamePacMan/Picture/Pacman.png"));
            pacman.Fill = pacmanPicture;

            ImageBrush redGhostPicture = new ImageBrush();
            redGhostPicture.ImageSource = new BitmapImage(new Uri("C:/Users/Uik11/source/repos/GamePacMan/GamePacMan/Picture/GhoustRed.png"));
            ghoustRed.Fill = redGhostPicture;

            ImageBrush yellowGhostPicture = new ImageBrush();
            yellowGhostPicture.ImageSource = new BitmapImage(new Uri("C:/Users/Uik11/source/repos/GamePacMan/GamePacMan/Picture/GhoustYellow.png"));
            ghoustYellow.Fill = yellowGhostPicture;

            ImageBrush blueGhostPicture = new ImageBrush();
            blueGhostPicture.ImageSource = new BitmapImage(new Uri("C:/Users/Uik11/source/repos/GamePacMan/GamePacMan/Picture/GhoustBlue.png"));
            ghoustBlue.Fill = blueGhostPicture;


        }

        private void GameLoop(object sender, EventArgs e)
        {
            txtScore.Content = "Очки: " + score;

            if (right)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + speed);
            }
            if (left)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - speed);
            }
            if (up)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
            }
            if (down)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) + speed);
            }


            if (down && Canvas.GetTop(pacman) + 80 > Application.Current.MainWindow.Height)
            {
                stopDown = true;
                down = false;
            }
            if (up && Canvas.GetTop(pacman) < 1)
            {
                stopUp = true;
                up = false;
            }
            if (left && Canvas.GetLeft(pacman) < 1)
            {
                stopLeft = true;
                left = false;
            }
            if (right && Canvas.GetLeft(pacman) + 60 > Application.Current.MainWindow.Width)
            {
                stopRight = true;
                right = false;
            }

            pacmanInformation = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height);

            foreach (var x in mainCanvas.Children.OfType<Rectangle>())
            {
                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                if ((string)x.Tag == "wall")
                {
                    if (left == true && pacmanInformation.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 10);
                        stopLeft = true;
                        left = false;
                    }

                    if (right == true && pacmanInformation.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 10);
                        stopRight = true;
                        right = false;
                    }

                    if (down == true && pacmanInformation.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                        stopDown = true;
                        down = false;
                    }

                    if (up == true && pacmanInformation.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) + 10);
                        stopUp = true;
                        up = false;
                    }

                }

                if ((string)x.Tag == "coin") // 35
                {
                    if (pacmanInformation.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        score++;
                    }
                }



                if ((string)x.Tag == "ghost")
                {
                    if(pacmanInformation.IntersectsWith(hitBox))
                        GameOver("Ты попался в руки призраха (хоть у него и нет рук), нажми Ok и начни снова");

                    if (x.Name.ToString() == "ghoustYellow")
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + ghostSpeed);
                    if (x.Name.ToString() == "ghoustRed")
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + ghostSpeed);
                    if (x.Name.ToString() == "ghoustBlue")
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + ghostSpeed);
                    currentGhostStep--;
                    if (currentGhostStep < 1)
                    {
                        currentGhostStep = ghostMoveStep;
                        ghostSpeed = -ghostSpeed;
                    }
                }
            }

            if (score == 35)
                GameOver("Ты выйграл, поздравдяю");
        }

        private void GameOver(string message)
        {
            gameTimer.Stop();
            MessageBox.Show(message, "Вы проиграли");

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        } 
    }
}
