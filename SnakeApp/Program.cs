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
        private int _width = 50;
        private int _height = 25;
        private int _score = 0;
        private int _score2 = 0;
        private List<Position> _snake = new List<Position>();
        private List<Position> _snake2 = new List<Position>();
        private Position _food;
        private Direction _direction = Direction.Right;
        private Direction _direction2 = Direction.Left;
        private bool _gameOver = false;

        public void Run()
        {
            InitializeGame();
            while (!_gameOver)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    ChangeDirection(key);
                }
                DrawBoard();
                MoveSnake();
                CheckCollision();
                Thread.Sleep(200);
            }
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.WriteLine($"Player1: {_score}");
            Console.WriteLine($"Player2: {_score2}");
        }

        private void InitializeGame()
        {
            _snake.Add(new Position(5, 5));
            _snake2.Add(new Position(15, 15));
            GenerateFood();
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
                        Console.Write("1");
                    }
                    else if (_snake2.Contains(new Position(i, j)))
                    {
                        Console.Write("2");
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
            Console.WriteLine($"Player1: {_score}");
            Console.WriteLine($"Player2: {_score2}");
        }

        private void MoveSnake()
        {
            MoveSingleSnake(_snake, _direction, ref _score);
            MoveSingleSnake(_snake2, _direction2, ref _score2);
        }

        private void MoveSingleSnake(List<Position> snake, Direction direction, ref int score)
        {
            Position head = snake[0];
            Position newHead = head;

            switch (direction)
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

            snake.Insert(0, newHead);
            if (newHead.Equals(_food))
            {
                score++;
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void ChangeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (_direction != Direction.Right)
                        _direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    if (_direction != Direction.Left)
                        _direction = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    if (_direction != Direction.Down)
                        _direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (_direction != Direction.Up)
                        _direction = Direction.Down;
                    break;
                case ConsoleKey.A:
                    if (_direction2 != Direction.Right)
                        _direction2 = Direction.Left;
                    break;
                case ConsoleKey.D:
                    if (_direction2 != Direction.Left)
                        _direction2 = Direction.Right;
                    break;
                case ConsoleKey.W:
                    if (_direction2 != Direction.Down)
                        _direction2 = Direction.Up;
                    break;
                case ConsoleKey.S:
                    if (_direction2 != Direction.Up)
                        _direction2 = Direction.Down;
                    break;


            }
        }

        private void GenerateFood()
        {
            Random rand = new Random();
            _food = new Position(rand.Next(1, _height - 1), rand.Next(1, _width - 1));
        }

        private void CheckCollision()
        {
            CheckSingleCollision(_snake, _snake2);
            CheckSingleCollision(_snake2, _snake);

        }

        private void CheckSingleCollision(List<Position> snake, List<Position> otherSnake)
        {
            Position head = snake[0];
            if (head.X <= 0 || head.X >= _height - 1 || head.Y <= 0 || head.Y >= _width - 1 ||
                snake.GetRange(1, snake.Count - 1).Contains(head) || otherSnake.Contains(head))
            {
                _gameOver = true;
            }
        }
    }

    struct Position
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

    enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }
}