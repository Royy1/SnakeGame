using System;

class Food
{
    public Game.Point Position { get; private set; }
    private Random rand = new Random();
    private int width;
    private int height;

    public Food(int width, int height)
    {
        this.width = width;
        this.height = height;
        Generate(null);
    }

    public void Generate(Snake snake)
    {
        do
        {
            Position = new Game.Point(rand.Next(1, width - 1), rand.Next(1, height - 1));
        } while (snake != null && snake.IsSnake(Position.X, Position.Y));
    }

    public bool IsFood(int x, int y)
    {
        return Position.X == x && Position.Y == y;
    }
}
