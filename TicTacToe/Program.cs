using System;

class TicTacToe
{
    static char[,] board;
    static char currentPlayer;

    static void Main(string[] args)
    {
        bool playAgain = true;
        while (playAgain)
        {
            InitializeGame();
            PlayGame();
            playAgain = AskPlayAgain();
        }
    }

    static void InitializeGame()
    {
        board = new char[3, 3];
        currentPlayer = 'X';
        InitializeBoard();
    }

    static void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = ' ';
    }

    static void PrintBoard()
    {
        Console.WriteLine("  0 1 2");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
                if (j < 2)
                    Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2)
                Console.WriteLine("  -----");
        }
    }

    static void PlayGame()
    {
        bool gameEnd = false;
        while (!gameEnd)
        {
            Console.Clear();
            PrintBoard();
            Console.WriteLine($"Player {currentPlayer}, enter your move (row and column, e.g. 1 2): ");

            var input = Console.ReadLine().Split();
            if (input.Length == 2 && int.TryParse(input[0], out int row) && int.TryParse(input[1], out int col) &&
                row >= 0 && row < 3 && col >= 0 && col < 3)
            {
                if (board[row, col] == ' ')
                {
                    board[row, col] = currentPlayer;
                    if (CheckWin(row, col))
                    {
                        gameEnd = true;
                        Console.Clear();
                        PrintBoard();
                        Console.WriteLine($"Player {currentPlayer} wins!");
                    }
                    else if (CheckDraw())
                    {
                        gameEnd = true;
                        Console.Clear();
                        PrintBoard();
                        Console.WriteLine("The game is a draw!");
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    }
                }
                else
                {
                    Console.WriteLine("This move is not valid. Press any key to try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key to try again.");
                Console.ReadKey();
            }
        }
    }

    static bool CheckWin(int row, int col)
    {
        // Check the row
        if (board[row, 0] == currentPlayer && board[row, 1] == currentPlayer && board[row, 2] == currentPlayer)
            return true;

        // Check the column
        if (board[0, col] == currentPlayer && board[1, col] == currentPlayer && board[2, col] == currentPlayer)
            return true;

        // Check the diagonals
        if (row == col && board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
            return true;

        if (row + col == 2 && board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
            return true;

        return false;
    }

    static bool CheckDraw()
    {
        foreach (char c in board)
        {
            if (c == ' ')
                return false;
        }
        return true;
    }

    static bool AskPlayAgain()
    {
        Console.WriteLine("Do you want to play again? (y/n): ");
        string response = Console.ReadLine().ToLower();
        return response == "y" || response == "yes";
    }
}
