using System;
using System.Collections.Generic;

namespace Xiangqi
{
    public class main
    {
        public enum GameState
        {
            SelectPiece,
            SelectMove,
            Gameover
        }

        public static GameState gameState = GameState.SelectPiece;
        public static Game g = new Game();
        static String[] steam = { "red", "black" };
        static string team = "red";

        public static void Main(string[] args)
        {
            printBoard();
        }
        public static void AskForInput()
        {      
            try
            {
                switch (gameState)
                {
                    case GameState.SelectPiece:
                        Console.WriteLine($"It is team {g.getTeam()}'s turn.Please choose a chess by entering the coordinate.");
                        string input = Console.ReadLine();
                        string[] inputArray = input.Split(",");
                        if (input.Length != 3)
                        {
                            throw new ArgumentException("This is not a coordinate!");
                        }
                        int x = Int32.Parse(inputArray[0]);
                        int y = Int32.Parse(inputArray[1]);
                        if (x > 9 || x <0 || y < 0 || y > 8)
                        {
                            throw new ArgumentException("This is not a coordinate!");
                        }

                        g.ChoosePiece(x, y);
                        ChangeState(GameState.SelectMove);
                        break;
                    case GameState.SelectMove:
                        Console.WriteLine($"You have choosed {g.getChoosedChess().getName()}, please choose your move");
                        string dest = Console.ReadLine();
                        string[] destArray = dest.Split(",");
                        int x1 = Int32.Parse(destArray[0]);
                        int y1 = Int32.Parse(destArray[1]);
                        g.MovePiece(x1, y1);
                        g.refresh(g.getBoard(), g.getrc(), g.getbc());
                        ChangeState(GameState.SelectPiece);
                        g.switchTeam();
                        break;
                }
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                ChangeState(GameState.SelectPiece);
            }
            catch (ArithmeticException e)
            {
                Console.WriteLine(e.Message);
                ChangeState(GameState.SelectMove);
            }
            printBoard();
        }
        public static void printBoard()
        {
            Console.Clear();
            for (int i = 0; i < 9; i++)
            {
                if (i == 0)
                {
                    Console.Write("  ");
                }
                Console.Write($"{i} ");
            }
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0)
                    {
                        Console.Write($"{i} ");
                    }
                    if (g.getChoosedChess().getName() != "")
                    {
                        List<string> area = g.getChoosedChess().moveableArea(g.getrc(), g.getbc(), g.getBoard());
                        string coord = i + "," + j;
                        if (area.Contains(coord))
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                    }
                    string piece = g.getBoard()[i, j];
                    if (piece == "俥" || piece == "兵" || piece == "炮" || piece == "傌" || piece == "相" || piece == "仕" || piece == "帅")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(g.getBoard()[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("\n");
            }
            AskForInput();


        }
        public static void ChangeState(GameState newState)
        {
            gameState = newState;
            switch (newState)
            {
                case GameState.SelectMove:
                    break;

                case GameState.SelectPiece:
                    break;
            }
        }
    }
}
