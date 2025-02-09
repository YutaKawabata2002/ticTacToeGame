using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticTacToeGame
{
    /// <summary>
    /// Represents the Tic-Tac-Toe game logic
    /// </summary>
    public class clsTicTacToe
    {
        /// <summary>
        /// The game board
        /// </summary>
        private string[,] saBoard;

        /// <summary>
        /// Number of wins for Player 1
        /// </summary>
        private int iPlayer1Win;

        /// <summary>
        /// Number of wins for Player 2
        /// </summary>
        private int iPlayer2Win;

        /// <summary>
        /// Number of ties
        /// </summary>
        private int iTies;

        /// <summary>
        /// The winning move of the current game
        /// </summary>
        private WinningMove eWinningMove;

        /// <summary>
        /// Represents the possible winning moves in the game
        /// </summary>
        public enum WinningMove
        {
            None,
            Row1,
            Row2,
            Row3,
            Col1,
            Col2,
            Col3,
            Diag1,
            Diag2
        }

        /// <summary>
        /// Gets the number of wins for Player 1
        /// </summary>
        public int Player1Wins => iPlayer1Win;

        /// <summary>
        /// Gets the number of wins for Player 2
        /// </summary>
        public int Player2Wins => iPlayer2Win;

        /// <summary>
        /// Gets the number of ties
        /// </summary>
        public int Ties => iTies;

        /// <summary>
        /// Initializes a new instance of the clsTicTacToe class
        /// </summary>
        public clsTicTacToe()
        {
            saBoard = new string[3, 3];
            iPlayer1Win = 0;
            iPlayer2Win = 0;
            iTies = 0;
            ResetBoard();
        }

        /// <summary>
        /// Resets the game board
        /// </summary>
        public void ResetBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    saBoard[row, col] = string.Empty;
                }
            }
            eWinningMove = WinningMove.None;
        }

        /// <summary>
        /// Makes a move on the board
        /// </summary>
        /// <param name="row">The row of the move</param>
        /// <param name="col">The column of the move</param>
        /// <param name="playerSymbol">The symbol of the current player</param>
        /// <returns>True if the move was successful, false otherwise</returns>
        public bool MakeMove(int row, int col, string playerSymbol)
        {
            if (string.IsNullOrEmpty(saBoard[row, col]))
            {
                saBoard[row, col] = playerSymbol;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the last move was a winning move
        /// </summary>
        /// <returns>True if the last move was a winning move, false otherwise</returns>
        public bool IsWinningMove()
        {
            return IsHor() || IsVer() || IsDiag();
        }

        /// <summary>
        /// Checks for a horizontal win
        /// </summary>
        /// <returns>True if there's a horizontal win, false otherwise</returns>
        private bool IsHor()
        {
            for (int row = 0; row < 3; row++)
            {
                if (saBoard[row, 0] == saBoard[row, 1] && saBoard[row, 1] == saBoard[row, 2] && !string.IsNullOrEmpty(saBoard[row, 0]))
                {
                    eWinningMove = (WinningMove)(row + 1);
                    UpdateWinCount(saBoard[row, 0]);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for a vertical win
        /// </summary>
        /// <returns>True if there's a vertical win, false otherwise</returns>
        private bool IsVer()
        {
            for (int col = 0; col < 3; col++)
            {
                if (saBoard[0, col] == saBoard[1, col] && saBoard[1, col] == saBoard[2, col] && !string.IsNullOrEmpty(saBoard[0, col]))
                {
                    eWinningMove = (WinningMove)(col + 4);
                    UpdateWinCount(saBoard[0, col]);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for a diagonal win
        /// </summary>
        /// <returns>True if there's a diagonal win, false otherwise</returns>
        private bool IsDiag()
        {
            if (saBoard[0, 0] == saBoard[1, 1] && saBoard[1, 1] == saBoard[2, 2] && !string.IsNullOrEmpty(saBoard[0, 0]))
            {
                eWinningMove = WinningMove.Diag1;
                UpdateWinCount(saBoard[0, 0]);
                return true;
            }
            if (saBoard[0, 2] == saBoard[1, 1] && saBoard[1, 1] == saBoard[2, 0] && !string.IsNullOrEmpty(saBoard[0, 2]))
            {
                eWinningMove = WinningMove.Diag2;
                UpdateWinCount(saBoard[0, 2]);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the game is a tie
        /// </summary>
        /// <returns>True if the game is a tie, false otherwise</returns>
        public bool IsTie()
        {
            foreach (var cell in saBoard)
            {
                if (string.IsNullOrEmpty(cell))
                {
                    return false;
                }
            }
            iTies++;
            return true;
        }

        /// <summary>
        /// Updates the win count for the winning player
        /// </summary>
        /// <param name="winningSymbol">The symbol of the winning player</param>
        private void UpdateWinCount(string winningSymbol)
        {
            if (winningSymbol == "X")
                iPlayer1Win++;
            else
                iPlayer2Win++;
        }

        /// <summary>
        /// Gets the winning move of the current game
        /// </summary>
        /// <returns>The winning move enum value</returns>
        public WinningMove GetWinningMove()
        {
            return eWinningMove;
        }

        /// <summary>
        /// Gets a copy of the current game board
        /// </summary>
        public string[,] GetBoard()
        {
            return (string[,])saBoard.Clone();
        }
    }
}
