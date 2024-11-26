using System;
using System.Threading;

class Game
{
    public class Point{
        public int X{get; set;}
        public int Y{get; set;}
        
        public Point(int x, int y){
            X = x;
            Y = y;
        }
    }
    private Snake snake;
    private Food food;
    private int width = 20;
    private int height = 10;
    private int score = 0;
    private int highScore;
    private bool gameOver = false;
    private int difficultyLevel = 1;
    private HighScoreManager highScoreManager = new HighScoreManager("highscore.txt");

    public void ShowMenu()
    {
        highScore = highScoreManager.ReadHighScore();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SNAKE GAME ===");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Instructions");
            Console.WriteLine("3. Choose Difficulty");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            ConsoleKey choice = Console.ReadKey(true).Key;
            switch (choice)
            {
                case ConsoleKey.D1:
                    StartGame();
                    break;
                case ConsoleKey.D2:
                    ShowInstructions();
                    break;
                case ConsoleKey.D3:
                    ChooseDifficulty();
                    break;
                case ConsoleKey.D4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice! Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void ShowInstructions()
    {
        Console.Clear();
        Console.WriteLine("=== INSTRUCTIONS ===");
        Console.WriteLine("Use the arrow keys to move the snake.");
        Console.WriteLine("Don't hit the walls or yourself!");
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    private void ChooseDifficulty()
    {
        Console.Clear();
        Console.WriteLine("=== CHOOSE DIFFICULTY ===");
        Console.WriteLine("1. Easy (Slow speed)");
        Console.WriteLine("2. Medium (Normal speed)");
        Console.WriteLine("3. Hard (Fast speed)");
        Console.Write("Choose difficulty: ");

        ConsoleKey choice = Console.ReadKey(true).Key;
        difficultyLevel = choice switch
        {
            ConsoleKey.D1 => 1,
            ConsoleKey.D2 => 2,
            ConsoleKey.D3 => 3,
            _ => 1
        };

        Console.WriteLine($"Difficulty set to {GetDifficultyText()}.");
        Thread.Sleep(1000);
    }

    private string GetDifficultyText()
    {
        return difficultyLevel switch
        {
            1 => "Easy",
            2 => "Medium",
            3 => "Hard",
            _ => "Easy"
        };
    }

    private void StartGame()
    {
        Console.Clear();
        gameOver = false;
        score = 0;

        snake = new Snake(width / 2, height / 2);
        food = new Food(width, height);

        int speed = difficultyLevel switch
        {
            1 => 200,
            2 => 150,
            3 => 100,
            _ => 200
        };

        while (!gameOver)
        {
            Draw();
            snake.HandleInput();
            Update();
            Thread.Sleep(speed);
        }

        EndGame();
    }

    private void Draw()
    {
        Console.Clear();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    Console.Write("#");
                else if (snake.IsSnake(x, y))
                    Console.Write("O");
                else if (food.IsFood(x, y))
                    Console.Write("F");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }

        Console.SetCursorPosition(0, height);
        Console.WriteLine($"Score: {score}  High Score: {highScore}  Difficulty: {GetDifficultyText()}");
    }

    private void Update()
    {
        if (snake.Move(food))
        {
            score++;
            food.Generate(snake);
        }
        else if (snake.CheckCollision(width, height))
        {
            gameOver = true;
        }
    }

    private void EndGame()
    {
        Console.Clear();
        Console.WriteLine("=== GAME OVER ===");
        Console.WriteLine($"Your Score: {score}");

        if (score > highScore)
        {
            highScore = score;
            highScoreManager.WriteHighScore(highScore);
            Console.WriteLine("New High Score!");
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
