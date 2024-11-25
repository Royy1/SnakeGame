
using System.Globalization;
using System.Runtime.CompilerServices;

public class Point{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y) {
        X = x;
        Y = y;
    }
}
class Program
{
    static List<Point> snake = new List<Point>();
    static Point food;
    const int width = 20;
    const int height = 10;
    static int dx = 1, dy = 0;
    static bool gameOver = false;
    static Random random= new Random();
    static int score = 0;
    static void GenerateFood(){
        food = new Point(random.Next(1,width -1),random.Next(1,height -1));
    }
    static void InitializeGame(){
        snake.Clear();
        snake.Add(new Point(width/2, height/2));
        GenerateFood();
    }

    static void Draw(){
        Console.Clear();
        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                if(x == 0 || x == width-1 || y == 0 || y == height-1){
                    Console.Write("#"); //wall
                }
                else if (x == food.X && y == food.Y){
                    Console.Write("\u2665"); //food
                }
                else if (snake.Any(p => p.X == x && p.Y == y)) {
                    Console.Write("*"); // snake
                }
                else{
                    Console.Write(" ");
                }
            }
            Console.WriteLine();  
        }
       
    }   
    static void Update (){
        Point newHead = new Point(snake[0].X+dx,snake[0].Y+dy);
        if(newHead.X <= 0 || newHead.Y>= width -1 || newHead.Y <= 0 || newHead.Y >= height-1 || snake.Any(p=> p.X == newHead.X && p.Y == newHead.Y) ){
            gameOver = true;
            return;
        }
        snake.Insert(0,newHead);    
        if (newHead.X == food.X && newHead.Y == food.Y){
            score++;
            GenerateFood();
        }
        else {
            snake.RemoveAt(snake.Count - 1);
        }
    }
    static void Input (){
        if(Console.KeyAvailable){
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key){
                case ConsoleKey.UpArrow:
                if (dy==0){
                    dx = 0;
                    dy = -1;
                 }
                break;
                case ConsoleKey.DownArrow:
                if (dy==0){
                    dx = 0;
                    dy = 1;
                }
                break;
                case ConsoleKey.LeftArrow:
                if (dx==0){
                    dx = -1;
                    dy = 0;   
                }
                break;
                case ConsoleKey.RightArrow:
                if(dx==0){
                    dx = 1;
                    dy = 0;
                }
                break;
            }
        }
    }

    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        InitializeGame();

        while(!gameOver){
        Draw();
        Input();
        Update();
        Thread.Sleep(200);
        }
        
        Console.SetCursorPosition(0,height);
        Console.WriteLine("Score: " + score);
    }
}