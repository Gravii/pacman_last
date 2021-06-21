using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GamePacMan
{
    internal class GameProcess
    {
        readonly Canvas mainCanvas;
        bool goLeft, goRight, goDown, goUp;
        bool stopLeft, stopRight, stopDown, stopUp;


        internal void TryMoveRight(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right && stopRight == false)
            {
                stopLeft = stopDown = stopUp = false;
                goLeft = goDown = goUp = false;

                goRight = true;
                //pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
            }
        }
        //internal void TryMoveUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Up && stopUp == false)
        //    {
        //        stopLeft = stopDown = stopRight = false;
        //        goLeft = goDown = goRight = false;

        //        goUp = true;
        //        pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
        //    }
        //}
        //internal void TryMoveLeft(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Left && stopLeft == false)
        //    {
        //        stopRight = stopUp = stopDown = false;
        //        goRight = goUp = goDown = false;

        //        goLeft = true;
        //        pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);
        //    }
        //}
        internal void TryMoveDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && stopDown == false)
            {
                stopLeft = stopRight = stopUp = false;
                goLeft = goRight = goUp = false;

                goDown = true;
                //pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
            }
        }
    }
}
