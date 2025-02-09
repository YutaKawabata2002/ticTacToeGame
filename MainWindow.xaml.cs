using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ticTacToeGame
{
    public partial class MainWindow : Window
    {
        private clsTicTacToe TicTacToe;
        private clsTicTacToeAI AI;
        private bool bHasGameStarted;
        private bool bIsAIGame;
        private string currentPlayer;

        public MainWindow()
        {
            InitializeComponent();
            TicTacToe = new clsTicTacToe();
            AI = new clsTicTacToeAI(TicTacToe);
            bHasGameStarted = false;
            bIsAIGame = false;
            currentPlayer = "X"; // Player 1 starts
            UpdateStatusLabel();
        }

        private void startGamePvPBtn_Click(object sender, RoutedEventArgs e)
        {
            bHasGameStarted = true;
            bIsAIGame = false;
            StartNewGame();
        }

        private void startGameAIBtn_Click(object sender, RoutedEventArgs e)
        {
            bHasGameStarted = true;
            bIsAIGame = true;
            StartNewGame();
        }

        private void StartNewGame()
        {
            TicTacToe.ResetBoard();
            ResetLabels();
            ResetColors();
            currentPlayer = "X"; // Reset to Player 1
            UpdateStatusLabel();
        }

        private void PlayersMoveClick(object sender, MouseButtonEventArgs e)
        {
            if (!bHasGameStarted || (bIsAIGame && currentPlayer == "O"))
                return;

            var label = sender as Label;

            // Get the coordinates based on the label's name
            string[] parts = label.Name.Substring(3).ToCharArray().Select(c => c.ToString()).ToArray();
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            // Try to make the player's move
            if (TicTacToe.MakeMove(row, col, currentPlayer))
            {
                label.Content = currentPlayer;

                if (CheckGameEnd())
                    return;

                // **Switch to next player (X ↔ O)**
                currentPlayer = (currentPlayer == "X") ? "O" : "X";
                UpdateStatusLabel();

                // If it's AI's turn, make the AI move
                if (bIsAIGame && currentPlayer == "O")
                {
                    MakeAIMove();
                }
            }
        }


        private async void MakeAIMove()
        {
            // Add a small delay to make AI moves feel more natural
            await System.Threading.Tasks.Task.Delay(500);

            var (row, col) = AI.GetBestMove("O", "X");
            if (row != -1 && col != -1)
            {
                string labelName = $"Lbl{row}{col}";
                var label = this.FindName(labelName) as Label;

                if (TicTacToe.MakeMove(row, col, "O"))
                {
                    label.Content = "O";

                    if (!CheckGameEnd())
                    {
                        currentPlayer = "X";
                        UpdateStatusLabel();
                    }
                }
            }
        }

        private bool CheckGameEnd()
        {
            if (TicTacToe.IsWinningMove())
            {
                HighlightWinningMove(TicTacToe.GetWinningMove());
                UpdateStats();
                string winner = currentPlayer == "X" ? "Player 1" : (bIsAIGame ? "AI" : "Player 2");
                MessageBox.Show($"{winner} wins!");
                bHasGameStarted = false;
                return true;
            }
            else if (TicTacToe.IsTie())
            {
                UpdateStats();
                MessageBox.Show("It's a tie!");
                bHasGameStarted = false;
                return true;
            }
            return false;
        }

        private void ResetLabels()
        {
            Lbl00.Content = Lbl01.Content = Lbl02.Content = string.Empty;
            Lbl10.Content = Lbl11.Content = Lbl12.Content = string.Empty;
            Lbl20.Content = Lbl21.Content = Lbl22.Content = string.Empty;
        }

        private void ResetColors()
        {
            Lbl00.Background = Lbl01.Background = Lbl02.Background = Brushes.Transparent;
            Lbl10.Background = Lbl11.Background = Lbl12.Background = Brushes.Transparent;
            Lbl20.Background = Lbl21.Background = Lbl22.Background = Brushes.Transparent;
        }

        private void UpdateStatusLabel()
        {
            if (bIsAIGame)
            {
                StatusLabel.Content = currentPlayer == "X" ? "Your turn" : "AI's turn";
            }
            else
            {
                StatusLabel.Content = currentPlayer == "X" ? "Player 1's turn" : "Player 2's turn";
            }
        }

        private void HighlightWinningMove(clsTicTacToe.WinningMove winningMove)
        {
            switch (winningMove)
            {
                case clsTicTacToe.WinningMove.Row1:
                    Lbl00.Background = Lbl01.Background = Lbl02.Background = Brushes.Yellow;
                    break;
                case clsTicTacToe.WinningMove.Row2:
                    Lbl10.Background = Lbl11.Background = Lbl12.Background = Brushes.Yellow;
                    break;
                case clsTicTacToe.WinningMove.Row3:
                    Lbl20.Background = Lbl21.Background = Lbl22.Background = Brushes.Yellow;
                    break;
                case clsTicTacToe.WinningMove.Col1:
                    Lbl00.Background = Lbl10.Background = Lbl20.Background = Brushes.Yellow;
                    break;
                case clsTicTacToe.WinningMove.Col2:
                    Lbl01.Background = Lbl11.Background = Lbl21.Background = Brushes.Yellow;
                    break;
                case clsTicTacToe.WinningMove.Col3:
                    Lbl02.Background = Lbl12.Background = Lbl22.Background = Brushes.Yellow;
                    break;
                case clsTicTacToe.WinningMove.Diag1:
                    Lbl00.Background = Lbl11.Background = Lbl22.Background = Brushes.Yellow;
                    break;
                case clsTicTacToe.WinningMove.Diag2:
                    Lbl02.Background = Lbl11.Background = Lbl20.Background = Brushes.Yellow;
                    break;
            }
        }

        private void UpdateStats()
        {
            Player1WinsLabel.Content = $"Player 1 Wins: {TicTacToe.Player1Wins}";
            Player2WinsLabel.Content = $"{(bIsAIGame ? "AI" : "Player 2")} Wins: {TicTacToe.Player2Wins}";
            TiesLabel.Content = $"Ties: {TicTacToe.Ties}";
        }
    }
}