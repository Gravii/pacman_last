using System.Windows.Controls;
using System.Windows.Input;

namespace GamePacMan
{
    public class Game
    {
        private readonly Canvas mainCanvas;

        int score = 0;

        GameProcess gameProcess;
        public Game()
        {
        }

        internal void keyDownCanvas(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    
                    break;
                case Key.Left:

                    break;
                case Key.Right:
                    break;
                case Key.Down:
                    break;
            }    
        }
    }
}