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

namespace Unwinnable_TicTacToe
{
    public partial class MainWindow : Window
    {
        private FieldState[] gameState;
        private bool isPlayerTurn;
        private bool isGameEnded;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            gameState = new FieldState[9];
            isPlayerTurn = true;
            isGameEnded = false;
            GameWindow.Children.Cast<Button>().ToList().ForEach(button =>
            {
                if (button.Name != "ButtonReset")
                {
                    button.Content = string.Empty;
                }
            });
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlayerTurn || isGameEnded)
            {
                return;
            }

            var button = (Button)sender;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            int clickedButtonIndex = row * 3 + column;
          
            if(gameState[clickedButtonIndex] == FieldState.Unassigned)
            {
                isPlayerTurn = false; 
                gameState[clickedButtonIndex] = FieldState.X;
                button.Content = "X";
                button.Foreground = Brushes.Blue;
                CheckGameEnded(column, row, clickedButtonIndex);
            }

            BotTurn();
        }

        private void BotTurn()
        {
            if (isPlayerTurn || isGameEnded)
            {
                return;
            }


            isPlayerTurn = true;

            //add MinMax algorithm to work and then check game for end  

        }

        private void CheckGameEnded(int column, int row, int clickedButtonIndex)
        {
            isGameEnded = CheckColumn(column) || CheckRow(row) || CheckDiagonals(clickedButtonIndex);
        }

        private bool CheckColumn(int column)
        {
            if(gameState[column] == gameState[column+3] && gameState[column] == gameState[column+6])
            {
                return true;
            }
            return false;
        }

        private bool CheckRow(int row)
        {
            if (gameState[row*3] == gameState[(row*3)+1] && gameState[row*3] == gameState[(row * 3) + 2])
            {
                return true;
            }
            return false;
        }

        private bool CheckDiagonals(int clickedButtonIndex)
        {
            if (clickedButtonIndex % 2 == 0 && gameState[4] != FieldState.Unassigned)
            {
                if ((gameState[0] == gameState[4] && gameState[0] == gameState[8]) || gameState[2] == gameState[4] && gameState[2] == gameState[6])
                {
                    return true;
                }
            }
                return false;
        }
    }
}
