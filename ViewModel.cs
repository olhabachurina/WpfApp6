using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6
{
    class ViewModel : INotifyPropertyChanged
    {
        private int leftPadPosition = 180;
        private int rightPadPosition = 180;
        private int leftResult = 10;
        private int rightResult = 10;
        private Ball ball = new Ball { XPosition = 380, YPosition = 210, IsDirectionRight = true };

        public int LeftPadPosition
        {
            get { return leftPadPosition; }
            set
            {
                leftPadPosition = value;
                OnPropertyChanged("LeftPadPosition");
            }
        }

        public int RightPadPosition
        {
            get { return rightPadPosition; }
            set
            {
                rightPadPosition = value;
                OnPropertyChanged("RightPadPosition");
            }
        }

        public int LeftResult
        {
            get { return leftResult; }
            set
            {
                leftResult = value;
                OnPropertyChanged(nameof(LeftResult));
            }
        }

        public int RightResult
        {
            get { return rightResult; }
            set
            {
                rightResult = value;
                OnPropertyChanged(nameof(RightResult));
            }
        }

        public double BallXPosition
        {
            get { return ball.XPosition; }
            set
            {
                ball.XPosition = value;
                OnPropertyChanged("BallXPosition");
            }
        }


        public double BallYPosition
        {
            get { return ball.YPosition; }
            set
            {
                ball.YPosition = value;
                OnPropertyChanged("BallYPosition");
            }
        }

        public bool IsBallDirectionRight
        {
            get { return ball.IsDirectionRight; }
            set
            {
                ball.IsDirectionRight = value;
                OnPropertyChanged("IsBallDirectionRight");
            }
        }


        public void changeBallDirection()
        {
            IsBallDirectionRight = !IsBallDirectionRight;
        }
        public void PlayerMissedBall(bool isLeftPlayer)
        {
            if (isLeftPlayer)
            {
                LeftResult--;

                if (LeftResult == 0)
                {
                    // Левый игрок проиграл
                    Console.WriteLine("Левый игрок проиграл!");
                    // Добавьте вашу логику завершения игры для левого игрока здесь
                }
            }
            else
            {
                RightResult--;

                if (RightResult == 0)
                {
                    // Правый игрок проиграл
                    Console.WriteLine("Правый игрок проиграл!");
                    // Добавьте вашу логику завершения игры для правого игрока здесь
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler GameOver;

        protected virtual void OnGameOver(EventArgs e)
        {
            GameOver?.Invoke(this, e);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}