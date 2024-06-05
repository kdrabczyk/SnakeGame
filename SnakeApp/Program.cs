namespace SnakeGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            SnakeGame game = new SnakeGame();
            game.Run();
        }
    }

    internal class SnakeGame
    {
        private int _width = 40;
        private int _height = 20;
        private int _score = 0;
        private List<Position> _snake = new List<Position>();
        private Position _food;
        private Direction _direction = Direction.Right;
        private bool _gameOver = false;

        public void Run()
        {
            InitializeGame();
            while (!_gameOver)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    //ChangeDirection(key);
                }
                DrawBoard();
                MoveSnake();
                //CheckCollision();
                Thread.Sleep(100);
            }
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.WriteLine($"Your score: {_score}");
        }

        private void InitializeGame()
        {
            _snake.Add(new Position(5, 5));
            //GenerateFood();
        }

        private void DrawBoard()
        {
            Console.Clear();
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (i == 0 || i == _height - 1 || j == 0 || j == _width - 1)
                    {
                        Console.Write("#");
                    }
                    else if (_snake.Contains(new Position(i, j)))
                    {
                        Console.Write("O");
                    }
                    else if (_food.Equals(new Position(i, j)))
                    {
                        Console.Write("F");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Score: {_score}");
        }

        private void MoveSnake()
        {
            Position head = _snake[0];
            Position newHead = head;

            switch (_direction)
            {
                case Direction.Right:
                    newHead = new Position(head.X, head.Y + 1);
                    break;

                case Direction.Left:
                    newHead = new Position(head.X, head.Y - 1);
                    break;

                case Direction.Up:
                    newHead = new Position(head.X - 1, head.Y);
                    break;

                case Direction.Down:
                    newHead = new Position(head.X + 1, head.Y);
                    break;
            }

            _snake.Insert(0, newHead);
            if (newHead.Equals(_food))
            {
                _score++;
                GenerateFood();
            }
            else
            {
                _snake.RemoveAt(_snake.Count - 1);
            }
        }
    }

    internal enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    internal struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Position)
            {
                Position p = (Position)obj;
                return X == p.X && Y == p.Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}