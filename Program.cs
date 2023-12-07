using System;
using System.Collections.Generic;

namespace PR9
{
    public enum Border
    {
        MaxRight = 30,
        MaxLower = 30
    }
    internal class Program
    {
        public static List<(int, int)> snake;
        private static (int, int) food;
        public static void Main()
        {
            snake = new List<(int, int)> { ((int)Border.MaxRight - 10, (int)Border.MaxLower - 10) };
            Food();
            StartGame();
        }
        private static void Food()
        {
            Random rnd = new Random();
            int x;
            int y;
            do
            {
                x = rnd.Next(0, (int)Border.MaxRight);
                y = rnd.Next(0, (int)Border.MaxLower);
            }
            while (snake.Contains((x, y)));
            food = (x, y);
        }
        public static void StartGame()
        {
            while (true)
            {
                Snake();
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    var segment = snake[0];
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        segment = (segment.Item1, segment.Item2 - 1);
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        segment = (segment.Item1, segment.Item2 + 1);
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        segment = (segment.Item1 - 1, segment.Item2);
                    }
                    else if ( key.Key == ConsoleKey.RightArrow)
                    {
                        segment = (segment.Item1 + 1, segment.Item2);
                    }

                    if (segment.Item1 < 0 || segment.Item1 >= (int)Border.MaxRight || segment.Item2 < 0 || segment.Item2 >= (int)Border.MaxLower || snake.Contains(segment))
                    {
                        Console.Clear();
                        Console.WriteLine("Потрачено");
                        Environment.Exit(0);
                    }
                    snake.Insert(0, segment);
                    if (segment == food)
                    {
                        Food();
                    }
                    else
                    {
                        snake.RemoveAt(snake.Count - 1);
                    }
                }
                Thread.Sleep(100);
            }
        }
        private static void Snake()
        {
            Console.Clear();

            for (int i = 0; i < (int)Border.MaxLower; i++)
            {
                Console.SetCursorPosition((int)Border.MaxRight, i);
                Console.Write("|");
            }

            for (int i = 0; i < (int)Border.MaxRight; i++)
            {
                Console.SetCursorPosition(i, (int)Border.MaxLower);
                Console.Write("-");
            }

            foreach (var item in snake)
            {
                Console.SetCursorPosition(item.Item1, item.Item2);
                Console.Write("O");
            }
            Console.SetCursorPosition(food.Item1, food.Item2);
            Console.Write("+");
        }
    }
}