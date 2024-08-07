using System.Security.Cryptography.X509Certificates;

namespace CatchEggs;
class GameBoard
{
    private static int _row = 15;
    private static int _col = 50;
    private int _speed = 500;
    private string _direction = "RIGHT";
    public static string[,] _board = new string[_row, _col];
    public List<Point> EggList = new List<Point>();
    public Point[] Barstrict = new Point[]
    {
        new(_row - 2, 21),
        new(_row - 2, 22),
        new(_row - 2, 23),
        new(_row - 2, 24),
        new(_row - 2, 25),
    };

    public void InitGameBoard()
    {
        for (int row = 0; row < _row; row++)
        {
            for (int col = 0; col < _col; col++)
            {
                if (row == 0 || row == _row - 1 || col == 0 || col == _col - 1)
                {
                    _board[row, col] = "#";
                }
                else
                {
                    _board[row, col] = " ";
                }
            }
        }
    }

    public void DrawBoard()
    {
        for (int row = 0; row < _row; row++)
        {
            for (int col = 0; col < _col; col++)
            {
                Console.Write(_board[row, col]);
            }
            Console.WriteLine();
        }
    }

    public void AddEggToList()
    {
        Egg egg = new();
        EggList.Add(egg.Position);
    }

    private void UpdateEggsPosition()
    {
        foreach (Point egg in EggList)
        {
            if (egg.X < _row - 1)
            {
                _board[egg.X, egg.Y] = " ";
                egg.X += 1;
                _board[egg.X, egg.Y] = "*";
            }
            else
            {
                egg.X = _row - 1;
                _board[egg.X, egg.Y] = "#";
            }
        }
    }

    public void UpdateBarstrict()
    {
        for (int i = 0; i < _col; i++)
        {
            for (int j = 0; j < Barstrict.Length; j++)
            {
                if (i == Barstrict[j].Y)
                {
                    _board[_row - 2, i] = "=";
                }
                // else
                // {
                //     _board[_row - 2, i] = " ";
                // }
            }
        }
        // for (int i = Barstrict.Length - 1; i > 0; i--)
        // {
        //     _board[Barstrict[i].X, Barstrict[i].Y] = "=";
        // }
    }

    public void MoveBarstrict()
    {
        if (_direction == "RIGHT")
        {
            for (int i = Barstrict.Length - 1; i > 0; i--)
            {
                if (Barstrict[i].Y < _col - 2)
                {
                    Barstrict[i].Y += 1;
                }
            }
        }
        if (_direction == "LEFT")
        {
            for (int i = 0; i < Barstrict.Length; i++)
            {
                if (Barstrict[i].Y > 0)
                {
                    Barstrict[i].Y -= 1;
                }
            }
        }
    }

    public void UpdateGame()
    {
        AddEggToList();
        UpdateEggsPosition();
        // UpdateBarstrict();
        // MoveBarstrict();
    }
    public void StartGame()
    {
        Thread thread = new Thread(ListenKey);
        thread.Start();
        InitGameBoard();
        do
        {
            Console.Clear();
            DrawBoard();
            UpdateGame();
            Task.Delay(_speed).Wait();
        }
        while (true);
    }

    public void ListenKey()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.RightArrow:
                _direction = "RIGHT";
                break;
            case ConsoleKey.LeftArrow:
                _direction = "LEFT";
                break;
        }

    }
}