using System;

namespace ticTacToeGame
{
    public class clsTicTacToeAI
    {
        private clsTicTacToe game;

        public clsTicTacToeAI(clsTicTacToe currentGame)
        {
            game = currentGame;
        }

        public (int, int) GetBestMove(string aiSymbol, string playerSymbol)
        {
            string[,] board = game.GetBoard();

            // Try to win
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (string.IsNullOrEmpty(board[row, col]))
                    {
                        board[row, col] = aiSymbol;
                        if (WouldWin(board, aiSymbol))
                        {
                            return (row, col);
                        }
                        board[row, col] = string.Empty;
                    }
                }
            }

            // Block player's winning move
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (string.IsNullOrEmpty(board[row, col]))
                    {
                        board[row, col] = playerSymbol;
                        if (WouldWin(board, playerSymbol))
                        {
                            return (row, col);
                        }
                        board[row, col] = string.Empty;
                    }
                }
            }

            // Try to take center
            if (string.IsNullOrEmpty(board[1, 1]))
            {
                return (1, 1);
            }

            // Try to take corners
            int[,] corners = { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } };
            foreach (var i in new[] { 0, 1, 2, 3 })
            {
                int row = corners[i, 0];
                int col = corners[i, 1];
                if (string.IsNullOrEmpty(board[row, col]))
                {
                    return (row, col);
                }
            }

            // Take any available edge
            int[,] edges = { { 0, 1 }, { 1, 0 }, { 1, 2 }, { 2, 1 } };
            foreach (var i in new[] { 0, 1, 2, 3 })
            {
                int row = edges[i, 0];
                int col = edges[i, 1];
                if (string.IsNullOrEmpty(board[row, col]))
                {
                    return (row, col);
                }
            }

            return (-1, -1); // No moves available
        }

        private bool WouldWin(string[,] board, string symbol)
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == symbol && board[row, 1] == symbol && board[row, 2] == symbol)
                    return true;
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == symbol && board[1, col] == symbol && board[2, col] == symbol)
                    return true;
            }

            // Check diagonals
            if (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol)
                return true;

            if (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol)
                return true;

            return false;
        }
    }
}