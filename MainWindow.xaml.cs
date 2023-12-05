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

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel vm = new ViewModel();
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
            timer.Tick += GameTickCalculation;
        }
        private double angle = 155;
        private double speed = 5;
        private int padSpeed = 7;

        void GameTickCalculation(object sender, EventArgs e)
        {
            if (vm.BallYPosition <= 0)
                angle = angle + (180 - 2 * angle);
            if (vm.BallYPosition >= MainCanvas.ActualHeight - 20)
                angle = angle + (180 - 2 * angle);

            if (CheckCollision())
            {
                ChangeAngle();
                vm.changeBallDirection();
            }

            double radians = (Math.PI / 180) * angle;
            Vector vector = new Vector { X = Math.Sin(radians), Y = -Math.Cos(radians) };
            vm.BallXPosition += vector.X * speed;
            vm.BallYPosition += vector.Y * speed;
            if (vm.LeftResult == 0 || vm.RightResult == 0)
            {

                MessageBox.Show("Игра завершена!");
                Application.Current.Shutdown(); // Закрываем приложение
                return;

            }
            if (vm.BallXPosition >= MainCanvas.ActualWidth - 10)
            {
                if (vm.LeftResult > 0)
                {
                    vm.LeftResult -= 1;
                    if (vm.LeftResult == 0)
                    {
                        MessageBox.Show("Левый игрок проиграл!");
                        return;
                    }
                    GameResetBallPosition();
                }

            }
            if (vm.BallXPosition <= -10)
            {
                if (vm.RightResult > 0)
                {
                    vm.RightResult -= 1;
                    if (vm.RightResult == 0)
                    {
                        MessageBox.Show("Правый игрок проиграл!");
                        
                        return; 
                    }
                    GameResetBallPosition();
                }
            }
            if (CheckCollision())
            {
                ChangeAngle();
                vm.changeBallDirection();
            }
        }

        private void GameResetBallPosition()
        {
            vm.BallXPosition = 380;
            vm.BallYPosition = 210;
        }

        private void ChangeAngle()
        {
            if (vm.IsBallDirectionRight)
                angle = 270 - ((vm.BallYPosition + 10) - (vm.RightPadPosition + 40));
            else
                angle = 90 + ((vm.BallYPosition + 10) - (vm.LeftPadPosition + 40));
        }

        private bool CheckCollision()
        {
            if (vm.IsBallDirectionRight)
                return vm.BallXPosition >= 760 && (vm.BallYPosition > vm.RightPadPosition - 20 && vm.BallYPosition < vm.RightPadPosition + 80);

            return vm.BallXPosition <= 20 && (vm.BallYPosition > vm.LeftPadPosition - 20 && vm.BallYPosition < vm.LeftPadPosition + 80);
        }

        private void MainWindow_OnKeyDown(object sender, KeyboardEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W)) vm.LeftPadPosition = verifyBounds(vm.LeftPadPosition, -padSpeed);
            if (Keyboard.IsKeyDown(Key.S)) vm.LeftPadPosition = verifyBounds(vm.LeftPadPosition, padSpeed);

            if (Keyboard.IsKeyDown(Key.Up)) vm.RightPadPosition = verifyBounds(vm.RightPadPosition, -padSpeed);
            if (Keyboard.IsKeyDown(Key.Down)) vm.RightPadPosition = verifyBounds(vm.RightPadPosition, padSpeed);
        }

        private int verifyBounds(int position, int change)
        {
            position += change;

            if (position < 0)
                position = 0;
            if (position > (int)MainCanvas.ActualHeight - 90)
                position = (int)MainCanvas.ActualHeight - 90;

            return position;
        }
    }
}

    

