using System;
using System.Collections.Generic;

class Snake
{
    private List<Game.Point> body = new List<Game.Point>();
    private int dx = 1, dy = 0;

    public Snake(int startX, int startY)
    {
        body.Add(new Game.Point(startX, startY));
    }

    public bool IsSnake(int x, int y)
    {
        return body.Exists(p => p.X == x && p.Y == y);
    }

    public void HandleInput()
    {
        if (!Console.KeyAvailable) return;

        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.UpArrow && dy == 0) { dx = 0; dy = -1; }
        if (key == ConsoleKey.DownArrow && dy == 0) { dx = 0; dy = 1; }
        if (key == ConsoleKey.LeftArrow && dx == 0) { dx = -1; dy = 0; }
        if (key == ConsoleKey.RightArrow && dx == 0) { dx = 1; dy = 0; }
    }

    public bool Move(Food food)
    {
        var head = new Game.Point(body[0].X + dx, body[0].Y + dy);
        body.Insert(0, head);

        if (head.X == food.Position.X && head.Y == food.Position.Y)
            return true;

        body.RemoveAt(body.Count - 1);
        return false;
    }

    public bool CheckCollision(int width, int height)
    {
        var head = body[0];
        return head.X <= 0 || head.X >= width - 1 || head.Y <= 0 || head.Y >= height - 1 ||
               body.GetRange(1, body.Count - 1).Exists(p => p.X == head.X && p.Y == head.Y);
    }
}
